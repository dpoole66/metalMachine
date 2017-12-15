using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.AI;


public class Attack : MonoBehaviour {


	private Animator thisAnimator;

	int AttackHash = Animator.StringToHash("Attacking");

	public void Start(){

		thisAnimator = GetComponentInChildren<Animator> ();

	}

	public void AttackNow (){

		thisAnimator.SetTrigger (AttackHash);

	}
}
