using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_AudioOnCollision : MonoBehaviour {
	public AudioClip[] vSound2Play;
	private AudioSource cAS;
	private bool vReady;

	void Start(){
		cAS = this.GetComponent<AudioSource>();

	}
	void OnCollisionEnter(){
		if (!vReady)
			vReady = true;
		else{
		//Debug.Log("poop");
			cAS.PlayOneShot(vSound2Play[Random.Range(0,vSound2Play.Length-1)],.1f);
			}
	}
}
