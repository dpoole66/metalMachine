using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "MettleAI/Decisions/Look")]
public class LookDecision : Decision {

	public override bool Decide(EnemyStateController controller){

		bool targetVisible = Look (controller);
		return targetVisible;

	}

	private bool Look(EnemyStateController controller){

		RaycastHit hit;

		Debug.DrawRay (controller.EnemyCam.position, controller.EnemyCam.forward.normalized * controller.enemyStats.lookRange, Color.blue);

		if (Physics.SphereCast (controller.EnemyCam.position, controller.enemyStats.lookSphereCastRadius, controller.EnemyCam.forward, out hit, 
			controller.enemyStats.lookRange) && hit.collider.CompareTag ("Enemy")) {

			controller.playerTarget = hit.transform;
			return true;

		} else {
			return false;
		}
	}
}
