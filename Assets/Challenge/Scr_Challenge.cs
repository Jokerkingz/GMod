using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Challenge : MonoBehaviour {
	public int vChallengeStep;
	public string[] vPartsList;
	public List<Scr_Data_Parts> vPartsCheck = new List<Scr_Data_Parts>();

	public string[] vPartName;
	public GameObject[] vPartObj;

	public GameObject vTargetSource;
	public GameObject vObstacleSource;
	public List<GameObject> vObjToClear = new List<GameObject>();
	public Text vTextSource;
	public float vYlimit;
	public GameObject vTempObj;

	public Scr_System_SourceList cGE;
	public GameObject vHolsterA;
	public GameObject vHolsterB;
	private Vector3 vPos;

	public Scr_PointToMove[] vPTM;// = new Scr_PointToMove[2];
	// Use this for initialization
	void Start () {
		GameObject tPlayer = GameObject.FindGameObjectWithTag("GameController");
		cGE = tPlayer.GetComponent<Scr_System_SourceList>();
		vHolsterA.SetActive(false);
		vHolsterB.SetActive(false);
		vPos = this.transform.position;
		foreach (Scr_PointToMove tPTM in vPTM) {
			tPTM.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	if (vChallengeStep <= 0)
		return;
		//foreach (Scr_Data_Parts tPart in vPartsCheck){
		//	fPartCheck(tPart);
		//}
		fArrayCheck();
		int tCount = GameObject.FindGameObjectsWithTag("Target").Length;
		if (tCount <= 0){
			vChallengeStep ++;
			fNextChallenge();
			}
	}
	public void fNextChallenge(){
		for (int i = 0; i < vPartObj.Length; i++) {
			Destroy(vPartObj[i].gameObject);
		}
		foreach (GameObject tObjCle in vObjToClear) {
			Destroy(tObjCle);
		}
		vObjToClear = new List<GameObject>();
		GameObject tObj;
			string[] tTemp;
		switch (vChallengeStep){
		case 1:
			
			tObj = Instantiate(vTargetSource);
			tObj.transform.position = new Vector3(0,3,8);

			tTemp = new string[]{"Handle_Simple_A","Base_Magazine_A","Barrel_Simple_A"};
			fSpawnArray(tTemp);
			vTextSource.text = "Basics : \n Place your hand over parts to identify them. \n Some parts have a built-in ammunition.";
		break;
		case 2:
			tObj = Instantiate(vTargetSource);
			tObj.transform.position = new Vector3(4,3,8);
			tObj = Instantiate(vTargetSource);
			tObj.transform.position = new Vector3(-4,5,8);

			tTemp = new string[]{"Handle_Simple_A","Base_Simple_B","Barrel_Simple_A","Magazine_Pellet_A","Magazine_Simple_A"};
			fSpawnArray(tTemp);
			vTextSource.text = "Offset : \n Some parts may have weird designs and limits.";
			break;
		case 3:
			tObj = Instantiate(vTargetSource);
			tObj.transform.position = new Vector3(3,4,8);
			tObj.AddComponent<Scr_Target_Lerp>().vEndPoint = new Vector3(-3,4,8);
			//tObj = Instantiate(vObstacleSource);
			//tObj.transform.position = new Vector3(0,4,4);
			tTemp = new string[]{"Handle_Simple_A","Base_Battery_A","Barrel_Plasma_A","Barrel_Simple_A"};
			fSpawnArray(tTemp);
			vTextSource.text = "Batteries : \n Battery powered parts are colored blue \n Battery meters are found around battery sources.";
			//fSpawnParts(tTemp);
			break;
		case 4:
			tObj = Instantiate(vTargetSource);
			tObj.transform.position = new Vector3(0,3,20);

			tTemp = new string[]{"Handle_Simple_A","Base_Battery_A","Barrel_Rail_A","Magazine_Simple_A","Base_Magazine_A","Magazine_Pellet_A"};
			fSpawnArray(tTemp);
			vTextSource.text = "Rail Gun : \n Rail guns require batteries and ammunition \n Railguns boost projectile speed with magnets to cause more damage";
			//fSpawnParts(tTemp);
		break;
		case 5:
			for (int i = 0; i < 10; i++) {
				tObj = Instantiate(vTargetSource);
				tObj.transform.position = new Vector3(-10+(i*2),5,10);
			}

			tTemp = new string[]{"Module_Rotator_A","Handle_Simple_A","Base_Simple_A","Base_Magazine_A","Base_Magazine_A","Base_Magazine_A","Base_Magazine_A","Base_Magazine_A","Base_Cylinder_A","Barrel_Simple_A","Barrel_Simple_A","Barrel_Simple_A","Barrel_Simple_A","Magazine_Simple_A"};
			fSpawnArray(tTemp);
			vTextSource.text = "Motor Modules : \n Motor modules spin parts connected to it. \n Holding the trigger button activates motors when connected.";
			//fSpawnParts(tTemp);
			break;
		case 6:
			tObj = Instantiate(vTargetSource);
			tObj.transform.position = new Vector3(0,5,10);
			tObj = Instantiate(vObstacleSource);
			tObj.transform.position = new Vector3(0f,5.5f,4f);
			vObjToClear.Add(tObj);

			tTemp = new string[]{"Handle_Simple_A","Base_Simple_A","Magazine_Pellet_A","Barrel_Curve_A"};
			fSpawnArray(tTemp);
			vTextSource.text = "Curving barrels : \n You must swing your gun to curve the bullets on the given direction.";
			//fSpawnParts(tTemp);
		break;

		case 7:
			tObj = Instantiate(vTargetSource);
			tObj.transform.position = new Vector3(3,4,10);
			tObj.AddComponent<Scr_Target_Lerp>().vEndPoint = new Vector3(-3,4,10);
			tObj = Instantiate(vTargetSource);
			tObj.transform.position = new Vector3(-3,3,10);
			tObj.AddComponent<Scr_Target_Lerp>().vEndPoint = new Vector3(3,3,10);
			tObj = Instantiate(vTargetSource);
			tObj.transform.position = new Vector3(4,6,10);
			tObj.AddComponent<Scr_Target_Lerp>().vEndPoint = new Vector3(-3,2,10);
			tObj = Instantiate(vTargetSource);
			tObj.transform.position = new Vector3(-4,2,10);
			tObj.AddComponent<Scr_Target_Lerp>().vEndPoint = new Vector3(3,6,10);

			tTemp = new string[]{"Handle_Simple_A","Handle_Simple_A","Base_Barrel_A","Base_Battery_A","Barrel_Simple_A","Barrel_Plasma_A"};
			fSpawnArray(tTemp);
			vTextSource.text = "Dual : \n You can create and use two seperate guns. \n The holsters are not activated during this course.";
			//fSpawnParts(tTemp);
			break;
		case 8:
			for (int i = 0; i < 5; i++) {
				tObj = Instantiate(vTargetSource);
				tObj.transform.position = new Vector3(-5+(i*2),5,10);
			}

			for (int i = 0; i < 5; i++) {
				tObj = Instantiate(vTargetSource);
				tObj.transform.position = new Vector3(-5+(i*2),4,10);
			}

			tTemp = new string[]{"Handle_Simple_A","Handle_Simple_A","Base_Simple_A","Base_Cylinder_A","Base_Battery_A","Magazine_Simple_A","Magazine_Pellet_A","Barrel_Simple_A","Barrel_Curve_A","Barrel_Rail_A","Magazine_Plasma_A","Module_Rotator_A"};
			fSpawnArray(tTemp);
			vTextSource.text = "Mixing things Together : \n Having many parts together can create a unique gun \n Be creative with what you have";
			//fSpawnParts(tTemp);
			break;
		case 9:
			tObj = Instantiate(vTargetSource);
			tObj.transform.position = new Vector3(0,-10,0);


			//tTemp = new string[]{"Handle_Simple_A","Base_Simple_A","Base_Cylinder_A","Base_Magazine_A","Base_Battery_A","Magazine_Simple_A","Magazine_Pellet_A","Barrel_Simple_A","Barrel_Curve_A","Barrel_Rail_A","Magazine_Plasma_A","Module_Rotator_A"};
			//fSpawnArray(tTemp);
			tTemp = new string[]{};
			fSpawnArray(tTemp);
			vTextSource.text = "Thank you for trying the showcase. \n Teleporters are available behind you.";
			//fSpawnParts(tTemp);
		break;

		}

	}
	void fSpawnArray(string[] vSpawnList){
		vPartName = new string[vSpawnList.Length];
		vPartObj = new GameObject[vSpawnList.Length];
		int tIndex = 0;
		int tSign =1;
		foreach (string tPart in vSpawnList) {
			vPartName[tIndex] = vSpawnList[tIndex];
			vPartObj[tIndex] = Instantiate(cGE.fGetPrefab(vSpawnList[tIndex]));
			Vector3 tSpot = this.transform.position;
			tSpot.x += (tIndex*tSign*.1f);
			vPartObj[tIndex].transform.position = tSpot;
			Scr_FabricationCollective tTemp = vPartObj[tIndex].AddComponent<Scr_FabricationCollective>();
			tTemp.Start();
			tTemp.vLockSpot = tSpot;
			tIndex ++;
			tSign *= -1;
		}
	}

	void fArrayCheck(){
		int tSign =1;
		for (int i = 0; i < vPartObj.Length; i++) {
			if (vPartObj[i]  == null){
				vPartObj[i]  = Instantiate(cGE.fGetPrefab(vPartName[i]));
				Vector3 tSpot = this.transform.position;
				tSpot.x += (i*tSign*.1f);
				vPartObj[i].transform.position = tSpot;
				Scr_FabricationCollective tTemp = vPartObj[i].AddComponent<Scr_FabricationCollective>();
				tTemp.Start();
				tTemp.vLockSpot = tSpot;


				}
			else if (vPartObj[i].transform.position.y < vYlimit){
				if (!vPartObj[i].GetComponent<OVRGrabbable>().vIsBeingGripped){
					Destroy(vPartObj[i]);
					vPartObj[i] = null;
				}
			}
			tSign *= -1;
		}

	}
}
