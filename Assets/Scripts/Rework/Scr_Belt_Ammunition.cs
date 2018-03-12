using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Belt_Ammunition : MonoBehaviour {
	public GameObject[] vPresentMagazines;
	public string[] vNameOfMagazine;
	public Vector3[] vPresentPosition;
	public Vector3[] vPresentEuler;
	public Vector3[] vPresentScale;

	public string vCreationStatus;
	public float vCreationCoolDown;
	public GameObject vNextCreation;
	public int vMaxCreation = 3;

	private Scr_Belt_Holsters cBH;
	private bool vShowing;
	void Start(){
		vNameOfMagazine = new string[3];
		vPresentScale = new Vector3[3];

		// new
		vPresentMagazines = new GameObject[3];
		cBH = this.GetComponent<Scr_Belt_Holsters>();
	}
	void Update(){
		switch(cBH.vStatus){
		case "Nothing":
			if (cBH.vHoloRate > 0 ){
				if (vShowing){
					vShowing = false;
					foreach (GameObject tObj in vPresentMagazines){
						if (tObj != null){ tObj.GetComponent<Renderer>().enabled = false; }
						}
					}
				}
			else {
				if (!vShowing){
					vShowing = true;
					foreach (GameObject tObj in vPresentMagazines){
						if (tObj != null){ tObj.GetComponent<Renderer>().enabled = true; }
						}
					}
				}
			break;
		case "Holding":
			if (vShowing){
				vShowing = false;
				foreach (GameObject tObj in vPresentMagazines){
					if (tObj != null){ tObj.GetComponent<Renderer>().enabled = false; }
					}
				}
		break;
		}
	}

	public void fReceiveMagazines(GameObject tHandle){
		Scr_Mod_Magazine[] tMagazineList = tHandle.GetComponentsInChildren<Scr_Mod_Magazine>();
		int tCount = 0;
		GameObject tObj;
		Destroy(vPresentMagazines[0]);
		Destroy(vPresentMagazines[1]);
		Destroy(vPresentMagazines[2]);
		vPresentMagazines = new GameObject[3];
		vShowing = false;
		foreach (Scr_Mod_Magazine tMagazine in tMagazineList){
			Debug.Log("Found a Mag");
			tObj = tMagazine.vMagazineToPop;
			vNameOfMagazine[tCount] = tMagazine.GetComponent<Scr_ModSaverPart>().vPartType;
			vPresentMagazines[tCount] = Instantiate(tObj);
			vPresentMagazines[tCount].GetComponent<Renderer>().enabled = false;
			Vector3 tVect = tMagazine.transform.localEulerAngles;
			tMagazine.transform.localEulerAngles = new Vector3(0,90,90);
			vPresentMagazines[tCount].transform.localScale = tObj.transform.lossyScale;
			tMagazine.transform.localEulerAngles = tVect;
			vPresentMagazines[tCount].transform.SetParent(this.transform);
			vPresentMagazines[tCount].transform.localPosition = new Vector3(0,+.2f-(.2f*tCount),0);
			vPresentMagazines[tCount].transform.localEulerAngles = Vector3.zero;

			vPresentScale[tCount] = tObj.transform.lossyScale;
			tCount += 1;
			if (tCount >= 3)
				break;
		}
		vMaxCreation = tCount;

	}

	public void fRequestReload(){
		


	}
}
