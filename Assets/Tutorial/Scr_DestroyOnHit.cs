using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DestroyOnHit : MonoBehaviour {
	public void fHit(){
		Destroy(this.gameObject);
	}
}
