using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DestroyOnHit : MonoBehaviour {
	public void fHit(){
		if (this.gameObject.tag!="AI")
		{
		Destroy(this.gameObject);
		}

		//---DUSTYN 
		if (this.gameObject.tag=="AI")
		{
			this.gameObject.GetComponent<scr_droneHealth>().Damage(2);
		}
	}
}
