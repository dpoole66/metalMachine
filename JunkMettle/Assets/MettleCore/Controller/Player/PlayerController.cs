using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

	private Animator PlayerAnimator = null;

	// Use this for initialization
	void Awake () {

		PlayerAnimator = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {

		float Horizontal = CrossPlatformInputManager.GetAxis ("Horizontal");
		float Vertical = CrossPlatformInputManager.GetAxis ("Vertical");

		PlayerAnimator.SetFloat ("Horizontal", Horizontal, 0.1f, Time.deltaTime);
		PlayerAnimator.SetFloat ("Vertical", Vertical, 0.1f, Time.deltaTime);

	}
}
