using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseFSM : StateMachineBehaviour {

	public GameObject Player;
	public GameObject Enemy;
	public float speed = 2.0f;
	public float rotSpeed = 360.0f;
	public float enGuardRange = 2.0f;
	public float stamina = 100.0f;
	public float health = 100.0f;


	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int  layerIndex){

		Player = animator.gameObject;
		Enemy = Player.GetComponent<PlayerAI> ().GetEnemy ();

	}
}

