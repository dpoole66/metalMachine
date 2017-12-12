using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateControllerBU : MonoBehaviour {

	// Character controller editor input
	[SerializeField] float m_MovingTurnSpeed = 360;
	[SerializeField] float m_StationaryTurnSpeed = 180;
	[SerializeField] float m_JumpPower = 12f;
	[Range(1f, 4f)][SerializeField] float m_GravityMultiplier = 2f;
	[SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
	[SerializeField] float m_MoveSpeedMultiplier = 1f;
	[SerializeField] float m_AnimSpeedMultiplier = 1f;
	[SerializeField] float m_GroundCheckDistance = 0.1f;



	public State currentState;
    public State remainState;
    public Stats Stats;
	public Transform ThisCam;
	public Transform  Enemy;
	public Transform GoTarget;

	// Character Controller
	Rigidbody m_Rigidbody;
	bool Grounded;
    bool Crouching;
    float m_OrigGroundCheckDistance;
	const float k_Half = 0.5f;
	float Turn;
	float Forward;
	Vector3 m_GroundNormal;
	float m_CapsuleHeight;
	Vector3 m_CapsuleCenter;
	CapsuleCollider m_Capsule;

	// OLD Movement for Animator
	//[HideInInspector] public float m_Forward;
	//[HideInInspector] public float m_Turn;
	[HideInInspector] public Animator ThisAnimator;
	[HideInInspector] public NavMeshAgent  ThisAgent;

	int ForwardHash = Animator.StringToHash("Forward");
	int TurnHash = Animator.StringToHash("Turn");
	int GroundHash = Animator.StringToHash("Grounded");
	int CrouchHash = Animator.StringToHash("Crouching");
    int RotationHash = Animator.StringToHash("Rotation");

    void Awake () {

		ThisAgent = GetComponent<NavMeshAgent> ();
		ThisAnimator = GetComponent<Animator> ();
        Enemy = GetComponent<Transform>();

		// Character Controller
		m_Rigidbody = GetComponent<Rigidbody>();
		m_Capsule = GetComponent<CapsuleCollider>();
		m_CapsuleHeight = m_Capsule.height;
		m_CapsuleCenter = m_Capsule.center;

		m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		m_OrigGroundCheckDistance = m_GroundCheckDistance;
	
	}

	void Update(){

		//currentState.UpdateState (this);
		// Old Animation control
		float Forward = ThisAgent.velocity.z * Stats.moveSpeed;
		float Turn = ThisAgent.velocity.x  * Stats.turnSpeed;
		ThisAnimator.SetFloat (ForwardHash, Forward );
		ThisAnimator.SetFloat (TurnHash, Turn);

        ThisAgent.transform.LookAt(Enemy.transform.position);
    }

	void OnDrawGizmos(){

		if (currentState != null && ThisCam != null) {

			Gizmos.color = currentState.sceneGizmoColor;
			Gizmos.DrawWireSphere (ThisCam.position, 0.7f);

		}

	}

	public void TransitionToState(State nextState){

		if (nextState != remainState) {

			currentState = nextState;

		}

	}


	public void Move(Vector3 move, bool crouch, bool jump)
	{

		// convert the world relative moveInput vector into a local-relative
		// turn amount and forward amount required to head in the desired
		// direction.
		if (move.magnitude > 1f) move.Normalize();
		move = transform.InverseTransformDirection(move);
		CheckGroundStatus();
		move = Vector3.ProjectOnPlane(move, m_GroundNormal);
        Turn = Mathf.Atan2(move.x, move.z);
        Forward = move.z;

		ApplyExtraTurnRotation();

		// control and velocity handling is different when grounded and airborne:
		if (Grounded)
		{
			HandleGroundedMovement(crouch, jump);
		}
		else
		{
			HandleAirborneMovement();
		}

		ScaleCapsuleForCrouching(crouch);
		PreventStandingInLowHeadroom();

		// send input and other state parameters to the animator
		UpdateAnimator(move);
	}

	void ScaleCapsuleForCrouching(bool crouch)
	{
		if (Grounded && crouch)
		{
			if (Crouching) return;
			m_Capsule.height = m_Capsule.height / 2f;
			m_Capsule.center = m_Capsule.center / 2f;
            Crouching = true;
		}
		else
		{
			Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
			float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
			if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
			{
                Crouching = true;
				return;
			}
			m_Capsule.height = m_CapsuleHeight;
			m_Capsule.center = m_CapsuleCenter;
            Crouching = false;
		}
	}

	void PreventStandingInLowHeadroom()
	{
		// prevent standing up in crouch-only zones
		if (!Crouching)
		{
			Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
			float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
			if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
			{
                Crouching = true;
			}
		}
	}


	void UpdateAnimator(Vector3 move)
	{
		// update the animator parameters
		ThisAnimator.SetFloat(ForwardHash, Forward, 0.1f, Time.deltaTime);
		ThisAnimator.SetFloat(TurnHash, Turn, 0.1f, Time.deltaTime);
		ThisAnimator.SetBool(CrouchHash, Crouching);
		ThisAnimator.SetBool(GroundHash, Grounded);
        ThisAnimator.SetFloat(RotationHash, ThisAgent.transform.rotation.y);
        if (!Grounded)
		{
			ThisAnimator.SetFloat("Jump", m_Rigidbody.velocity.y);
		}

		// calculate which leg is behind, so as to leave that leg trailing in the jump animation
		// (This code is reliant on the specific run cycle offset in our animations,
		// and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
		float runCycle =
			Mathf.Repeat(
				ThisAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
		float jumpLeg = (runCycle < k_Half ? 1 : -1) * Forward;
		if (Grounded)
		{
			ThisAnimator.SetFloat("JumpLeg", jumpLeg);
		}

		// the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
		// which affects the movement speed because of the root motion.
		if (Grounded && move.magnitude > 0)
		{
			ThisAnimator.speed = m_AnimSpeedMultiplier;
		}
		else
		{
			// don't use that while airborne
			ThisAnimator.speed = 1;
		}
	}


	void HandleAirborneMovement()
	{
		// apply extra gravity from multiplier:
		Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
		m_Rigidbody.AddForce(extraGravityForce);

		m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
	}


	void HandleGroundedMovement(bool crouch, bool jump)
	{
		// check whether conditions are right to allow a jump:
		if (jump && !crouch && ThisAnimator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
		{
			// jump!
			m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower, m_Rigidbody.velocity.z);
            Grounded = false;
			ThisAnimator.applyRootMotion = false;
			m_GroundCheckDistance = 0.1f;
		}
	}

	void ApplyExtraTurnRotation()
	{
		// help the character turn faster (this is in addition to root rotation in the animation)
		float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, Forward);
		transform.Rotate(0, Turn * turnSpeed * Time.deltaTime, 0);
	}


	public void OnAnimatorMove()
	{
		// we implement this function to override the default root motion.
		// this allows us to modify the positional speed before it's applied.
		if (Grounded && Time.deltaTime > 0)
		{
			Vector3 v = (ThisAnimator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

			// we preserve the existing y part of the current velocity.
			v.y = m_Rigidbody.velocity.y;
			m_Rigidbody.velocity = v;
		}
	}

	void CheckGroundStatus()
	{
		RaycastHit hitInfo;
		#if UNITY_EDITOR
		// helper to visualise the ground check ray in the scene view
		Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
		#endif
		// 0.1f is a small offset to start the ray from inside the character
		// it is also good to note that the transform position in the sample assets is at the base of the character
		if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
		{
			m_GroundNormal = hitInfo.normal;
            Grounded = true;
			ThisAnimator.applyRootMotion = true;
		}
		else
		{
            Grounded = false;
			m_GroundNormal = Vector3.up;
			ThisAnimator.applyRootMotion = false;
		}
	}



}
 