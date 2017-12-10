using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Belt : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = Vector3.Scale(transform.eulerAngles,new Vector3(0f,1f,0f));
	}
}
