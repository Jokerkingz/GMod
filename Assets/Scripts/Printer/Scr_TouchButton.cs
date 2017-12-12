using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TouchButton : MonoBehaviour {
	public Scr_Printer vSource;
	public string vTypeOfButton;
	public GameObject[] vTargets;

	void Start(){
		vTargets = GameObject.FindGameObjectsWithTag("Respawn");

	}
	// Use this for initialization
	void OnTriggerEnter(Collider tOther){
		if (vTypeOfButton == "Restart"){
			foreach (GameObject tThat in vTargets){
				tThat.SetActive(true);
				tThat.GetComponent<Scr_RespawnStuff>().Reset();

			}
		}
		else if (tOther.tag == "FingerTip"){
			if (tOther.GetComponent<Scr_TouchTip>().vPointing)
				vSource.vReceiveButton(vTypeOfButton);
			}
	}
}
