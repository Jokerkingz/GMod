using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CheckBody : MonoBehaviour {
	public Vector3 vOpenSpot;
	public GameObject vBodyChecker;
	public bool vHasASpot;
	public float vCD = 2f;
	public GameObject vModel;
	public Material vMatGood;
	public Material vMatBad;
	// Use this for initialization
	void Start () {
		vBodyChecker = GameObject.FindGameObjectWithTag("TeleportHere");
	}
	
	// Update is called once per frame
	void Update () {
		vModel.transform.Rotate(0,1f*Time.deltaTime,0);
		vCD -= .5f;
		if (vCD <= 0){
			vCD = 0f;
			CheckFreeSpot();
		}
	}
	public void CheckFreeSpot(){
		{vOpenSpot = transform.position;
			vBodyChecker.transform.position = vOpenSpot;
			//GetComponentInParent<Scr_PointToMove>().vChosenSpot.transform.position = vOpenSpot; 
			if (!vHasASpot){
				vHasASpot = true;
				vModel.GetComponent<Renderer>().material = vMatGood;
				//GetComponent<Renderer>().enabled = true;
				}
			
			}
		}
	void OnTriggerStay(Collider tOther){
		if (tOther.tag == "Solid" || tOther.tag == "Untagged"){
			vCD = 2f;
			vHasASpot = false;
			vModel.GetComponent<Renderer>().material = vMatBad;}
	}
}
