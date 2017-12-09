using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "MettleAI/Decisions/Maneuver")]
public class ManeuverDecision : Decision {

	public override bool Decide(StateController controller){

		bool Maneuvering = InManeuvers (controller);
		return Maneuvering;

		}

	private bool  InManeuvers (StateController controller) {
		
		if (controller.GoTarget != null && controller.ThisAgent.remainingDistance > controller.ThisAgent.stoppingDistance) {

			controller.ThisAnimator.SetBool ("P_Maneuver", true);
			controller.ThisAnimator.SetBool ("P_EnGuarde", false);
			return true;
		
		} else {

			controller.ThisAnimator.SetBool ("P_Maneuver", false);
			controller.ThisAnimator.SetBool ("P_EnGuarde", true);
			return false;

		}
	}
}
