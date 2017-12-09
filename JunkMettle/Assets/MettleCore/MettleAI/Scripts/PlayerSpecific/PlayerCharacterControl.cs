using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof (StateController))]
public class PlayerCharacterControl : MonoBehaviour{
	
	public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
	public StateController character { get; private set; } // the character we are controlling
	public Transform Target;    // target to aim for


	private void Start()
	{
		// get the components on the object we need ( should not be null due to require component so no need to check )
		agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
		character = GetComponent<StateController>();

		agent.updateRotation = false;
		agent.updatePosition = true;
	}


	private void Update()
	{
			if (Target != null)
				agent.SetDestination(Target.position);

		if (agent.remainingDistance > agent.stoppingDistance)
			character.Move(agent.desiredVelocity, false, false);
		else
			character.Move(Vector3.zero, false, false);
	}


	public void SetTarget(Transform target)
	{
			this.Target = target;
	}
}