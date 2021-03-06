using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[CreateAssetMenu (menuName = "MettleAI/Decisions/Range")]
public class RangeDecision : Decision {

	public override bool Decide(StateController controller){

		bool targetInRange = inRange (controller);
		return targetInRange;

	}

	private bool inRange(StateController controller){

		var range = Vector3.Distance (controller.transform.position, controller.Enemy.transform.position);

		if (range <= controller.Stats.lookRange) {

			controller.ThisAnimator.SetBool ("InRange", true);
			return true;

		} else {

			controller.ThisAnimator.SetBool ("InRange", false);
			return false;


		}

	}

}
