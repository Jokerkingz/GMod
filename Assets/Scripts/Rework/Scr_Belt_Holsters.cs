using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Belt_Holsters : MonoBehaviour {
	public string vStatus;
	public GameObject vSavedHandle;
	public string vSavedGun;
	public float vHoloRate;
	public GameObject vHologramObj;
	public GameObject vHologramSource;
	public Material vHoloMaterial;

	// Update is called once per frame
	void Update () {
		switch (vStatus){
			case "Nothing":
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
			case "Holding":
			vSavedHandle.transform.position = this.transform.position;
			vSavedHandle.transform.eulerAngles = this.transform.eulerAngles;

			break;
			case "Magazine":


			break;
			default:
			vStatus = "Nothing";
			vSavedHandle = null;
			vSavedGun = null;

			break;

		}
	}

	public void fReceiveHandle(GameObject tHandle){
		if (vStatus == "Nothing"){
			Destroy(vHologramObj.gameObject);
			vHologramSource = null;
			vHologramObj = null;
			Rigidbody tHR = tHandle.GetComponent<Rigidbody>();
			tHR.isKinematic = true;
			tHR.useGravity = false;


			vStatus = "Holding";
			vSavedHandle = tHandle;
			vSavedGun = tHandle.GetComponent<Scr_ModSaverMain>().fInitialize(tHandle.gameObject);

			vSavedHandle.transform.position = this.transform.position;
			vSavedHandle.transform.eulerAngles = this.transform.eulerAngles;

			tHandle.GetComponent<Scr_ModSystem_Handler>().vHolsterConnectedTo = this.gameObject;
			//this.GetComponent<Scr_Belt_Ammunition>().fReceiveMagazines(tHandle.gameObject);

			Scr_ModSaverSocket[] tSocketList = tHandle.GetComponent<Scr_ModSaverPart>().vModSaverSocketList;
				foreach (Scr_ModSaverSocket tSocket in tSocketList) {
					Scr_Female_Socket tFemaleSocket = tSocket.GetComponent<Scr_Female_Socket>();
					if (tFemaleSocket.vConnectedObject != null){
						GameObject tObj = tFemaleSocket.vConnectedObject ;
						tFemaleSocket.vConnectedObject.GetComponent<Scr_Male_Socket>().Detach(tFemaleSocket.vConnectedObject);
						Destroy(tObj);
					}
					tFemaleSocket.vConnectedObject = null;
					tSocket.vConnection = null;
				}
				/*
			Renderer[] tListA =  vHologramObj.GetComponentsInChildren <Renderer>();
			foreach (Renderer tR in tListA){
				Material[] tNew = new Material[tR.materials.Length];
				for(int i = 0; i < tR.materials.Length;i++)
					tNew[i] = vHoloMaterial;
				tR.materials = tNew;
				}
			*/
			tHandle.GetComponent<Scr_ModHandle>().fUpdateList();
			}
	}
	public void fRemoveHandle(GameObject tHandle){
		if (vStatus == "Holding" ){
			vStatus = "";
			Scr_Female_Socket tFem = tHandle.GetComponentInChildren<Scr_Female_Socket>();
			Rigidbody tHR = tHandle.GetComponent<Rigidbody>();
			tHR.isKinematic = false;
			tHR.useGravity = true;
			vSavedHandle.GetComponent<Scr_ModLoadMain>().fConvert(vSavedGun);
			vSavedGun = ""; 
			tHandle.GetComponent<Scr_ModSystem_Handler>().vHolsterConnectedTo = null;
			tHandle.GetComponent<Scr_ModHandle>().fUpdateList();


			//Scr_FabricationCollective tTemp = tFem.gameObject.AddComponent<Scr_FabricationCollective>();
			//tTemp.vIsLocked = false;
			//tTemp.vDontChange = true;
			//tTemp.Start();
			}


		//vStatus = "Holding";
		//vSavedHandle = tHandle;
		//vSavedGun = tHandle.GetComponent<Scr_ModSaverMain>().fInitialize(tHandle.gameObject);
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
				//vHologramObj.BroadcastMessage("TurnOff","Hollow");
				vHologramObj.transform.SetParent(this.transform);
				vHologramObj.transform.position = this.transform.position;
				vHologramObj.transform.eulerAngles = this.transform.eulerAngles;

				tTransList = new Transform[0];
				tTransList = vHologramObj.GetComponentsInChildren<Transform>();
				//foreach (Transform tObjects in tTransList)
				//	tObjects.tag = "Hollow";
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

	/*
	void OnTriggerEnter(Collider tOther){
if (tOther.tag == "GripPart"){
	Scr_ModSystem_Handler tMSH = tOther.transform.root.GetComponent<Scr_ModSystem_Handler>();
		if (tMSH != null)
				tMSH.vHolsterConnectedTo = this.gameObject;
			}
	}

	void OnTriggerExit(Collider tOther){
		if (tOther.tag == "GripPart"){
		Scr_ModSystem_Handler tMSH = tOther.transform.root.GetComponent<Scr_ModSystem_Handler>();
		if (tMSH != null)
			tMSH.vHolsterConnectedTo = null;
			}
	}
	*/
}
