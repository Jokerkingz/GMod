using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Tutorial_Touch : MonoBehaviour {
	public Scr_Tutorial_Table vTableSource;

	void OnTriggerEnter(Collider tOther){
		if (tOther.tag == "FingerTip"){
			if (tOther.GetComponent<Scr_TouchTip>().vPointing)
				vTableSource.gameObject.SendMessage("fPrintPressed");
				}
	}
}
