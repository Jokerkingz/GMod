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

	public GameObject vOrienter;
	public bool vIsRight;

	public string vPressCheck = "Checking";


	// Pointing System
	private Vector3 vPointSpot;
	public LayerMask vHitLayer;
	public Vector3 vLineRenderPoint;

	public LineRenderer cLR;


	// Update is called once per frame
	void Start () {
		//vTeleportTo = GameObject.FindGameObjectWithTag("TeleportHere");
		cLR = GetComponent<LineRenderer>();
	}

	void Update () {
		
		vPressCheck = "Checking";
		float tX;
		float tY;

		if (vIsRight){
			 tX = Input.GetAxis("Oculus_GearVR_RThumbstickX");
			 tY = Input.GetAxis("Oculus_GearVR_RThumbstickY");
			}
			else
			{
			 tX = Input.GetAxis("Oculus_GearVR_LThumbstickX");
			 tY = Input.GetAxis("Oculus_GearVR_LThumbstickY");
			}
		if (tX != 0 && tY != 0){
		/*
			if (vPressCheck == "Not Being Pressed")
				vPressCheck = "Start Pressing";
			else
				vPressCheck = "Being Pressed";
		*/
			//vPressCheck = "Being Pressed";
			vPointToThere();
			vPress = 2f;
			vActive = true;
			}
			/*
		else {
			if (vPressCheck == "Being Pressed")
				vPressCheck = "Just Stopped Pressing";
			else
				vPressCheck = "Not Being Pressed";
		}
		*/
		float tAngle = Mathf.Atan2(tX,tY)*180/Mathf.PI;
		float tAddition = transform.eulerAngles.y;
		vAngleToUse = tAngle+tAddition;
		/*
		if ((vIsRight && Input.GetButton("OGVR_RThumbPress")) || (!vIsRight && Input.GetButton("OGVR_LThumbPress"))){
			vPointToThere();
			vPress = 2f;
			vActive = true;
		}
		*/
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
				if (vTemp != null){
					if (Vector2.Distance(Vector2.zero,new Vector3(tX,tY)) > .5f)
						cOVRPC.vAngleOffSet = vAngleToUse;
						cOVRPC.gameObject.transform.position = vTemp.GetComponentInChildren<Scr_CheckBody>().vOpenSpot;
					if (Vector2.Distance(Vector2.zero,new Vector3(tX,tY)) > .5f)
						vOrienter.transform.localEulerAngles = new Vector3(0,vAngleToUse,0);
					}
				
			//GameObject.FindGameObjectWithTag("Orient").transform.localEulerAngles = new Vector3(0,vAngleToUse,0);
			//Debug.Log("Pork");
			Destroy(vTemp);


			}
		}
	}

	void vPointToThere(){
		Ray tRay;
		Vector3 tStartingPosition = transform.position;
		Vector3 tStartingDirection = transform.TransformDirection(Vector3.forward);
		//bool tHit = false;
		bool tDone = false;
		float tDistance = 0;
		int tIndex = 0;
		RaycastHit tHit;
		cLR.SetPosition(0,tStartingPosition);
			cLR.positionCount = 10;
		while (!tDone){
			tRay = new Ray(tStartingPosition,tStartingDirection);
			//tRay = new Ray(

			tIndex += 1;
			if (tIndex <= 9)
			tDistance = .7f+tIndex*.02f;
			else {
				tDistance = Mathf.Infinity;
			}

	
			if (Physics.Raycast(tRay,out tHit,tDistance,vObstacles)){
				if (tHit.collider.tag == "Floor"){
					if (vTemp.gameObject == null){
						vTemp = Instantiate(vObjectToCreate);
						vTemp.transform.position = tHit.point;
						tStartingPosition = tHit.point;
						cLR.positionCount = tIndex+1;
						tDone = true;
						}
					else{
						vTemp.transform.position = tHit.point;
						tStartingPosition = tHit.point;
						vTemp.GetComponentInChildren<Scr_CheckBody>().vRotAngle = vAngleToUse;
						cLR.positionCount = tIndex+1;
						tDone = true;
					}
				}
			} else{

				tStartingPosition = tRay.GetPoint(tDistance);
				tStartingDirection = new Vector3(tStartingDirection.x,tStartingDirection.y-(tIndex*.07f),tStartingDirection.z);

			}
			cLR.SetPosition(tIndex,tStartingPosition);
			//tRay.GetPoint(1);
			if (tIndex > 10)
				tDone = true;
		}



		/*
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
		*/
	}
	void vRaycastToThere(){
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


	// Update is called once per frame
	void WDnoeai () {
		Ray tRay;
		Vector3 tMySpot = transform.position;
		bool tDone = false;
		float tDistance = 1f;
		int tCount = 0;
			cLR.positionCount = 1;
		cLR.SetPosition(tCount,transform.position);
		while (!tDone) {
			tCount += 1;
			cLR.positionCount = tCount+1;
			vPointSpot = transform.TransformDirection(Vector3.forward*tDistance);
			tRay = new Ray (tMySpot,  vPointSpot);
			RaycastHit tHit;
			if (Physics.Raycast (tRay,out tHit, tDistance,vHitLayer)){
				//vLineRenderPoint = tHit.point;
				cLR.SetPosition(tCount,transform.position+tHit.point+(new Vector3(Random.value-.5f,Random.value-.5f,Random.value-.5f)*.25f));
				tDone = true;
				}
			cLR.SetPosition(tCount,transform.position+vPointSpot+(new Vector3(Random.value-.5f,Random.value-.5f,Random.value-.5f)*.25f));
			tDistance += 1f;
			if (tCount > 15)
				tDone = true;
			}
		}
}
