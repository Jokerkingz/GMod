using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Female_Socket : MonoBehaviour {
	private GameObject vHologramObj;
	private GameObject vHologramSource;
	public GameObject vConnectedObject;

	[Header("Hologram Setting")]
	public GameObject vPseudoPart;
	//public GameObject vOriginalPart;
	public Material vMatGood;
	public Material vMatBad;
	public float vHoloRate;
	public float vCollisionRate;

	private Vector3 vHollowAngle;
	public string vPartType;

	[Header("Audio Source")]
	private AudioSource cAS;
	public AudioClip vSFX;
	// Use this for initialization
	void Start () {
		cAS = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (vHoloRate <= 0){
			if (vHologramObj != null){
				Destroy(vHologramObj.gameObject);
				vHologramSource = null;
				vHologramObj = null;
			}
		}
	else{
		vHoloRate -= .5f;
		vCollisionRate -= .2f;
		if (vHoloRate <= 0){
				if (vHologramObj != null){
					Destroy(vHologramObj.gameObject);
					vHologramSource = null;
					vHologramObj = null;
				}

			return;
			}
		Material tMat = vMatGood;
		if (vHologramObj != null){
				vHollowAngle = Reorientate(vHologramSource);
				vHologramObj.transform.localEulerAngles = vHollowAngle;
				if (vCollisionRate > 0f)
					tMat = vMatBad;
				Renderer[] tListA =  vHologramObj.GetComponentsInChildren <Renderer>();
				foreach (Renderer tR in tListA){
					Material[] tNew = new Material[tR.materials.Length];
					for(int i = 0; i < tR.materials.Length;i++)
						tNew[i] = tMat;
					tR.materials = tNew;
					}
				}
		}

	}

	public void ShowHollogram(GameObject tReference){
		//if (this.GetComponentInParent<OVRGrabbable>().vIsBeingGripped){
			vHoloRate = 1f;
		if (vHologramObj == null && GameObject.FindGameObjectsWithTag("Hollow").Length <= 0){
				List<GameObject> tColliderList = new List<GameObject>();
				Transform[] tTransList = tReference.GetComponentsInChildren<Transform>();
				//List<Transform> tObjList = tReference.GetComponentsInChildren<Transform>();
				foreach (Transform tObjects in tTransList){
					if (tObjects.tag == "GripPart")
					tColliderList.Add(tObjects.gameObject);

				}
			vHologramSource = tReference;

			// hologram creation
				vHologramObj = Instantiate(tReference) as GameObject;
				vHologramObj.BroadcastMessage("TurnOff","Hollow");
				vHologramObj.transform.SetParent(this.transform);
				vHologramObj.transform.localPosition =Vector3.zero;
				vHologramObj.transform.localEulerAngles = Reorientate(tReference);

				tTransList = new Transform[0];
				tTransList = vHologramObj.GetComponentsInChildren<Transform>();
				foreach (Transform tObjects in tTransList){
					if (tObjects.tag == "GripPart"){
						Scr_ContactSource_ForCollision tCSFC = tObjects.gameObject.AddComponent<Scr_ContactSource_ForCollision>();
						tCSFC.vSkipList = tColliderList;
						tCSFC.vSourceSocket = this;
						}
					else{Collider tCollider = tObjects.GetComponent<Collider>();
						if (tCollider != null)
							tCollider.enabled = false;

					}
				}


				// Turn Off Scripts
				OVRGrabbable[] tOVRGrabbableList = vHologramObj.GetComponentsInChildren<OVRGrabbable>();
			foreach (OVRGrabbable tOVRGrabbable in tOVRGrabbableList)
				Destroy(tOVRGrabbable);
				Scr_Male_Socket[] tMaleSocketList = vHologramObj.GetComponentsInChildren<Scr_Male_Socket>();
			foreach (Scr_Male_Socket tMaleSocket in tMaleSocketList)
				Destroy(tMaleSocket);
				Scr_Female_Socket[] tFemaleSocketList = vHologramObj.GetComponentsInChildren<Scr_Female_Socket>();
			foreach (Scr_Female_Socket tFemaleSocket in tFemaleSocketList)
				Destroy(tFemaleSocket);
				Scr_Mod_Magazine[] tModMagazineList = vHologramObj.GetComponentsInChildren<Scr_Mod_Magazine>();
				foreach (Scr_Mod_Magazine tModMagazine in tModMagazineList)
				Destroy(tModMagazine);
				Scr_ModSystem_Handler[] tModHandlerList = vHologramObj.GetComponentsInChildren<Scr_ModSystem_Handler>();
			foreach (Scr_ModSystem_Handler tModHandler in tModHandlerList)
				Destroy(tModHandler);


				Renderer[] tListA =  vHologramObj.GetComponentsInChildren <Renderer>();
				foreach (Renderer tR in tListA){
					Material[] tNew = new Material[tR.materials.Length];
					for(int i = 0; i < tR.materials.Length;i++)
						tNew[i] = vMatGood;
					tR.materials = tNew;
				}
			//}
		}
	}

	public void fAcceptAttachement(GameObject tReference){
		if (vHologramObj != null)
		if (vCollisionRate <= 0f && vHologramSource == tReference){
			Scr_ModSaverSocket tTemp = this.GetComponent<Scr_ModSaverSocket>();
				tTemp.vConnection = tReference;


			vConnectedObject = tReference;
			tReference.transform.SetParent(this.transform);
			tReference.transform.localPosition= Vector3.zero;
			tReference.transform.localEulerAngles = vHollowAngle;//Reorientate(tReference);//+this.transform.eulerAngles;
			tReference.GetComponent<Rigidbody>().useGravity = false;
			tReference.GetComponent<Rigidbody>().isKinematic = true; 


			Scr_Male_Socket tRefMS = tReference.GetComponent<Scr_Male_Socket>();

			foreach (GameObject tThat in tRefMS.vOriginalParts) {
				tThat.transform.SetParent(this.transform);
			}
			tRefMS.vConnectedTo = this.gameObject;
			tReference.transform.SetParent(this.transform);

				Scr_ModSystem_Handler tRootMSH = tReference.transform.root.GetComponent<Scr_ModSystem_Handler>();

				if (tRootMSH != null){
					tRootMSH.GetComponent<Scr_ModHandle>().fUpdateList();
					tRootMSH.lModsConnected.Add(tReference);
					if (tRefMS.vIsMagazine)
						tRootMSH.lMagazineList.Add(tReference);
				}
			}
	}
	// Re angle the hologram
	Vector3 Reorientate(GameObject tObject){
		if (tObject == null)
			return Vector3.zero;
		Vector3 tNewVect = tObject.transform.localEulerAngles;
		OVRGrabbable tCheck = this.GetComponentInParent<OVRGrabbable>();
		GameObject tObj = tCheck.gameObject;
		Vector3 tOwnVect = tObj.transform.eulerAngles;
		tNewVect.y = tNewVect.y-tOwnVect.y; // I reversed this
		if (tNewVect.y >360) tNewVect.y -= 360f;

		tNewVect.x = 0;
		tNewVect.y = ((Mathf.Round(tNewVect.y/90f))*90f);
		tNewVect.z = 0;
		return tNewVect;
	}
}
