using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "MettleAI/Actions/EnGuarde")]
public class EnGuardeAction : Action {

	public override void Act(StateController controller){

		EnGuarde (controller);

	}

	private void EnGuarde(StateController controller){
		/*
		var rotation = Quaternion.LookRotation ((controller.Enemy.transform.position - controller.ThisAgent.transform.position));
		controller.ThisAnimator.SetFloat ("Rotation", controller.ThisAgent.transform.rotation.y);

		controller.ThisAgent.transform.rotation = Quaternion.Slerp (controller.ThisAgent.transform.rotation, rotation, 
				Time.deltaTime * controller.Stats.turnSpeed); 
		*/
		controller.ThisAgent.SetDestination (controller.GoTarget.position);
		controller.ThisAnimator.SetBool ("P_EnGuarde", true);
		controller.ThisAnimator.SetBool ("P_Maneuver", false);

	}

}
