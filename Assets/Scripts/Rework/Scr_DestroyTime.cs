using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DestroyTime : MonoBehaviour {
public float vTimer;
public bool vAutoDie;
	// Use this for initialization
	void Start(){
		if (vAutoDie)
			Invoke("DestroyThis",vTimer);
	}
	public void fStartTimer (float tTimer) {
		Invoke("DestroyThis",tTimer);
	}
	
	// Update is called once per frame
	void DestroyThis() {
		Destroy(this.gameObject);
	}
}
