using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_AvatarController : MonoBehaviour {
	public Transform vMain;
	// Use this for initialization
	void Start () {
		vMain = GameObject.FindGameObjectWithTag("MainOVR").transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 tTemp = vMain.position;
		tTemp.y += .68f;
		this.transform.position = tTemp;
	}
}
