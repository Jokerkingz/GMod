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
		cLR.enabled = false;
		if (tX != 0 && tY != 0 && (Vector2.Distance(Vector2.zero,new Vector3(tX,tY)) > .05f)){
			cLR.enabled = true;
			vPress = 2f;
			vPointToThere();
			vActive = true;
			}
		float tAngle = Mathf.Atan2(tX,tY)*180/Mathf.PI;
		float tAddition = transform.eulerAngles.y;
		if (Vector2.Distance(Vector2.zero,new Vector3(tX,tY)) > .5f)
			vAngleToUse = tAngle+tAddition;
		if (vPress > 0f)
			vPress -= .5f;
		else{
			vPress = 0f;
			if (vActive){
				vActive = false;
				if (vAngleToUse > 360f)
						vAngleToUse -= 360f;
				if (vAngleToUse < 0f)
						vAngleToUse += 360f;
				if (vTemp != null){
					cOVRPC.vAngleOffSet = vAngleToUse;
					//if (vTemp.GetComponentInChildren<Scr_CheckBody>().vHasASpot)
					Vector3 tSpot = vTemp.GetComponentInChildren<Scr_CheckBody>().vOpenSpot;
					if (tSpot != Vector3.zero)
						cOVRPC.gameObject.transform.position = tSpot;
					vOrienter.transform.localEulerAngles = new Vector3(0,vAngleToUse,0);
					}
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
			tIndex += 1;
			cLR.positionCount = tIndex+1;
			if (tIndex <= 9)
				tDistance = .5f+tIndex*.01f;
			else {
				tDistance = 10f;//Mathf.Infinity;
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
				if (vTemp.gameObject == null){
					vTemp = Instantiate(vObjectToCreate);
					vTemp.transform.position = tStartingPosition;
					tStartingPosition = tHit.point;
					}
				else{
					vTemp.transform.position = tHit.point;
					tStartingPosition = tStartingPosition;}
				tStartingDirection = new Vector3(tStartingDirection.x,tStartingDirection.y-(tIndex*.07f),tStartingDirection.z);
		
			}
			cLR.SetPosition(tIndex,tStartingPosition);
			//tRay.GetPoint(1);
			if (tIndex > 10)
				tDone = true;
		}
		}
}
