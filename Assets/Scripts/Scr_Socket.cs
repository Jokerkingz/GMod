using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Socket : MonoBehaviour {
	public List<GameObject> vFemaleSocket;
	public string vState;

	void Start(){
		vFemaleSocket.Clear();
		vState = "Free";

	}
	void Update(){
		foreach (GameObject tThis in vFemaleSocket){
			tThis.GetComponent<Scr_SocketF>().ShowHollogram(this.gameObject,"Base");
		}


	}
	public void CheckForAttach(){
		foreach (GameObject tThis in vFemaleSocket){
			tThis.GetComponent<Scr_SocketF>().AcceptPart(this.gameObject,"Base");
		}
	}
	void OnTriggerEnter(Collider tOther){
		if (tOther.tag == "SocketFemale"){
			vFemaleSocket.Add(tOther.gameObject);
		}
	}
	void OnTriggerExit(Collider tOther){
		if (tOther.tag == "SocketFemale"){
			vFemaleSocket.Remove(tOther.gameObject);
		}
	}
/*
	public bool vMale;
	public List<GameObject> vConnectWith = new List<GameObject>();
	public int tCount;
	public bool vOccupied;
	public GameObject vSampleGun;
	private Rigidbody cRB;
	// Use this for initialization
	private bool vShowCopy;

	public GameObject vHandHeld;
	public bool vBeingHeld;

	void Start(){
		cRB = this.GetComponent<Rigidbody>();

	}
	void Update(){
		if (vBeingHeld)
			{
			this.transform.position = vHandHeld.transform.position;
			this.transform.eulerAngles = vHandHeld.transform.eulerAngles;
			//cRB.angularVelocity = Vector3.zero;
			//cRB.velocity = Vector3.zero;
		
			}

	}

	public void Grabbing(GameObject tHand){
		vBeingHeld = true;
		vHandHeld = tHand;

	}
	public void LetGo(Vector3 tVelocity){
		vBeingHeld = false;
		vHandHeld = null;
		cRB.velocity = -tVelocity;
		cRB.AddForce(-tVelocity);
		//this.GetComponent<Scr_DebugShow>().ShowText(this.gameObject,tVelocity.ToString());
	}

	void OnTriggerEnter(){


	}
	void OnTriggerExit(){


	}
	*/
}
