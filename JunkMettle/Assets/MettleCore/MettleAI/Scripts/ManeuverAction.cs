using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName = "MettleAI/Actions/Maneuver")]
public class ManeuverAction : Action {

	public override void Act(StateController controller){

		Maneuver (controller);

	}

	private void Maneuver(StateController controller){

		if (controller.GoTarget != null) {

			controller.ThisAgent.SetDestination (controller.GoTarget.position);

		}

		if (controller.ThisAgent.remainingDistance > controller.ThisAgent.stoppingDistance) {

			//controller.ThisAgent.Move (controller.ThisAgent.transform.forward * Time.deltaTime );
			controller.ThisAgent.isStopped = false;
			controller.ThisAnimator.SetBool ("Moveing", true);
			controller.ThisAnimator.SetBool ("P_Maneuver", true);

		} else {

			controller.ThisAgent.isStopped = true;
			controller.ThisAnimator.SetBool ("Moveing", false);
			controller.ThisAnimator.SetBool ("P_Maneuver", false);

		}
	}

}
	