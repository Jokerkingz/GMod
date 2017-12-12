using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Scaler : MonoBehaviour {
	private int vAccScale;
private float vScale;
	void Start(){
		}
	// Use this for initialization
	void Update () {
		//Triggered ();
		if (vScale > 0)
			vScale -= Time.deltaTime*5f;

		vScale += vAccScale*.1f;
		vScale = Mathf.Clamp(vScale,.5f,7.5f);
		this.transform.localScale = new Vector3(vScale,vScale,vScale);
	}
	
	// Update is called once per frame
	public void Triggered (){
		if (vScale < 7.5)
			vScale += 50f*Time.deltaTime;
		//cRB.angularVelocity = new Vector3(0f,50f,0f);
		//transform.Rotate(new Vector3(0f,5f,0f));
	}
}
