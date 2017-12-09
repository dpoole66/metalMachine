using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "MettleAI/Decisions/Look")]
public class LookDecision : Decision {

	public override bool Decide(StateController controller){

		bool targetVisible = Look (controller);
		return targetVisible;

	}

	private bool Look(StateController controller){

		RaycastHit hit;

		Debug.DrawRay (controller.ThisCam.position, controller.ThisCam.forward.normalized * controller.Stats.lookRange, Color.blue);

		if (Physics.SphereCast (controller.ThisCam.position, controller.Stats.lookSphereCastRadius, controller.ThisCam.forward, out hit, 
			controller.Stats.lookRange) && hit.collider.CompareTag ("Enemy")) {

			//controller.GoTarget = hit.transform;
			return true;

		} else {
			return false;
		}
	}
}
