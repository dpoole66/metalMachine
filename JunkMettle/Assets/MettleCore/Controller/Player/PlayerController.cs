using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Runtime.InteropServices;
// ## 12, 13, 2017 changes based on scottpetrovic.com unity3d 3rd person basic movement/rotation

// ## This will automatically add a character controller to the stack if it has been removed or a new char is being built.
[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour {

	private Animator PlayerAnimator = null;
	// ##
	private CharacterController PlayerCharController = null;

	// ##
	private Vector3 moveDirection = Vector3.zero;
	private Vector3 forward = Vector3.zero;
	private Vector3 right = Vector3.zero;

	// ## Editor value inputs
	public float movementSpeed = 2.0f;
	public float rotateSpeed = 90.0f;

	// Use this for initialization
	void Awake () {

		PlayerAnimator = GetComponent<Animator> ();
		// ##
		PlayerCharController = GetComponent<CharacterController> ();

	}
	
	// Update is called once per frame
	void Update () {
		// ##
		forward = transform.forward;
		right = new Vector3(forward.z, 0, -forward.x);
		// ## GetAxis is switched to Raw to remove smothing that GetAxis applies
		float Horizontal = CrossPlatformInputManager.GetAxisRaw ("Horizontal");
		float Vertical = CrossPlatformInputManager.GetAxisRaw ("Vertical");

		PlayerAnimator.SetFloat ("Horizontal", Horizontal, 0.1f, Time.deltaTime);
		PlayerAnimator.SetFloat ("Vertical", Vertical, 0.1f, Time.deltaTime);

		// ##
		var targetDirection = Horizontal * right  + Vertical * forward;

		moveDirection = Vector3.RotateTowards (moveDirection, targetDirection, rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);

		var movement = moveDirection * Time.deltaTime * movementSpeed;
		PlayerCharController.Move (movement);

		// ## Final rotation applied
		if (targetDirection != Vector3.zero) {
			transform.rotation = Quaternion.LookRotation (moveDirection);
		}

	}
		
}
