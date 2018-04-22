using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BulFX_Curve : MonoBehaviour {
	public Rigidbody cRB;
	public Vector3 vTilt;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		cRB.AddForce(vTilt*150f);
	}
}
