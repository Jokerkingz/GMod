using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Scr_Tutorial_Table : MonoBehaviour {
	public string vStatus = "SecondStep";
	public GameObject vHandle;
	public Vector3 vHandleSpawnPosition;
	public GameObject vBase;
	public Vector3 vBaseSpawnPosition;
	public GameObject vBarrel;
	public Vector3 vBarrelSpawnPosition;
	public GameObject vMagazine;
	public Vector3 vMagazineSpawnPosition;
	private float vPrintCD;
	public float vYlimit;

	// Image Control
	public int vImageIndex;
	public Sprite[] vSpriteList;
	public Scr_System_SourceList cGE;
	public Image vImageShowA;
	public Image vImageShowB;
	public Text vTextSource;

	// Print Button Control
	public Vector3 vVectScale;
	private float vFloatUse;
	public GameObject vPrintButton;

	// Holster Check
	public Scr_Belt_Holsters vHolsterSourceA;
	public Scr_Belt_Holsters vHolsterSourceB;
	// Target to spawn
	public GameObject vTarget;
	public GameObject vTargetToSpawn;
	public Vector3 vTargetSpawnPoint;

	public GameObject vNextFloor;

	public Scr_PointToMove[] vPTM;// = new Scr_PointToMove[2];
	public GameObject vBarrelSource;
	void Start () {
		GameObject tPlayer = GameObject.FindGameObjectWithTag("GameController");
		cGE = tPlayer.GetComponent<Scr_System_SourceList>();
		vVectScale = new Vector3(.5f,vFloatUse,1);
		vPrintButton.transform.localScale = vVectScale;
		//Scr_PointToMove[] vPTM = tPlayer.GetComponentsInChildren<Scr_PointToMove>();
		foreach (Scr_PointToMove tPTM in vPTM) {
			tPTM.enabled = false;
		}
		vTextSource.text = "Place one hand on top of the table for the hand scanner";
	}
	
	// Update is called once per frame
	void Update () {
		switch (vStatus){
		case "Idle":
			
			break;
		case "FirstWait": // Show a print button
			vFloatUse += 6f*Time.deltaTime;
			if (vFloatUse > 1f){
				vFloatUse = 1f;
				vStatus = "FirstStep";
				vImageIndex ++;
				vImageShowA.sprite = vSpriteList[vImageIndex];
				vImageShowB.sprite = vSpriteList[vImageIndex];
				vTextSource.text = "Extend your index finger and touch the print button";
				}
			vVectScale = new Vector3(.5f,vFloatUse,1);
			vPrintButton.transform.localScale = vVectScale;
		break;
		case "FirstStep": // Show a print button
			
		break;
		case "SecondWait":
			vFloatUse -= 6f*Time.deltaTime;
			if (vFloatUse < 0f){
				vFloatUse = 0f;
				vStatus = "SecondStep";
				vImageIndex ++;
				vImageShowA.sprite = vSpriteList[vImageIndex];
				vImageShowB.sprite = vSpriteList[vImageIndex];
				vTextSource.text = "Use your middle finger to grip and hold a gun part";
				}
			vVectScale = new Vector3(.5f,vFloatUse,1);
			vPrintButton.transform.localScale = vVectScale;
		break;
		case "SecondStep": // print one handle until the player grabs it
			fHandleCheck();
			if (vHandle != null)
				if (vHandle.GetComponent<OVRGrabbable>().vIsBeingGripped){
					vImageIndex ++;
					vImageShowA.sprite = vSpriteList[vImageIndex];
				vImageShowB.sprite = vSpriteList[vImageIndex];
				vTextSource.text = "Connect gun parts using their sockets. Yellow male sockets connects to blue female sockets";
					vStatus = "ThirdStep";
					}
		break;
		case "ThirdStep": // print one handle until the player grabs it
			fHandleCheck();
			fBaseCheck();
			if (vHandle != null)
				if (vHandle.GetComponentInChildren<Scr_Female_Socket>().vConnectedObject != null){
					vImageIndex ++;
					vImageShowA.sprite = vSpriteList[vImageIndex];
				vImageShowB.sprite = vSpriteList[vImageIndex];
				vTextSource.text = "Connect all parts together. Yellow male sockets connects to blue female sockets";
					vStatus = "FourthStep";
					}
		break;
		case "FourthStep": // Print all have target
			fHandleCheck();
			fBarrelCheck();
			fBaseCheck();
			fMagazineCheck();
			int tCount = 0;
			Scr_Male_Socket[] tSocketList;
			if (vHandle != null){
				tSocketList = vHandle.GetComponentsInChildren<Scr_Male_Socket>();
				foreach (Scr_Male_Socket tMS in tSocketList){
					if (tMS.vConnectedTo != null)
						tCount ++;
					}
				}
			if (tCount >= 3){
				vTarget = Instantiate(vTargetToSpawn);
				vTarget.transform.position = vTargetSpawnPoint;
				Scr_FabricationCollective tFC = vTarget.AddComponent<Scr_FabricationCollective>();
				tFC.Start();
				tFC.vIsLocked = false;
				vImageIndex ++;
				vImageShowA.sprite = vSpriteList[vImageIndex];
				vImageShowB.sprite = vSpriteList[vImageIndex];
				vTextSource.text = "Press on the index finger to shoot your gun";
				vStatus = "FifthStep";
			}
		break;
		case "FifthStep":
			fHandleCheck();
			fBarrelCheck();
			fBaseCheck();
			fMagazineCheck();
			if (vTarget == null){
				vImageIndex ++;
				vImageShowA.sprite = vSpriteList[vImageIndex];
				vImageShowB.sprite = vSpriteList[vImageIndex];
				vTextSource.text = "Place handles on your holsters on the side to keep your current gun build";
				vStatus = "SixthStep";
				}

			break;

		case "SixthStep":
			fHandleCheck();
			fBarrelCheck();
			fBaseCheck();
			fMagazineCheck();
			if (vHolsterSourceA.vSavedHandle != null || vHolsterSourceB.vSavedHandle != null){
				vImageIndex ++;
				vImageShowA.sprite = vSpriteList[vImageIndex];
				vImageShowB.sprite = vSpriteList[vImageIndex];
				vTextSource.text = "Use the analog stick to rotate and move. \n The arrow indicates where you will be facing once you let go of the analog stick";
					foreach (Scr_PointToMove tPTM in vPTM) {
						tPTM.enabled = true;
				}
				vStatus = "SeventhStep";
				vTarget = Instantiate(vNextFloor);
				vTarget.transform.position = Vector3.zero;
				Scr_FabricationCollective tFC = vTarget.AddComponent<Scr_FabricationCollective>();
				tFC.Start();
				tFC.vIsLocked = false;
				tFC.vMaxMeter = 4f;
			}
				

		break;
		case "SeventhStep":
			fHandleCheck();
			fBarrelCheck();
			fBaseCheck();
			fMagazineCheck();

		break;
		}
	}
	void fHandleCheck(){
		if (vHandle == null){
			GameObject vPrefab = cGE.fGetPrefab("Handle_Simple_A");
			if (vPrefab != null){
				vPrefab = Instantiate(vPrefab);
				vPrefab.transform.position = vHandleSpawnPosition;
				Scr_FabricationCollective tTemp = vPrefab.AddComponent<Scr_FabricationCollective>();
				tTemp.Start();
				tTemp.vLockSpot = vHandleSpawnPosition;
				vHandle = vPrefab;
				}
			return;
		}
		if (vHandle.transform.position.y < vYlimit){
			if (!vHandle.GetComponent<OVRGrabbable>().vIsBeingGripped){
				Destroy(vHandle);
				vHandle = null;
				}
			}
	}

	void fBaseCheck(){
		if (vBase == null){
			GameObject vPrefab = cGE.fGetPrefab("Base_Simple_A");
			if (vPrefab != null){
				vPrefab = Instantiate(vPrefab);
				vPrefab.transform.position = vBaseSpawnPosition;
				Scr_FabricationCollective tTemp = vPrefab.AddComponent<Scr_FabricationCollective>();
				tTemp.Start();
				tTemp.vLockSpot = vBaseSpawnPosition;
				vBase = vPrefab;
				}
			return;
		}
		if (vBase.transform.position.y < vYlimit){
			if (!vBase.GetComponent<OVRGrabbable>().vIsBeingGripped){
				Destroy(vBase);
				vBase = null;
				}
			}
	}

	void fBarrelCheck(){
		if (vBarrel == null){
			GameObject vPrefab = cGE.fGetPrefab("Barrel_Simple_A");
			if (vPrefab != null){
				vPrefab = Instantiate(vPrefab);
				vPrefab.transform.position = vBarrelSpawnPosition;
				Scr_FabricationCollective tTemp = vPrefab.AddComponent<Scr_FabricationCollective>();
				tTemp.Start();
				tTemp.vLockSpot = vBarrelSpawnPosition;
				vBarrel = vPrefab;
				}
			return;
		}
		if (vBarrel.transform.position.y < vYlimit){
			if (!vBarrel.GetComponent<OVRGrabbable>().vIsBeingGripped){
				Destroy(vBarrel);
				vBarrel = null;
				}
			}
	}

	void fMagazineCheck(){
		if (vMagazine == null){
			GameObject vPrefab = cGE.fGetPrefab("Magazine_Simple_A");
			if (vPrefab != null){
				vPrefab = Instantiate(vPrefab);
				vPrefab.transform.position = vMagazineSpawnPosition;
				Scr_FabricationCollective tTemp = vPrefab.AddComponent<Scr_FabricationCollective>();
				tTemp.Start();
				tTemp.vLockSpot = vMagazineSpawnPosition;
				vMagazine = vPrefab;
				}
			return;
		}
		if (vMagazine.transform.position.y < vYlimit){
			if (!vMagazine.GetComponent<OVRGrabbable>().vIsBeingGripped){
				Destroy(vMagazine);
				vMagazine = null;
				}
			}
	}
	public void fHandPressed(){
		if (vStatus == "Idle"){
			vStatus = "FirstWait";
			}

	}
	public void fPrintPressed(){
		if (vStatus == "FirstStep"){
			vStatus = "SecondWait";
			}
	}
}
