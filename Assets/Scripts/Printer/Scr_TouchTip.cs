using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TouchTip : MonoBehaviour {
	public bool vPointing;
	public bool vIsRightHand;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		vPointing = false;
		if (vIsRightHand){
			if (Input.GetAxis("OGVR_RIndexTouch") <= 0 )// && Input.GetAxis("OGVR_RThumbRest") > 0 && Input.GetAxis("OGVR_RGrip") > 0)
				vPointing = true;
			}
		else{
			if (Input.GetAxis("OGVR_LIndexTouch") <= 0 )//&& Input.GetAxis("OGVR_LThumbRest") > 0 && Input.GetAxis("OGVR_LGrip") > 0)
				vPointing = true;
		}
	}
}
