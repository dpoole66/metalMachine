using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MettleAware : MonoBehaviour {

    public GameObject EnemyNPC;
    public GameObject GetEnemyNPC() {
        return EnemyNPC;
    }

    static Animator Player_Anim;
    private NavMeshAgent Player_Ctrl;


	// Use this for initialization
	void Start () {

        Player_Anim = GetComponent<Animator>();
        Player_Ctrl = GetComponent<NavMeshAgent>();

	}
	
	// Update is called once per frame
	void Update () {

        Player_Anim.SetFloat("Distance", Vector3.Distance(transform.position, 
            EnemyNPC.transform.position));

        if(Player_Ctrl.isStopped == true) {

            transform.LookAt(EnemyNPC.transform.position);

        }
        
		
	}
}
