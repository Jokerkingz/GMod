using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_doorAmbush : MonoBehaviour {


	private Animation anim;
	public AudioClip[] audioClip;
	private AudioSource As;
	public float warningTimer;

	public GameObject[] enemiesAmbushing;
	public bool isAlreadyAlerted;
	void Start () {

		anim = this.gameObject.GetComponent<Animation>();
		As = this.gameObject.GetComponent<AudioSource>();
	}
	
	
	void Update () {

	}


		void PlaySound(int clip)
	{
		AudioSource audio = GetComponent<AudioSource> ();

		audio.clip = audioClip [clip];
		audio.Play ();

	}

	public void DoorAlarm()
	{
		As.loop=true;
		As.Play();
		StartCoroutine(DoorAmbushSoon());
	}
	private IEnumerator DoorAmbushSoon()
	{
		yield return new WaitForSeconds(warningTimer);
		As.loop=false;
		DoorOpen();
	}

	 void DoorOpen()
	{	
		PlaySound(1);
		anim.Play(anim.clip.name="ani_openDoor");
	}

	void AlertTheAmbushEnemies()
	{
	if (!isAlreadyAlerted){
		foreach (GameObject enemy in enemiesAmbushing)
		{
			enemy.GetComponent<Scr_BasicAI>().boolChase=true;
		}
		isAlreadyAlerted=true;
	}
	}

}
