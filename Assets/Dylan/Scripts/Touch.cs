using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour {

	public Material newMaterial1;
	public Material newMaterial2;
	public Material newMaterial3;

	void OnTriggerEnter(Collider other){
		if (other.tag == "FingerTip") {
			Renderer rend = GetComponent<Renderer> ();
			rend.material = newMaterial1;
			Debug.Log ("Is it it in yet?");
		} else if (other.tag == "Select") {
			Renderer rend = GetComponent<Renderer> ();
			rend.material = newMaterial2;
			Debug.Log ("Is it it in yet?");
		}
	}
//	void OnTriggerStay (Collider other){
//		Renderer rend = GetComponent<Renderer> ();
//		rend.material = newMaterial2;
//		Debug.Log ("It's in!");
//	}

	void OnTriggerExit (Collider other){
		Renderer rend = GetComponent<Renderer> ();
		rend.material = newMaterial3;
		Debug.Log ("That's it?");
}
}
