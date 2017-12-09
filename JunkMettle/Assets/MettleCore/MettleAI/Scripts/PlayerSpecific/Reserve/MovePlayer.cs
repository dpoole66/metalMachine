using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovePlayer : MonoBehaviour {
	public NavMeshAgent PlayerAgent;
	public Transform GoTarget;

	private Animator PlayerAnimator;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GoTarget != null) {

			PlayerAgent.SetDestination (GoTarget.position);
			PlayerAgent.Move (GoTarget.transform.position);

		}
	}
}
