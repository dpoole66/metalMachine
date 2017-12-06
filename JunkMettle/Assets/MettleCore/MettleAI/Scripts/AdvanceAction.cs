using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "MettleAI/Actions/Chase")]
public class AdvanceAction : Action {

	public override void Act(EnemyStateController controller){

		Advance (controller);

	}

	private void Advance(EnemyStateController controller){

		controller.Enemy_Agent.destination = controller.playerTarget.position;
		controller.Enemy_Agent.isStopped = false;
	}


}
