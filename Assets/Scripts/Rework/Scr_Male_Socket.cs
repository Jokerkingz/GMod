using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Male_Socket : MonoBehaviour {
	[Header("Connection Status")]
	public List<GameObject> vOriginalParts;
	public List<GameObject> vGripParts;
	public GameObject vConnectedTo;
	public bool vIsMagazine;
	private List<GameObject> vOwnSockets = new List<GameObject>();
	public List<GameObject> vConnectableSockets = new List<GameObject>();
	private bool vStarted = false;

	[Header("Status Result of mod")]
	public string vStatus = "Active"; // Materializing, Hologram, neutral

	[Header("Audio Settings")]
	public AudioClip vSFX;
	private AudioSource cAS;

	// Misc
	private OVRGrabbable cOVRGrabbable;



	// Use this for initialization
	public void Start () {
		if (vStarted) return;

		cAS = this.GetComponent<AudioSource>();
		cOVRGrabbable = GetComponent<OVRGrabbable>();

		// Sort through all objects and categorize to which List
		Transform[] tObjectList = this.GetComponentsInChildren<Transform>();
		foreach (Transform tObject in tObjectList){
			if (tObject.gameObject != this.gameObject){
				if (tObject.gameObject.tag == "GripPart"){
					vGripParts.Add(tObject.gameObject);
				}
				else{
					if (tObject.gameObject.tag == "SocketFemale")
						vOwnSockets.Add(tObject.gameObject);
					vOriginalParts.Add(tObject.gameObject);
				}
			}
		}
		this.enabled = false;
		vStarted = true;
	}
	
	// Update check if there are connectables
	void Update () {
		GameObject tClosest = NearestFromList();
		if (tClosest != null)
			tClosest.GetComponent<Scr_Female_Socket>().ShowHollogram(this.gameObject);
	}

	public void CheckForAttach(){
		GameObject tClosest = NearestFromList();
		if (tClosest != null){
			//if (tClosest.GetComponentInParent<OVRGrabbable>().vIsBeingGripped){
			tClosest.GetComponent<Scr_Female_Socket>().fAcceptAttachement(this.gameObject);
				//}
		}
	}


	// Detaching this for reset
	public void Detach(GameObject tDetachingObject){
		Scr_Male_Socket tMalSocket = tDetachingObject.GetComponent<Scr_Male_Socket>();
		// Update Handle
		Scr_ModSystem_Handler tRootMSH = tDetachingObject.transform.root.GetComponent<Scr_ModSystem_Handler>();
		if (tRootMSH != null){
			tRootMSH.lModsConnected.Remove(tDetachingObject);
			if (vIsMagazine)
				tRootMSH.lMagazineList.Remove(tDetachingObject);
		}

		cAS.PlayOneShot(vSFX,.3f);

		tDetachingObject.GetComponent<Rigidbody>().useGravity = true;
		tDetachingObject.GetComponent<Rigidbody>().isKinematic = false; 

		// Remove Connections
		if (tMalSocket.vConnectedTo != null){
			GameObject tSocket = tMalSocket.vConnectedTo;
			tSocket.GetComponent<Scr_Female_Socket>().vConnectedObject = null;
			tSocket.GetComponent<Scr_ModSaverSocket>().vConnection = null;
			tMalSocket.vConnectedTo = null;
			}
			/*
		if (tDOMS.vConnectedTo != null)
			{Scr_Female_Socket tCheckDetach = tDOMS.vConnectedTo.GetComponent<Scr_Female_Socket>();
			if (tCheckDetach != null){
				tCheckDetach.vConnectedObject = null;
				tCheckDetach.GetComponent<Scr_ModSaverSocket>().vConnection =null;
				}
			vConnectedTo = null;
			}
			*/
		foreach (GameObject tTrans in tMalSocket.vOriginalParts){
			tTrans.transform.SetParent(tDetachingObject.transform);
		}
		Transform tRoot = tDetachingObject.transform.root;
		tDetachingObject.transform.SetParent(null);

		tRoot.transform.SetParent(tDetachingObject.transform);
		tRoot.transform.SetParent(null);
		if (tRoot.GetComponent<Scr_ModHandle>() != null)
			tRoot.GetComponent<Scr_ModHandle>().fUpdateList();

	}

	// Socket Detection
	void OnTriggerStay(Collider tOther){
		if (tOther.tag == "SocketFemale"){
			if (!vOwnSockets.Contains(tOther.gameObject)){
				//if (tOther.GetComponent<Scr_SocketF>().vPartType != vPartType)
				if (tOther.GetComponent<Scr_Female_Socket>().vConnectedObject == null)
					if (!vConnectableSockets.Contains(tOther.gameObject))
					vConnectableSockets.Add(tOther.gameObject);
				}
		}
	}

	void OnTriggerExit(Collider tOther){
		if (tOther.tag == "SocketFemale"){
			if (!vOwnSockets.Contains(tOther.gameObject)){
				//if (tOther.GetComponent<Scr_SocketF>().vPartType != vPartType)
				if (tOther.GetComponent<Scr_Female_Socket>().vConnectedObject == null)
					vConnectableSockets.Remove(tOther.gameObject);
				}
		}
	}

	// Sorting Functions
	GameObject NearestFromList(){
		GameObject tClosest = null;
		float tDistance = 0f;
		foreach (GameObject tThis in vConnectableSockets){
			if (tClosest == null){
				tClosest = tThis;
				tDistance = Vector3.Distance(this.transform.position,tThis.transform.position);
				}
			else if (Vector3.Distance(this.transform.position,tThis.transform.position) < tDistance)
				{tClosest = tThis;
				tDistance = Vector3.Distance(this.transform.position,tThis.transform.position);
				}
			}
		return tClosest;
	}

}
 