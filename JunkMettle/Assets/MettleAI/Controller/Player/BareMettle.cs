using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;


[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(CoreMettleMover))]


public class BareMettle : MonoBehaviour {

// Assemble Mettle components
public UnityEngine.AI.NavMeshAgent MettleAgent { get; private set; } 
public CoreMettleMover MettleChar { get; private set; } 
public Transform goTarget;

private Animator MettleAnimator;
private Transform MettleTransform;

public GameObject EnemyNPC;
public GameObject GetEnemyNPC() {
	return EnemyNPC;
}


bool Moveing ;
public float RotSpeed = 1.5f;

int IdleHash = Animator.StringToHash("Idle");
int MoveHash = Animator.StringToHash("Moveing");


private void Start() {   

    // get the components on the object we need ( should not be null due to require component so no need to check )
    MettleAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    MettleChar = GetComponent<CoreMettleMover>();
    MettleAnimator = GetComponent<Animator>();


    MettleAgent.updateRotation = true;
    MettleAgent.updatePosition = true;
}


private void Update() {
    if (goTarget != null)

        MettleAgent.SetDestination(goTarget.position);

	if (MettleAgent.remainingDistance > MettleAgent.stoppingDistance)
		
		MettleChar.Move (MettleAgent.desiredVelocity, false, false);


        else
            MettleChar.Move(Vector3.zero, false, false);


       if(MettleAgent.velocity.magnitude != 0.0f) {

			MettleAnimator.SetTrigger(MoveHash);
			MettleAnimator.SetTrigger(IdleHash);
			Moveing = true;
			MettleAnimator.SetBool(MoveHash, true);

        } else {
	
			Moveing = false;
			MettleAnimator.SetBool(MoveHash, false);
			var rotation = Quaternion.LookRotation (EnemyNPC.transform.position - transform.position);
				
			MettleAgent.transform.rotation = Quaternion.Slerp (MettleAgent.transform.rotation, rotation, Time.deltaTime * RotSpeed);

	            }

	}
}
