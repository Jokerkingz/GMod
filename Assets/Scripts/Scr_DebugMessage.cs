using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DebugMessage : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	Invoke("Die",3f);
	GameObject[] tThat = GameObject.FindGameObjectsWithTag("MainCamera");
	foreach (GameObject tThis in tThat){
		if (tThis.name == "CenterEyeAnchor")
			this.transform.LookAt(tThis.transform.position);
			 }
	}
	void Die () {
		Destroy(this.gameObject);
	}
}
