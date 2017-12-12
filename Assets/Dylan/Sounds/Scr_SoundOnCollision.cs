using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_SoundManager : MonoBehaviour {
	public AudioClip vSound2Play;
	public AudioSource cAS;
	void Start(){
		cAS = this.GetComponent<AudioSource>();

	}
	void OnCollisionEnter(){
		Debug.Log("PlaySound");
		cAS.PlayOneShot(vSound2Play);
	}
}
