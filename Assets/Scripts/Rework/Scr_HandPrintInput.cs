using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_HandPrintInput : MonoBehaviour {
	public bool vIsUsedForTable;
	public Scr_TableController vTableSource;
	public Scr_Door vDoorSource;
	private float vCook;
	private float vSpeed;
	private bool vDone;
	private ParticleSystem[] vList = new ParticleSystem[0];
	void Start(){
		vList = GetComponentsInChildren<ParticleSystem>();

	}
	void Update(){
		vCook -= Time.deltaTime;
		foreach (ParticleSystem tThis in vList){
			//tThis.main.simulationSpeed = ;
			var main = tThis.main;
			main.simulationSpeed = .3f+(.5f*vCook);

		  } 
		if (!vDone && vCook > 2f){
			vDone = true;
			if (vIsUsedForTable)
			vTableSource.fHandPressed();
			else
			vDoorSource.fDoorOpen();
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
