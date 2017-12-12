using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Belt : MonoBehaviour {
	public GameObject vObj;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = new Vector3(0f,vObj.transform.eulerAngles.y,0f);
		//transform.eulerAngles = Vector3.Scale(transform.eulerAngles,new Vector3(0f,1f,0f));
	}
}
