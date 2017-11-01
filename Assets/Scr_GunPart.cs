using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GunPart : MonoBehaviour {
	public Scr_Socket[] vSocketList;
	// Use this for initialization
	void Start () {
		vSocketList = this.GetComponentsInChildren<Scr_Socket>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
