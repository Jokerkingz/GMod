using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PointToMove : MonoBehaviour {
	public GameObject vChosenSpot;
	public GameObject vObjectToCreate;
	public GameObject vTemp;
	public LayerMask vObstacles;

	public Vector3 vAngleChoice;
	public GameObject vTeleportTo;


	public float vAngle;
	// Update is called once per frame
	void Start () {
		vTeleportTo = GameObject.FindGameObjectWithTag("TeleportHere");
	}

	void FixedUpdate () {
		float tX = Input.GetAxis("Oculus_GearVR_RThumbstickX");
		float tY = Input.GetAxis("Oculus_GearVR_RThumbstickY");
		float tAngle = Mathf.Atan2(tX,tY)*180/Mathf.PI;
		float tAddition = transform.eulerAngles.y;
		vAngle = tAddition;
		//vAngleChoice = new Vector3(Input.GetAxis("Oculus_GearVR_RThumbstickX"),0,Input.GetAxis("Oculus_GearVR_RThumbstickY"));
		//vAngleChoice = transform.TransformDirection(vAngleChoice);
		//vAngle = Mathf.Atan2(Mathf.Cos(Input.GetAxis("Oculus_GearVR_RThumbstickX")),Mathf.Sin(Input.GetAxis("Oculus_GearVR_RThumbstickY")));
		//vTeleportTo.transform.LookAt(vTeleportTo.transform.position + vAngleChoice);

		vTeleportTo.transform.eulerAngles = new Vector3(0,tAngle+tAddition,0);
		//vTeleportTo.transform.eulerAngles(new Vector3(0,Mathf.Rad2Deg*Mathf.Atan2(Mathf.Cos(Input.GetAxis("Oculus_GearVR_RThumbstickX")),Mathf.Sin(Input.GetAxis("Oculus_GearVR_RThumbstickY"))),0));



		if (Input.GetButton("OGVR_RThumbPress")){
			vPointToThere();
		}
		if (OVRInput.GetUp(OVRInput.Button.SecondaryThumbstickUp)){
		}
	}
	void vPointToThere(){
		Ray tRay = new Ray(transform.position,transform.TransformDirection(Vector3.forward));
		RaycastHit tHit;
		if (Physics.Raycast(tRay,out tHit,10f,vObstacles)){
			if (tHit.collider.tag == "Floor"){
				if (vTemp.gameObject == null){
					vTemp = Instantiate(vObjectToCreate);
					vTemp.transform.position = tHit.point;
					}
				else{
					vTemp.transform.position = tHit.point;
					//vChosenSpot.transform.position = vTemp.GetComponentInChildren <Scr_CheckBody>().CheckFreeSpot();
				}
			}
		}

	}
}
