using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_LookAtTarget : MonoBehaviour {
	public GameObject vTarget;
	public string vCheck = "x";
	// Use this for initialization
	// Update is called once per frame
	void Update () {
		if (vTarget == null){
			Destroy(this.gameObject);
			return;}
		transform.LookAt(vTarget.transform.position);
		switch (vCheck){
			case "x":
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,0f,0f);
			break;
		case "y":
			transform.localEulerAngles = new Vector3(0f,transform.localEulerAngles.y,0f);

			break;
		case "z":
			transform.localEulerAngles = new Vector3(0f,0f,transform.localEulerAngles.z);

			break;

		}
	}
}
