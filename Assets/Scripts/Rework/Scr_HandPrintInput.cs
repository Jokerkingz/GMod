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
		GetComponentsInChildren<HandSymbolState>();



	}
	void Update(){
		vCook -= Time.deltaTime;
		foreach (ParticleSystem tThis in vList){
			//tThis.main.simulationSpeed = ;
			var main = tThis.main;
			main.simulationSpeed = .3f+(.5f*vCook);

		  } 
		if (!vDone && vCook > 1f){
			if (vIsUsedForTable)
				{vTableSource.fHandPressed();
				Destroy(this.gameObject);
				 vDone = true;
			}
			else{
				vDoorSource.fDoorOpen();
				//vDone = true;
				vCook = 0f;
				}
			Debug.Log("I found the Hand");
			//enabled = false;
		}
		vCook = Mathf.Clamp(vCook,0f,2f);
	}

	void OnTriggerStay(Collider tOther){
	if (tOther.tag == "Hand")
			vCook += 2*Time.deltaTime;
	}
}
