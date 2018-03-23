using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_SystemTouchReceiver : MonoBehaviour {
	public Scr_TableController vTableSource;
	public string vMessageToSend;

	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter(Collider tOther){
		if (tOther.tag == "FingerTip"){
			if (tOther.GetComponent<Scr_TouchTip>().vPointing)
				vTableSource.gameObject.SendMessage(vMessageToSend);
				}
	}
}
