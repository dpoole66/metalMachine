using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateController : MonoBehaviour {

	public State currentState;
	public EnemyStats enemyStats;
	public Transform EnemyCam;
	public State remainState;

	[HideInInspector] public NavMeshAgent  Enemy_Agent;
	[HideInInspector] public int nextWayPoint;
	[HideInInspector] public Transform playerTarget;

	void Awake () {

		Enemy_Agent = GetComponent<NavMeshAgent> ();
		
	}

	void Update(){

		currentState.UpdateState (this);

}

	void OnDrawGizmos(){

		if (currentState != null && EnemyCam != null) {

			Gizmos.color = currentState.sceneGizmoColor;
			Gizmos.DrawWireSphere (EnemyCam.position, enemyStats.lookSphereCastRadius);

		}

	}

	public void TransitionToState(State nextState){

		if (nextState != remainState) {

			currentState = nextState;

		}

	}

}
