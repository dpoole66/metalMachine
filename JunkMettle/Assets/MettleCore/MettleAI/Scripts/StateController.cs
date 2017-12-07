using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour {

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

	void Awake () {

		ThisAgent = GetComponent<NavMeshAgent> ();
		ThisAnimator = GetComponent<Animator> ();
		ThisCharacter = GetComponent<CharacterController> ();
		
	}

	void Update(){

		currentState.UpdateState (this);

}

	void OnDrawGizmos(){

		if (currentState != null && ThisCam != null) {

			Gizmos.color = currentState.sceneGizmoColor;
			Gizmos.DrawWireSphere (ThisCam.position, Stats.lookSphereCastRadius);

		}

	}

	public void TransitionToState(State nextState){

		if (nextState != remainState) {

			currentState = nextState;

		}

	}

}
 