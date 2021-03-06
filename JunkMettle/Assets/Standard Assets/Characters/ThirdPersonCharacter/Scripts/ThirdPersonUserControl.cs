using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
		private ThirdPersonCharacter playerCharacter; // A reference to the ThirdPersonCharacter on the object
      //  private Transform m_Cam;                  // A reference to the main camera in the scenes transform
      //  private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
		private NavMeshAgent playerAgent;
		private GameObject enemyTarget;
        
        private void Start()	{

            /*
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }
			*/
            // Get the Enemy by her tag
            enemyTarget = GameObject.FindGameObjectWithTag("Enemy");

            // get the third person character ( this should never be null due to require component )
			playerCharacter = GetComponent<ThirdPersonCharacter>();
			playerAgent = GetComponent <NavMeshAgent> ();
        }


        private void Update() {

            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);

			//m_Move = v*Vector3.forward + h*Vector3.right;
			/*
            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v*m_CamForward + h*m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v*Vector3.forward + h*Vector3.right;

            }
			*/

			//if (m_Cam !=null && playerAgent.isStopped == false) {
			if (h == 0.0f && v == 0.0f) {
				Debug.Log ("Stopped");
				m_Move = v*Vector3.forward + h*Vector3.right;
				//Rotate to Enemy
				var direction = enemyTarget.transform.position - playerAgent.transform.position;
				playerAgent.transform.rotation = Quaternion.Slerp(playerAgent.transform.rotation, Quaternion.LookRotation (direction), 6.0f * Time.deltaTime);

				// calculate camera relative direction to move:
				//m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
				//m_Move = v*m_CamForward + h*m_Cam.right;

			} else  {

				Debug.Log ("Moveing");
				// we use world-relative directions in the case of no main camera
				m_Move = v*Vector3.forward + h*Vector3.right;

			}

				

#if !MOBILE_INPUT
			// walk speed multiplier
	        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

            // pass all parameters to the character control script
			playerCharacter.Move(m_Move, crouch, m_Jump);
            m_Jump = false;
        }

		public void SetTarget(GameObject target)
		{
			this.enemyTarget = target;
		}
    }

}

