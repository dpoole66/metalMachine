using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "MettleAI/Actions/Idle")]
public class IdleAction : Action {

	public override void Act(EnemyStateController controller){

	}

	private void Patrol(EnemyStateController controller){

		controller.Enemy_Agent.isStopped = true;

	}


}
