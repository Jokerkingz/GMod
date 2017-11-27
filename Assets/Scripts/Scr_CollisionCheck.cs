using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CollisionCheck : MonoBehaviour {
	public float vHere = 0f;
	public GameObject vException;
	void Start(){
		vHere = 0f;
		}
	void Update(){
		vHere -= .5f;
		vHere = Mathf.Clamp(vHere,0f,1f);
	}
	void OnTriggerStay(Collider tOther){
		if (tOther.tag == "GripPart"){
			if (vException == null)
				vHere += 1f;
			else if (vException.gameObject != tOther.gameObject)
			vHere += 1f;
			}
	}
}
