using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TableController : MonoBehaviour {
	public string vStatus;
	public GameObject vHandSource;
	public GameObject vDisplay;
	public Vector3 vVectScale;
	private float vFloatUse;

	public float vAngle;
	public float vHoloRate;
	public GameObject vHologramObj;
	public GameObject vHologramSource;
	public Material vHoloMaterial;

	// Print Data

	public string[] vModType = new string[]{"Handle","Base","Barrel","Extension","Sword","Shield"};
	public int vModTypIndex;
	public string vSubType = "A"; //"B"
	public float vCoolDown;
	public GameObject vSpawnSpot;
	//public Text vMiddle;
	//public Text vBrand;
	public float vCD;
	public string vCurrentChoice;
	// Use this for initialization
	void Start () {

				vVectScale = new Vector3(.5f,vFloatUse,1);
				vDisplay.transform.localScale = vVectScale;
	}
	
	// Update is called once per frame
	void Update () {
		switch (vStatus){
			case "Idle":
			break;
			case "PopUp":
				vFloatUse += .6f*Time.deltaTime;
				if (vFloatUse >= .5f){
					vFloatUse = .5f;
					vStatus = "Selection";
				fCreateNewHolo();
				}
				vVectScale = new Vector3(.5f,vFloatUse,1);
				vDisplay.transform.localScale = vVectScale;
			break;
		case "Selection":
			vAngle += 25*Time.deltaTime;
			if (vHologramObj != null){
				vHologramObj.transform.position = this.transform.position;
				vHologramObj.transform.eulerAngles = new Vector3(0,vAngle,0);
				}
			break;
			case "Print":
				vCD	+= Time.deltaTime;
				if (vCD > 1f)
					{vStatus = "Selection";

					}
			break;
			case "PopIn":
				vFloatUse -= .3f*Time.deltaTime;
				if (vFloatUse < 0f){
					vFloatUse = 0f;
					vStatus = "Idle";
				}
				vVectScale = new Vector3(.5f,vFloatUse,1);
				vDisplay.transform.localScale = vVectScale;
			break;
			default:
				vStatus = "Idle";
			break;
			}
	}
	public void fHandPressed(){
		vStatus = "PopUp";
		vVectScale = new Vector3(.5f,0,1);
		vFloatUse = 0f;

	}
	public void fPreviousPressed(){
		if (vStatus != "Selection")
			return;
		//vStatus = "PopUp";
		//vVectScale = new Vector3(.5f,0,1);
		//vFloatUse = 0f;
		vModTypIndex --;
		if (vModTypIndex < 0 )
			vModTypIndex = vModType.Length-1;
		vSubType = "A";
	}
	public void fNextPressed(){
		if (vStatus != "Selection")
			return;
		//vStatus = "PopUp";
		//vVectScale = new Vector3(.5f,0,1);
		//vFloatUse = 0f;

		vModTypIndex ++;
		if (vModTypIndex >= vModType.Length)
			vModTypIndex = 0;
		vSubType = "A";
		fCreateNewHolo();
	}
	public void fPrintPressed(){
		if (vStatus != "Selection")
			return;
		vStatus = "Print";
		Destroy(vHologramObj);
		vHologramObj = null;
		vCD = 0;
		GameObject vPrefab = Resources.Load(vCurrentChoice) as GameObject;
		if (vPrefab != null){
			vPrefab = Instantiate(vPrefab) ;
			vPrefab.transform.position = this.transform.position;
			Scr_FabricationCollective tTemp = vPrefab.AddComponent<Scr_FabricationCollective>();
			tTemp.vLockSpot = transform.position;
			}
	}
	void fCreateNewHolo(){
		Destroy(vHologramObj);
		vHologramObj = null;
		string tTemp = "Pre_Mod_"+vModType[vModTypIndex]+"_"+vSubType;
		vCurrentChoice = tTemp;
		GameObject tPrefab = Resources.Load(tTemp) as GameObject;
		if (tPrefab != null){
			vAngle = 0;
			vHologramObj = Instantiate(tPrefab);
			vHologramObj.transform.position = this.transform.position;
			vHologramObj.transform.eulerAngles = Vector3.zero;

			// Turn Off Scripts
			OVRGrabbable[] tOVRGrabbableList = vHologramObj.GetComponentsInChildren<OVRGrabbable>();
			foreach (OVRGrabbable tOVRGrabbable in tOVRGrabbableList)
				//Destroy(tOVRGrabbable);
			tOVRGrabbable.enabled = false;
			Scr_Male_Socket[] tMaleSocketList = vHologramObj.GetComponentsInChildren<Scr_Male_Socket>();
			foreach (Scr_Male_Socket tMaleSocket in tMaleSocketList)
				tMaleSocket.enabled = false;
			Scr_Female_Socket[] tFemaleSocketList = vHologramObj.GetComponentsInChildren<Scr_Female_Socket>();
			foreach (Scr_Female_Socket tFemaleSocket in tFemaleSocketList)
				tFemaleSocket.enabled = false;
			Scr_ModSystem_Handler[] tModHandlerList = vHologramObj.GetComponentsInChildren<Scr_ModSystem_Handler>();
			foreach (Scr_ModSystem_Handler tModHandler in tModHandlerList)
				tModHandler.enabled = false;

				Renderer[] tListA =  vHologramObj.GetComponentsInChildren <Renderer>();
				foreach (Renderer tR in tListA){
					Material[] tNew = new Material[tR.materials.Length];
					for(int i = 0; i < tR.materials.Length;i++)
						tNew[i] = vHoloMaterial;
					tR.materials = tNew;
				}
			}

	}
	/*
	public void ShowHollogram(GameObject tReference){
		if (vStatus == "Nothing"){
			vHoloRate = 1f;
			if (vHologramObj == null && vHologramSource == null && GameObject.FindGameObjectsWithTag("Hollow").Length <= 0){
					List<GameObject> tColliderList = new List<GameObject>();
					Transform[] tTransList = tReference.GetComponentsInChildren<Transform>();
					foreach (Transform tObjects in tTransList){
						if (tObjects.tag == "GripPart")
							tColliderList.Add(tObjects.gameObject);
					}
			vHologramSource = tReference;

			// hologram creation
				vHologramObj = Instantiate(tReference) as GameObject;
				vHologramObj.BroadcastMessage("TurnOff","Hollow");
				vHologramObj.transform.SetParent(this.transform);
				vHologramObj.transform.position = this.transform.position;
				vHologramObj.transform.eulerAngles = this.transform.eulerAngles;

				tTransList = new Transform[0];
				tTransList = vHologramObj.GetComponentsInChildren<Transform>();
				foreach (Transform tObjects in tTransList)
					tObjects.tag = "Hollow";
				// Turn Off Scripts
				OVRGrabbable[] tOVRGrabbableList = vHologramObj.GetComponentsInChildren<OVRGrabbable>();
				foreach (OVRGrabbable tOVRGrabbable in tOVRGrabbableList)
					//Destroy(tOVRGrabbable);
				tOVRGrabbable.enabled = false;
				Scr_Male_Socket[] tMaleSocketList = vHologramObj.GetComponentsInChildren<Scr_Male_Socket>();
				foreach (Scr_Male_Socket tMaleSocket in tMaleSocketList)
					tMaleSocket.enabled = false;
				Scr_Female_Socket[] tFemaleSocketList = vHologramObj.GetComponentsInChildren<Scr_Female_Socket>();
				foreach (Scr_Female_Socket tFemaleSocket in tFemaleSocketList)
					tFemaleSocket.enabled = false;
				Scr_ModSystem_Handler[] tModHandlerList = vHologramObj.GetComponentsInChildren<Scr_ModSystem_Handler>();
				foreach (Scr_ModSystem_Handler tModHandler in tModHandlerList)
					tModHandler.enabled = false;

				Renderer[] tListA =  vHologramObj.GetComponentsInChildren <Renderer>();
				foreach (Renderer tR in tListA){
					Material[] tNew = new Material[tR.materials.Length];
					for(int i = 0; i < tR.materials.Length;i++)
						tNew[i] = vHoloMaterial;
					tR.materials = tNew;
				}
			}
		}
	}


			case "Print":
				cAS.PlayOneShot(vSFXPrint,.5f);
				tTemp = "Pre_Mod_"+vModType[vModTypIndex]+"_"+vSubType;
				Debug.Log(tTemp);
				vPrefab = Resources.Load(tTemp) as GameObject;
				if (vPrefab != null){
					vPrefab = Instantiate(vPrefab) ;
					vPrefab.transform.position = vSpawnSpot.transform.position;
					}
				break;
			}
			tTemp = "Pre_Mod_"+vModType[vModTypIndex]+"_"+vSubType;
			vPrefab = Resources.Load(tTemp) as GameObject;
			if (vPrefab == null)
				vSubType = "A";
			vMiddle.text = vModType[vModTypIndex]+ " " + vSubType;
			}
			*/
}
