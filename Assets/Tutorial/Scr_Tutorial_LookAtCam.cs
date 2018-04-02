using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Tutorial_LookAtCam : MonoBehaviour {
	private GameObject vCamera;
	// Use this for initialization
	void Start () {
		vCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(vCamera.transform);
	}
}
