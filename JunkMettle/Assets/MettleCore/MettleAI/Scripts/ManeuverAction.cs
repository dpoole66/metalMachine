using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName = "MettleAI/Actions/Maneuver")]
public class ManeuverAction : Action {

	public override void Act(StateController controller){

		Maneuver (controller);

	}

	private void Maneuver(StateController controller){
		
		controller.ThisAgent.SetDestination (controller.GoTarget.position);

	}
}
	