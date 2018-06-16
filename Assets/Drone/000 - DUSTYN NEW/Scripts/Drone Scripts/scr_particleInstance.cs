using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_particleInstance : MonoBehaviour {

	public ParticleSystem particleExplosion;
	
	public void fCreateParticle(Vector3 tPosition){
	this.transform.position = tPosition;
	float vTimeDuration = 3f;



	Invoke("DestroySelf",vTimeDuration);
	}

	
	void DestroySelf(){
	Destroy(this.gameObject);

	}
}
