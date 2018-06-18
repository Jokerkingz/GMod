using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	private string[] vModType = new string[]{"Handle","Base","Barrel","Magazine","Module","Accessory"};//,"Extension","Sword","Shield"};
	public int vModTypIndex;
	private string[] vHandleType = new string[]{"Simple"};
	public int vHandleTypIndex;
	private string[] vBaseType = new string[]{"Simple","Cylinder","Magazine","Battery"};
	public int vBaseTypIndex;
	private string[] vBarrelType = new string[]{"Simple","Rifle","Rail","Plasma","Curve"};
	public int vBarrelTypIndex;
	private string[] vMagazineType = new string[]{"Simple","Pellet"};
	public int vMagazineTypIndex;
	private string[] vModuleType = new string[]{"Rotator"};
	public int vModuleTypIndex;
	private string[] vAccessoryType = new string[]{"Scope","Sword"};
	public int vAccessoryTypIndex;
	public string vSubType = "A"; //"B"
	public float vCoolDown;
	public GameObject vSpawnSpot;
	//public Text vMiddle;
	//public Text vBrand;
	public float vCD;
	public string vCurrentChoice;

	private Scr_System_SourceList cGE;
	[Header("Display")]
	public Sprite[] vSpriteList;
	public Image vIconSource;
	public Text vTxtCategory;
	public Text vTxtSubategory;
	public Text vTxtType;

	// Use this for initialization
	void Start () {
		vVectScale = new Vector3(.5f,vFloatUse,1);
		vDisplay.transform.localScale = vVectScale;
		cGE = GameObject.FindGameObjectWithTag("GameController").GetComponent<Scr_System_SourceList>();
	}
	
	// Update is called once per frame
	void Update () {
		fUpdateDisplay();
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
					fCreateNewHolo();

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
	void fUpdateDisplay(){
		vIconSource.sprite = vSpriteList[vModTypIndex];
		vTxtCategory.text = vModType[vModTypIndex];
		vTxtSubategory.text = fGetType();
		vTxtType.text = "Type " +  vSubType;
	}
	public void fHandPressed(){
		vStatus = "PopUp";
		vVectScale = new Vector3(.5f,0,1);
		vFloatUse = 0f;

	}
	// Category Change // Category Change // Category Change // Category Change // Category Change // Category Change // Category Change // Category Change
	public void fPreviousCategory(){
		if (vStatus != "Selection")
			return;
		vModTypIndex --;
		if (vModTypIndex < 0 )
			vModTypIndex = vModType.Length-1;
		vSubType = "A";
		fCreateNewHolo();
	}
	public void fNextCategory(){
		if (vStatus != "Selection")
			return;
		vModTypIndex ++;
		if (vModTypIndex >= vModType.Length)
			vModTypIndex = 0;
		vSubType = "A";
		fCreateNewHolo();
	}
	// Type Change // Type Change // Type Change // Type Change // Type Change // Type Change // Type Change // Type Change // Type Change // Type Change // Type Change // Type Change 
	public void fNextType(){
		if (vStatus != "Selection")
			return;
		switch (vModType[vModTypIndex]){
		case "Handle":
			vHandleTypIndex ++;
			if (vHandleTypIndex >= vHandleType.Length)
				vHandleTypIndex = 0;
			break;
		case "Base":
			vBaseTypIndex ++;
			if (vBaseTypIndex >= vBaseType.Length)
				vBaseTypIndex = 0;
			break;
		case "Barrel":
			vBarrelTypIndex ++;
			if (vBarrelTypIndex >= vBarrelType.Length)
				vBarrelTypIndex = 0;
			break;
		case "Magazine":
			vMagazineTypIndex ++;
			if (vMagazineTypIndex >= vMagazineType.Length)
				vMagazineTypIndex = 0;
			break;
		case "Module":
			vModuleTypIndex ++;
			if (vModuleTypIndex >= vModuleType.Length)
				vModuleTypIndex = 0;
			break;
		case "Accessory":
			vAccessoryTypIndex ++;
			if (vAccessoryTypIndex >= vAccessoryType.Length)
				vAccessoryTypIndex = 0;
			break;


			//"Handle","Base","Barrel","Magazine","Module"
		}
		vSubType = "A";
		fCreateNewHolo();
	}
	public void fPreviousType(){
		if (vStatus != "Selection")
			return;
		switch (vModType[vModTypIndex]){
		case "Handle":
			vHandleTypIndex --;
			if (vHandleTypIndex < 0)
				vHandleTypIndex = vHandleType.Length-1;
			break;
		case "Base":
			vBaseTypIndex --;
			if (vBaseTypIndex < 0)
				vBaseTypIndex = vBaseType.Length-1;
			break;
		case "Barrel":
			vBarrelTypIndex --;
			if (vBarrelTypIndex < 0)
				vBarrelTypIndex = vBarrelType.Length-1;
			break;
		case "Magazine":
			vMagazineTypIndex --;
			if (vMagazineTypIndex < 0)
				vMagazineTypIndex = vMagazineType.Length-1;
			break;
		case "Module":
			vModuleTypIndex --;
			if (vModuleTypIndex < 0)
				vModuleTypIndex = vModuleType.Length-1;
			break;
		case "Accessory":
			vAccessoryTypIndex --;
			if (vAccessoryTypIndex < 0)
				vAccessoryTypIndex = vAccessoryType.Length-1;
			break;


			//"Handle","Base","Barrel","Magazine","Module"
		}
		vSubType = "A";
		fCreateNewHolo();
	}
	// Print Pressed // Print Pressed // Print Pressed // Print Pressed // Print Pressed // Print Pressed // Print Pressed // Print Pressed // Print Pressed // Print Pressed // Print Pressed
	public void fPrintPressed(){
		if (vStatus != "Selection")
			return;
		vStatus = "Print";
		Destroy(vHologramObj);
		vHologramObj = null;
		vCD = 0;
		//string tNewName;
		GameObject vPrefab = cGE.fGetPrefab(vCurrentChoice);
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

		string tTemp = fGetType();
		tTemp = vModType[vModTypIndex]+"_"+tTemp+"_"+vSubType;
		GameObject tPrefab = cGE.fGetPrefab(tTemp);
		if (tPrefab == null){
			tPrefab = cGE.fGetPrefab(vCurrentChoice);
			}
		else
			vCurrentChoice = tTemp;
		if (tPrefab != null){
			vAngle = 0;
			vHologramObj = Instantiate(tPrefab);
			vHologramObj.transform.position = this.transform.position;
			vHologramObj.transform.eulerAngles = Vector3.zero;

			// Turn Off Scripts
			OVRGrabbable[] tOVRGrabbableList = vHologramObj.GetComponentsInChildren<OVRGrabbable>();
			foreach (OVRGrabbable tOVRGrabbable in tOVRGrabbableList)
				Destroy(tOVRGrabbable);
			Scr_Male_Socket[] tMaleSocketList = vHologramObj.GetComponentsInChildren<Scr_Male_Socket>();
			foreach (Scr_Male_Socket tMaleSocket in tMaleSocketList)
				tMaleSocket.enabled = false;
			Scr_Female_Socket[] tFemaleSocketList = vHologramObj.GetComponentsInChildren<Scr_Female_Socket>();
			foreach (Scr_Female_Socket tFemaleSocket in tFemaleSocketList)
				tFemaleSocket.enabled = false;
			Scr_ModSystem_Handler[] tModHandlerList = vHologramObj.GetComponentsInChildren<Scr_ModSystem_Handler>();
			foreach (Scr_ModSystem_Handler tModHandler in tModHandlerList)
				tModHandler.enabled = false;
			Collider[] tModColliderList = vHologramObj.GetComponentsInChildren<Collider>();
			foreach (Collider tModCollider in tModColliderList)
				tModCollider.enabled = false;

				Renderer[] tListA =  vHologramObj.GetComponentsInChildren <Renderer>();
				foreach (Renderer tR in tListA){
					Material[] tNew = new Material[tR.materials.Length];
					for(int i = 0; i < tR.materials.Length;i++)
						tNew[i] = vHoloMaterial;
					tR.materials = tNew;
				}
			}

	}

	string fGetType(){
		string tTemp = "";
		switch (vModType[vModTypIndex]){
		case "Handle": tTemp = vHandleType[vHandleTypIndex]; break;
		case "Base": tTemp = vBaseType[vBaseTypIndex]; break;
		case "Barrel": tTemp = vBarrelType[vBarrelTypIndex]; break;
		case "Magazine": tTemp = vMagazineType[vMagazineTypIndex]; break;
		case "Module": tTemp = vModuleType[vModuleTypIndex]; break;
		case "Accessory": tTemp = vAccessoryType[vAccessoryTypIndex]; break;
		}
		return tTemp;
	}
}