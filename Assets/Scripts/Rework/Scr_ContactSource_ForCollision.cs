using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ContactSource_ForCollision : MonoBehaviour {
	public Scr_Female_Socket vSourceSocket;
	public List<GameObject> vSkipList;


	void OnTriggerStay(Collider tOther){
		bool tOutput = true;
	foreach (GameObject tSkipObject in vSkipList){
		if (tSkipObject == tOther.gameObject)
				tOutput = false;
		}
		if (tOutput)
		vSourceSocket.vCollisionRate = 1f;
	}
}
