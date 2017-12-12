using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour {

	public State currentState;
	public State remainState;
	public Stats Stats;
	public Transform ThisCam;
	public Transform  Enemy;

	[HideInInspector] public Animator ThisAnimator;
	[HideInInspector] public NavMeshAgent  ThisAgent;



	[HideInInspector] public CharacterController ThisCharacter;

	void Awake () {

		ThisAgent = GetComponent<NavMeshAgent> ();
		ThisAnimator = GetComponent<Animator> ();
		Enemy = GetComponent<Transform>();
		ThisCharacter = GetComponent<CharacterController> ();

	}

	void Update(){
		// Commented out below to stop errors with this backup file
		currentState.UpdateState (this);

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

}
