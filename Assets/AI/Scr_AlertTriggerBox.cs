using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_AlertTriggerBox : MonoBehaviour {
	public Scr_AI cAI;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(){
		cAI.vIsAlert = true;


	}
}
