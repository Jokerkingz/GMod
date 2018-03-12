using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ModSystem_Handler : MonoBehaviour {
	public List<GameObject> lModsConnected; // Current Attached mods
	public List<GameObject> lMagazineList;
	private float vCheckingTimer = 0;
	private bool vIsCheckingForParts = false;
	public GameObject vHolsterConnectedTo;
	public float vHollowRate;
	public GameObject vHolsterShowHollowTo;
	public Scr_ModHandle cMH;
	// Use this for initialization
	void Start () {
		lModsConnected.Clear();
	}
	
	// Update is called once per frame
	void Update () {
		if (vIsCheckingForParts){
			if (vCheckingTimer > 0)
				vCheckingTimer -= Time.deltaTime;
			if (vCheckingTimer < 0){
				vCheckingTimer = 0;
				vIsCheckingForParts = false;
			}
		}
		if (vHollowRate > 0f){
			if (vHolsterShowHollowTo != null)
				vHolsterShowHollowTo.GetComponent<Scr_Belt_Holsters>().ShowHollogram(this.gameObject);
			vHollowRate -=.2f;
			if (vHollowRate <= 0f)
				vHolsterShowHollowTo = null;
			}

	}
	void fInputToBroadCast(){
		if (!vIsCheckingForParts){
			
		}

	}
	void fRecallAllConnectedObjects(){
	/// When a new Mod is attached, check if parts are missing or if extras are attached.
		vIsCheckingForParts = true;
		vCheckingTimer = 1f;
		lModsConnected.Clear();
	// this.broadcastmessage();
	}

	public void fCallToReload(){
		


	}
	public void ReceiveModPart(GameObject tModPart){
		if (!lModsConnected.Contains(tModPart)){
			// Receive new parts that can send back the broadcast
			

		}
	}
	void OnTriggerStay(Collider tOther){
		if (tOther.tag == "Belt" && this.GetComponent<OVRGrabbable>().vIsBeingGripped){
			if (tOther.GetComponent<Scr_Belt_Holsters>().vStatus == "Nothing"){
				vHolsterShowHollowTo = tOther.gameObject;
					vHollowRate = 1f;
				}
			}
		//Debug.Log("I found something");

	}
}
