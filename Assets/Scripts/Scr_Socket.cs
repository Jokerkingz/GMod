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


	enum ModType {Null, Base, Barrel, Handle, Magazine, Ammunition, Shield, Battery};

	[Header("Connection Parts")]
	ModType vPartType = ModType.Base;
	ModType vRequiredPart = ModType.Null;

	GameObject vConnectedWith;


	void Start(){
		vFemaleSocket.Clear();
		Scr_SocketF[] tSkip = this.GetComponentsInChildren<Scr_SocketF>();
		foreach(Scr_SocketF tTemp in tSkip){
			vFemaleSkip.Add(tTemp.gameObject);
		}
		vState = "Free";
		cOVRGable = GetComponent<OVRGrabbable>();
	}
	void Update(){
		GameObject tClosest = NearestFromList();
		if (tClosest != null && cOVRGable.vIsBeingGripped)
			tClosest.GetComponent<Scr_SocketF>().ShowHollogram(this.gameObject,"Base");
	}
	public void CheckForAttach(){
		GameObject tClosest = NearestFromList();
		if (tClosest != null){
			GameObject tReceive = null;
			if (tClosest.GetComponentInParent<OVRGrabbable>().vIsBeingGripped){
				tReceive = tClosest.GetComponent<Scr_SocketF>().AcceptPart(this.gameObject,"Base");
				}
			vAttachedTo = tReceive;
		}
		vFemaleSocket.Clear();
	}
	public void Detach(){
		if (vAttachedTo != null){
			BroadCastThis("OldUnequip");
			vAttachedTo.GetComponent<Scr_SocketF>().RemoveAttachement(this.gameObject);
			vFemaleSocket.Clear();
			Debug.Log("RemovedAttachment");}
		
		vAttachedTo = null;
	}
	void OnTriggerEnter(Collider tOther){
		if (tOther.tag == "SocketFemale"){
			if (!vFemaleSkip.Contains(tOther.gameObject))
				vFemaleSocket.Add(tOther.gameObject);

		}
	}
	void OnTriggerExit(Collider tOther){
		if (tOther.tag == "SocketFemale"){
			if (!vFemaleSkip.Contains(tOther.gameObject))
				vFemaleSocket.Remove(tOther.gameObject);
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

	void BroadCastThis(string vAttachDetach){
		this.transform.root.gameObject.BroadcastMessage(vAttachDetach,this.gameObject);
	}

	void NewEquiped(GameObject tThis){
		if (vRequiredPart != ModType.Null){
			if (vRequiredPart == tThis.GetComponent<Scr_Socket>().vPartType){
				vConnectedWith = tThis.gameObject;
			}
			
		}

	}
	void OldUnequip(GameObject tThis){
		if (vConnectedWith == tThis.gameObject)
			vConnectedWith = null;
	}
}
