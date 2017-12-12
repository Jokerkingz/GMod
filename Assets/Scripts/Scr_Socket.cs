using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Socket : MonoBehaviour {
	[Header("Base")]
	public List<GameObject> vFemaleSocket;
	public List<GameObject> vFemaleSkip;
	public GameObject vAttachedTo;
	public string vState;
	private OVRGrabbable cOVRGable;

	private Scr_SubStatus cSS;
	GameObject vConnectedWith;

	public List<Transform> vOriginalParts = new List<Transform>();

	public string vPartType;

	private AudioSource cAS;
	public AudioClip vSFX;
	void Start(){
		cAS = this.GetComponent<AudioSource>();
		vFemaleSocket.Clear();
		Scr_SocketF[] tSkip = this.GetComponentsInChildren<Scr_SocketF>();
		foreach(Scr_SocketF tTemp in tSkip){
			vFemaleSkip.Add(tTemp.gameObject);
		}

		vState = "Free";
		cOVRGable = GetComponent<OVRGrabbable>();

		// sort through current Parts
		Transform[] tCheckList = this.GetComponentsInChildren<Transform>();
		foreach(Transform tTemp in tCheckList){
			if ((tTemp.gameObject.tag != "GripPart") && (tTemp.gameObject != this.gameObject))
				vOriginalParts.Add(tTemp);
			}
		cSS = GetComponent<Scr_SubStatus>();
		if (cSS = null)
			Debug.Log(this.name);
	}
	void Update(){
		GameObject tClosest = NearestFromList();
		if (tClosest != null && cOVRGable.vIsBeingGripped)
			tClosest.GetComponent<Scr_SocketF>().ShowHollogram(this.gameObject,"Base");
	}
	public void CheckForAttach(){
		GameObject tClosest = NearestFromList();
		if (tClosest != null){
			if (tClosest.GetComponentInParent<OVRGrabbable>().vIsBeingGripped){
					tClosest.GetComponent<Scr_SocketF>().AcceptPart(this.gameObject,"Base");
				}
		}
	}
	public void Detach(FastList<GameObject> tAlreadyDetached){
		if (tAlreadyDetached.Contains(this.gameObject))
			return;
		if (vAttachedTo != null){
			BroadCastThis("OldUnequip");
			cAS.PlayOneShot(vSFX,.3f);
			this.GetComponent<Rigidbody>().useGravity = true;
			this.GetComponent<Rigidbody>().isKinematic = false; 
			vAttachedTo.GetComponent<Scr_SocketF>().vAttachedObject = null;

			transform.SetParent(null);
			foreach (Transform tThat in vOriginalParts) {
				tThat.SetParent(this.transform);
				}
			}

		vFemaleSocket.Clear();
		tAlreadyDetached.Add(this.gameObject);
		foreach (Transform tThat in vOriginalParts) {
			Scr_SocketF tcSF = null;
			tcSF = tThat.gameObject.GetComponent<Scr_SocketF>();
			if (tcSF != null){
				if (tcSF.vAttachedObject != null){

					tcSF.vAttachedObject.GetComponent<Scr_Socket>().Detach(tAlreadyDetached);
					}
			}
		}
		vAttachedTo = null;
		}

	void OnTriggerEnter(Collider tOther){
		if (tOther.tag == "SocketFemale"){
			if (!vFemaleSkip.Contains(tOther.gameObject)){
				//if (tOther.GetComponent<Scr_SocketF>().vPartType != vPartType)
				if (tOther.GetComponent<Scr_SocketF>().vAttachedObject == null)
				vFemaleSocket.Add(tOther.gameObject);
				}
		}
	}
	void OnTriggerExit(Collider tOther){
		if (tOther.tag == "SocketFemale"){
			if (!vFemaleSkip.Contains(tOther.gameObject)){
				//if (tOther.GetComponent<Scr_SocketF>().vPartType != vPartType)
				if (tOther.GetComponent<Scr_SocketF>().vAttachedObject == null)
				vFemaleSocket.Remove(tOther.gameObject);
				}
		}
	}

	GameObject NearestFromList(){
		GameObject tClosest = null;
		float tDistance = 0f;
		foreach (GameObject tThis in vFemaleSocket){
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

	public void BroadCastThis(string vAttachDetach){
		this.transform.root.gameObject.BroadcastMessage(vAttachDetach,this.gameObject,SendMessageOptions.DontRequireReceiver);
	}

	public void NewEquiped(GameObject tThis){
	/*
		if (tThis != null)
		if (cSS.vRequiredPart != Scr_SubStatus.ModType.Null){
			if (cSS.vRequiredPart == tThis.GetComponent<Scr_Socket>().cSS.vPartType){
				vConnectedWith = tThis.gameObject;
			}
			
		}
		*/
	}
	 
	public void OldUnequip(GameObject tThis){
		if (vConnectedWith == tThis.gameObject)
			vConnectedWith = null;
	}

    public void TurnOff(string vWhy){
    	switch (vWhy){
    	case "Hollow":
    		this.enabled = false;
    		this.tag = "Hollow";
    	break;
    	}
    }
}
