using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DestroyTime : MonoBehaviour {
	// Use this for initialization
	public void fStartTimer (float tTimer) {
		Invoke("DestroyThis",tTimer);
	}
	
	// Update is called once per frame
	void DestroyThis() {
		Destroy(this.gameObject);
	}
}
