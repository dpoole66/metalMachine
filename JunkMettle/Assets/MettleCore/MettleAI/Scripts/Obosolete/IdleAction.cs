using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "MettleAI/Actions/Idle")]
public class IdleAction : Action {

	public override void Act(StateController controller){

		Idle (controller);

	}

	private void Idle(StateController controller){

		controller.ThisAgent.SetDestination (controller.ThisAgent.transform.position);
		//controller.ThisAgent.isStopped = true;

		}

}
