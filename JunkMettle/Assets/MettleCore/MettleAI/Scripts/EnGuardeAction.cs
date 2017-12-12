using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "MettleAI/Actions/EnGuarde")]
public class EnGuardeAction : Action {

	public override void Act(StateController controller){

		EnGuarde (controller);

	}

	private void EnGuarde(StateController controller){
        //Debug.Log("EnGuarde");
        // var rotation = Quaternion.LookRotation ((controller.Enemy.transform.position - controller.ThisAgent.transform.position));

        //When stopped, rototate toward Enemy
        //if(controller.ThisAgent.remainingDistance <= controller.ThisAgent.stoppingDistance && !controller.ThisAgent.pathPending ){

        //Vector3 relativePos = controller.Enemy.transform.position - controller.ThisAgent.transform.position;
		controller.ThisAgent.SetDestination(controller.ThisAgent.destination);
           // controller.ThisAgent.transform.rotation = Quaternion.Slerp(controller.ThisAgent.transform.rotation, rotation,
           // Time.deltaTime * controller.Stats.turnSpeed);

      //  }       else{

            //Keep moving
            //controller.ThisAgent.SetDestination(controller.GoTarget.position);
           // Debug.Log(controller.Enemy.transform.position);

        
       
	}

}
