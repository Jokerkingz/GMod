using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PointToMove : MonoBehaviour {
	public GameObject vObjectToCreate;
	public GameObject vTemp;
	public LayerMask vObstacles;

	public Vector3 vAngleChoice;
	public GameObject vTeleportTo;
	public float vAngleToUse;

	public OVRPlayerController cOVRPC;
	private float vPress = 0f;
	private bool vActive = false;
	// Update is called once per frame
	void Start () {
		//vTeleportTo = GameObject.FindGameObjectWithTag("TeleportHere");
	}

	void FixedUpdate () {
		float tX = Input.GetAxis("Oculus_GearVR_RThumbstickX");
		float tY = Input.GetAxis("Oculus_GearVR_RThumbstickY");
		float tAngle = Mathf.Atan2(tX,tY)*180/Mathf.PI;
		float tAddition = transform.eulerAngles.y;
		vAngleToUse = tAngle+tAddition;

		if (Input.GetButton("OGVR_RThumbPress")){
			vPointToThere();
			vPress = 2f;
			vActive = true;
		}
		if (vPress > 0f)
			vPress -= .5f;
		else{
			vPress = 0f;
			if (vActive){
				vActive = false;
		//if (Input.GetButtonUp("OGVR_RThumbPress")){
			if (vAngleToUse > 360f)
					vAngleToUse -= 360f;
			if (vAngleToUse < 0f)
				vAngleToUse += 360f;
				cOVRPC.vAngleOffSet = vAngleToUse;
			cOVRPC.gameObject.transform.position = vTemp.GetComponentInChildren<Scr_CheckBody>().vOpenSpot;
			GameObject.FindGameObjectWithTag("Orient").transform.localEulerAngles = new Vector3(0,vAngleToUse,0);
			Debug.Log("Pork");
			Destroy(vTemp);


			}
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
					vTemp.GetComponentInChildren<Scr_CheckBody>().vRotAngle = vAngleToUse;
				}
			}
		}

	}


}
