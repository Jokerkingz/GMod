using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerBox_Magazine : MonoBehaviour {
	private Scr_Magazine vParent;
	void Start(){
		vParent = GetComponentInParent<Scr_Magazine>();
	}
	void OnTriggerEnter(Collider tOther){
		if (tOther.tag == "Belt"){
			vParent.Reload();
		}
	}
}
