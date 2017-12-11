using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Rotator : MonoBehaviour {
private int vAccAngle;
private float vAngle;
	void Start(){
		}
	// Use this for initialization
	void Update () {
		//Triggered ();
		if (vAccAngle > 0)
			vAccAngle --;
	}
	
	// Update is called once per frame
	public void Triggered (){
		this.transform.localEulerAngles = new Vector3(0f,vAngle,0f);
		if (vAccAngle < 50)
		vAccAngle += 3;

		vAngle += vAccAngle*.3f;
		if (vAngle > 360)
			vAngle -= 360;
		//cRB.angularVelocity = new Vector3(0f,50f,0f);
		//transform.Rotate(new Vector3(0f,5f,0f));
	}
}
