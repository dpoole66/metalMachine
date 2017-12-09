using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "MettleAI/Decisions/Idle")]
public class IdleDecision : Decision {

	public override bool Decide(StateController controller){

		bool IsStopped = Stopped (controller);
		return IsStopped;

	}

	private bool Stopped(StateController controller){

		if (controller.ThisAgent.remainingDistance < controller.ThisAgent.stoppingDistance) {

			controller.ThisAnimator.SetBool ("Idle", true);
			controller.ThisAnimator.SetBool ("Move", false);
			Debug.Log ("Stopping");
			return true;

		} else
			controller.ThisAnimator.SetBool ("Idle", false);
			controller.ThisAnimator.SetBool ("Move", true);
			Debug.Log ("Not Stopping");
			return false;
	}
}
