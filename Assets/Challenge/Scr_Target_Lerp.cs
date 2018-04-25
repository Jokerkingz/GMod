using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Target_Lerp : MonoBehaviour {
	public Vector3 vStartingPoint;
	public Vector3 vEndPoint;
	private float vTimeLerp;
	private float vSpeedMultiplier = .1f;
	// Use this for initialization
	void Start () {
		vStartingPoint = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		vTimeLerp += vSpeedMultiplier*Time.fixedDeltaTime;
		if (vTimeLerp > 1f){
			Vector3 tTemp = vStartingPoint;
			vStartingPoint = vEndPoint;
			vEndPoint = tTemp;
			vTimeLerp = 0;
		}
		this.transform.position = Vector3.Lerp(vStartingPoint,vEndPoint,vTimeLerp);
	}
}
