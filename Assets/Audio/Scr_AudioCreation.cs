using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_AudioCreation : MonoBehaviour {
	public AudioSource vAudioSource;
	public AudioClip[] vAudioClip;
	// Use this for initialization

	public void fCreateSound(string tAudiotoplay,Vector3 tPosition){
	this.transform.position = tPosition;
		float vTimeDuration = 1f;
		switch (tAudiotoplay) {
			case "ClipOn":
			vAudioSource.PlayOneShot(vAudioClip[0]);
			break;
			case "ClipOff":
			vAudioSource.PlayOneShot(vAudioClip[1]);
			break;
		}

		Invoke("DestroySelf",vTimeDuration);
	}
	void DestroySelf(){
	Destroy(this.gameObject);

	}
}
