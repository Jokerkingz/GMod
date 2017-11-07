using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Hands : MonoBehaviour {
	public GameObject vHeldObject;
	public bool vIsHolding;
	private Vector3 vCurrentVelocity;
	private Vector3 vPreviousVelocity;
	private Vector3 vPrevious2Velocity;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		vPrevious2Velocity = vPreviousVelocity;
		vPreviousVelocity = vCurrentVelocity;
		vCurrentVelocity = this.transform.position;

		if (Input.GetAxis("RTriggerMiddle") > 0)
			this.GetComponent<Scr_DebugShow>().ShowText(this.gameObject,"O                     ");
		//if (Input.GetAxis("RTriggerIndex") < 0)
		//	this.GetComponent<Scr_DebugShow>().ShowText(this.gameObject,"Less than 0");



		if (Input.GetButtonDown("Oculus_GearVR_RIndexTrigger")){
			this.GetComponent<Scr_DebugShow>().ShowText(this.gameObject,"I pressed it");
		}
		if (Input.GetKeyDown(KeyCode.Space))
			this.GetComponent<Scr_DebugShow>().ShowText(this.gameObject,"I pressed it");
		
		if (Input.GetAxis("Oculus_GearVR_RIndexTrigger") <= 0 && vHeldObject != null){
			
			Vector3 tVelocity = (vCurrentVelocity-vPrevious2Velocity)*100f;
			//tVelocity = new Vector3(0f,vCurrentVelocity.y-vPrevious2Velocity.y,0f);
			//vHeldObject.GetComponent<Scr_Socket>().LetGo(tVelocity); //////////////////////////////////////////////////////////////////////////////
			vHeldObject = null;
			vIsHolding = false;
			//this.GetComponent<Scr_DebugShow>().ShowText(this.gameObject,vCurrentVelocity.ToString());
			this.GetComponent<Scr_DebugShow>().ShowText(this.gameObject,tVelocity.ToString());
			}
	}

/*	{
		if (m_grabbedObj != null)
        {
			OVRPose localPose = new OVRPose { position = OVRInput.GetLocalControllerPosition(m_controller), orientation = OVRInput.GetLocalControllerRotation(m_controller) };
            OVRPose offsetPose = new OVRPose { position = m_anchorOffsetPosition, orientation = m_anchorOffsetRotation };
            localPose = localPose * offsetPose;

			OVRPose trackingSpace = transform.ToOVRPose() * localPose.Inverse();
			Vector3 linearVelocity = trackingSpace.orientation * OVRInput.GetLocalControllerVelocity(m_controller);
			Vector3 angularVelocity = trackingSpace.orientation * OVRInput.GetLocalControllerAngularVelocity(m_controller);

            GrabbableRelease(linearVelocity, angularVelocity);
        }

        // Re-enable grab volumes to allow overlap events
        GrabVolumeEnable(true);
    }
	}
	*/
	void OnTriggerStay(Collider tOther){
		if (tOther.tag == "SocketMale"){
		if (Input.GetAxis("RTriggerMiddle") > 0)
			{
			//this.GetComponent<Scr_DebugShow>().ShowText(this.gameObject,"I am grabbing it");
			vHeldObject = tOther.gameObject;
			vIsHolding = true;
			//tOther.GetComponent<Scr_Socket>().Grabbing(this.gameObject);
			}
		}
	}
}
