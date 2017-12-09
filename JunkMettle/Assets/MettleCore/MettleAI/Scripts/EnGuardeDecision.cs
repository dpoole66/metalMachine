using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "MettleAI/Decisions/EnGuarde")]
public class EnGuardeDecision : Decision {

	public override bool Decide(StateController controller){

		bool enGuarde = EnGuarde (controller);
		return enGuarde;

	}

	private bool EnGuarde(StateController controller){

		if(controller.ThisAgent.transform.position.z <= controller.Stats.enGuardeRange && controller.Enemy.CompareTag ("Enemy")){

			controller.ThisAnimator.SetBool ("P_EnGuarde", true);
			controller.ThisAnimator.SetBool ("P_Maneuver", false);
			Debug.Log ("EnGuarde");
			return true;

		}	else{
			
		//	controller.ThisAnimator.SetBool ("InRange", false);
			controller.ThisAnimator.SetBool ("P_EnGuarde", false);
			controller.ThisAnimator.SetBool ("P_Maneuver", true);
			return false;

		}

	}

}
