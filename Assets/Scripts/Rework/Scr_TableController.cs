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
	public GameObject vHologramObj;
	public GameObject vHologramSource;
	public Material vHoloMaterial;
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
				vFloatUse += .3f*Time.deltaTime;
				if (vFloatUse >= .5f){
					vFloatUse = .5f;
					vStatus = "Selection";
				}
				vVectScale = new Vector3(.5f,vFloatUse,1);
				vDisplay.transform.localScale = vVectScale;
			break;
		case "Selection":
				if (vHoloRate <= 0){
					if (vHologramObj != null){
						Destroy(vHologramObj.gameObject);
						vHologramSource = null;
						vHologramObj = null;
					}
				}
				else{
					vHoloRate -= .2f;
					if (vHoloRate <= 0){
							if (vHologramObj != null){
								Destroy(vHologramObj.gameObject);
								vHologramSource = null;
								vHologramObj = null;
							}

						return;
						}
					if (vHologramObj != null){
							vHologramObj.transform.position = this.transform.position;
							vHologramObj.transform.eulerAngles = this.transform.eulerAngles;
							}
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
	public void fHandPressed(){
		vStatus = "PopUp";
		vVectScale = new Vector3(.5f,0,1);
		vFloatUse = 0f;

	}
	public void fNextPressed(){
		vStatus = "PopUp";
		vVectScale = new Vector3(.5f,0,1);
		vFloatUse = 0f;

	}

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
}
