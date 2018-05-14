using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Female_Socket : MonoBehaviour {
	private GameObject vHologramObj;
	private GameObject vHologramSource;
	public GameObject vConnectedObject;
	[Header("Audio Control")]
	public GameObject vAudioPlay;
	[Header("Hologram Setting")]
	public GameObject vPseudoPart;
	//public GameObject vOriginalPart;
	public Material vMatGood;
	public Material vMatBad;
	public float vHoloRate;
	public float vCollisionRate;

	private Vector3 vHollowAngle;
	public string vPartType;

	public GameObject vArrow;
	// Use this for initialization

	public string vAnglingType = "Joystick";
	void Start () {
		vArrow = Resources.Load("Pre_Arrow") as GameObject;
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
			// Get Angle From Hand
				float tAngleStick = 0;
				GameObject tHand =  this.transform.root.GetComponent<OVRGrabbable>().vHandObj;
				if (tHand != null)
					tAngleStick = tHand.GetComponentInChildren<Scr_PointToMove>().vAngleGiven;
			//float tAngleStick = tHand.GetComponentInChildren<Scr_PointToMove>().vAngleGiven;
			// End Of get Angle From Hand
			//vHollowAngle = Reorientate(vHologramSource);
				vHollowAngle = new Vector3(0,((Mathf.Round(tAngleStick/90f))*90f),0);
				vHologramObj.transform.localEulerAngles = vHollowAngle;//vHollowAngle;

			//vHologramObj.transform.LookAt(vHologramSource.transform.position);//
			//	vHollowAngle = new Vector3(0f,((Mathf.Round(vHologramObj.transform.localEulerAngles.y/90f))*90f),0f);//
			//	vHologramObj.transform.localEulerAngles = vHollowAngle;
				//vHologramObj.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,0f,0f);//

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
			vHologramSource = tReference;/*
			if (vArrow != null){
				GameObject tOut = Instantiate(vArrow) as GameObject;
				//tOut.BroadcastMessage("TurnOff","Hollow");
				tOut.transform.SetParent(this.transform);
				tOut.transform.localPosition =Vector3.zero;
				tOut.transform.localEulerAngles = Vector3.zero;
				tOut.GetComponent<Scr_LookAtTarget>().vTarget = tReference;
			}
			*/

			// hologram creation
				vHologramObj = Instantiate(tReference) as GameObject;
				vHologramObj.BroadcastMessage("TurnOff","Hollow");
				vHologramObj.transform.SetParent(this.transform);
				vHologramObj.transform.localPosition =Vector3.zero;
				//vHologramObj.transform.localEulerAngles = Reorientate(tReference);

				//vHologramObj.transform.LookAt(vHologramSource.transform.position);//
			//vHollowAngle = new Vector3(0f,((Mathf.Round(vHologramObj.transform.localEulerAngles.y/90f))*90f),0f);//
				vHollowAngle = Vector3.zero;
				vHologramObj.transform.localEulerAngles = vHollowAngle;

				tTransList = new Transform[0];
				tTransList = vHologramObj.GetComponentsInChildren<Transform>();
				foreach (Transform tObjects in tTransList){
					if (tObjects.tag == "GripPart"){
					/*
						Scr_ContactSource_ForCollision tCSFC = tObjects.gameObject.AddComponent<Scr_ContactSource_ForCollision>();
						tCSFC.vSkipList = tColliderList;
						tCSFC.vSourceSocket = this;*/
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
			Scr_ModBarrel[] tModBarrelList = vHologramObj.GetComponentsInChildren<Scr_ModBarrel>();
			foreach (Scr_ModBarrel tModBarrel in tModBarrelList)
				Destroy(tModBarrel);


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
		if (tReference == null)
			return;
		if (vHologramSource == null)
			return;
		if (vHologramObj == null)
		return;
		//vHologramObj.transform.LookAt(vHologramSource.transform.position);//
		//vHollowAngle = new Vector3(0f,((Mathf.Round(vHologramObj.transform.localEulerAngles.y/90f))*90f),0f);//
		vHologramObj.transform.localEulerAngles = vHollowAngle;
		if (vHologramObj != null)
		if (vCollisionRate <= 0f && vHologramSource == tReference){

			Instantiate(vAudioPlay).GetComponent<Scr_AudioCreation>().fCreateSound("ClipOn",this.transform.position);

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
		//vHologramObj.transform.LookAt(vHologramSource.transform.position);
		//transform.localEulerAngles = new Vector3(0f,transform.localEulerAngles.y,0f);
		return Vector3.zero;
	/*
		if (tObject == null)
			return Vector3.zero;
		Vector3 vSave = transform.localEulerAngles;
		Vector3 tMultiplier = Vector3.zero;
		if (vSave == Vector3.zero)
			tMultiplier = new Vector3(0,1,0);
		if (vSave.x != 0)
			tMultiplier = new Vector3(1,0,0);
		if (vSave.y != 0)
			tMultiplier = new Vector3(0,1,0);
		if (vSave.z != 0)
			tMultiplier = new Vector3(0,1,0);
		transform.LookAt(tObject.transform.position);
		Vector3 tNewVect = Vector3.Scale(transform.localEulerAngles, tMultiplier);
		//Vector3 tNewVect = transform.localEulerAngles = new Vector3(transform.localEulerAngles.x*tXMultiplier,transform.localEulerAngles.y*tYMultiplier,transform.localEulerAngles.z*tZMultiplier);
		transform.localEulerAngles = vSave;
		return tNewVect;
		/*
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
		*/
	}
	// Re angle the hologram
	Vector3 ReorientateOld(GameObject tObject){
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
