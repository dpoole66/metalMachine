using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController2 : MonoBehaviour {

	public State currentState;
	public Stats Stats;
	public Transform ThisCam;
	public State remainState;
	public GameObject  Enemy;
	public Transform GoTarget;

	// Movement for Animator
	[HideInInspector] public float m_Forward;
	[HideInInspector] public float m_Turn;
	[HideInInspector] public Animator ThisAnimator;
	[HideInInspector] public NavMeshAgent  ThisAgent;
	[HideInInspector] public CharacterController ThisCharacter;

	int ForwardHash = Animator.StringToHash("Forward");
	int TurnHash = Animator.StringToHash("Turn");

	void Awake () {

		ThisAgent = GetComponent<NavMeshAgent> ();
		ThisAnimator = GetComponent<Animator> ();
		ThisCharacter = GetComponent<CharacterController> ();

	}

	void Update(){
		// Commented out below to stop errors with this backup file
		//currentState.UpdateState (this);
		float Forward = ThisAgent.velocity.z * Stats.moveSpeed;
		float Turn = ThisAgent.velocity.x  * Stats.turnSpeed;
		ThisAnimator.SetFloat (ForwardHash, Forward );
		ThisAnimator.SetFloat (TurnHash, Turn);

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
	/*
	public OnTriggerStay(Collider other){

		if(other.CompareTag ("Enemy")){
			
			ThisAnimator.SetBool("InRange" = true);

		}
	}
*/
}
