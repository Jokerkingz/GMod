using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_AudioCreation : MonoBehaviour {
	public AudioSource vAudioSource;
	public AudioClip[] vAudioClipBullet;
	public AudioClip[] vAudioClipRail;
	public AudioClip[] vAudioClipPlasma;
	public AudioClip[] vAudioClipLaser;
	public AudioClip[] vAudioClipGrenade;
	public AudioClip[] vAudioClipPellet;
	public AudioClip[] vAudioClipDoor;
	public AudioClip[] vAudioClipSnapOn;
	public AudioClip[] vAudioClipSnapOff;
	public AudioClip[] vAudioClipGeneral;

	// Use this for initialization

	public void fCreateSound(string tAudiotoplay,Vector3 tPosition){
	this.transform.position = tPosition;
		float vTimeDuration = 1f;
		switch (tAudiotoplay) {
			case "ClipOn":
			vAudioSource.PlayOneShot(vAudioClipSnapOn[Random.Range (0, vAudioClipSnapOn.Length)]);
			break;
			case "ClipOff":
			vAudioSource.PlayOneShot(vAudioClipSnapOff[Random.Range (0, vAudioClipSnapOff.Length)]);
			break;
			case "BulletCreation":
			vTimeDuration = 2f;
			vAudioSource.PlayOneShot(vAudioClipBullet[Random.Range (0, vAudioClipBullet.Length)]);
			break;
			case "RailCreation":
			vTimeDuration = 2f;
			vAudioSource.PlayOneShot(vAudioClipRail[Random.Range (0, vAudioClipRail.Length)]);
			break;
			case "PlasmaCreation":
			vTimeDuration = 2f;
			vAudioSource.PlayOneShot(vAudioClipPlasma[Random.Range (0, vAudioClipPlasma.Length)]);
			break;
		}

		Invoke("DestroySelf",vTimeDuration);
	}
	void DestroySelf(){
	Destroy(this.gameObject);

	}
}
