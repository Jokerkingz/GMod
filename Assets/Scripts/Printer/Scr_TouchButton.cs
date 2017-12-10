using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TouchButton : MonoBehaviour {
	public Scr_Printer vSource;
	public string vTypeOfButton;
	// Use this for initialization
	void OnTriggerEnter(Collider tOther){
		if (tOther.tag == "FingerTip"){
			if (tOther.GetComponent<Scr_TouchTip>().vPointing)
				vSource.vReceiveButton(vTypeOfButton);
			}
	}
}
