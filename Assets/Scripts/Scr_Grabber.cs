using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Grabber : MonoBehaviour {
	public string vStatus;
	public GameObject vHeldObject;

	public List<GameObject> vCollidableList;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch (vStatus){
		default:
			vStatus = "Idle";
		break;



		}
	}
}
