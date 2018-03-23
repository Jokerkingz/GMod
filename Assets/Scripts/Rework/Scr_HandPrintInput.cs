using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_HandPrintInput : MonoBehaviour {
	public bool vIsUsedForTable;
	public Scr_TableController vTableSource;
	public string vDoorSource;
	private float vCook;
	private bool vDone;
	void Update(){
		vCook -= Time.deltaTime;
		if (!vDone && vCook > 2f){
			vDone = true;
			vTableSource.fHandPressed();
			Debug.Log("I found the Hand");
			enabled = false;
			Destroy(this.gameObject);
		}
		vCook = Mathf.Clamp(vCook,0f,3f);
	}
	void OnTriggerStay(){
			vCook += 2*Time.deltaTime;
	}
}
