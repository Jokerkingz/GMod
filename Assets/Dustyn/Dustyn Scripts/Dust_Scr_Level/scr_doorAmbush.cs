using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_doorAmbush : MonoBehaviour {


	private Animation anim;
	public AudioClip[] audioClip;
	private AudioSource As;
	public float warningTimer;
	public GameObject warningLight;
	public GameObject[] enemiesAmbushing;
	public bool isAlreadyAlerted;
	public bool isLightActive;
	public bool isFlareUp;
	public bool isFlareDown;
	void Start () {

		anim = this.gameObject.GetComponent<Animation>();
		As = this.gameObject.GetComponent<AudioSource>();
	}
	
	
	void Update () {

		if (isFlareUp &&isLightActive)
		{
			warningLight.GetComponent<Light>().intensity ++;
		}
		if (warningLight.GetComponent<Light>().intensity>=50 &&isLightActive)
		{
			isFlareUp=false;
			isFlareDown=true;
		}

		if (isFlareDown &&isLightActive)
		{
			warningLight.GetComponent<Light>().intensity --;
		}

		if (warningLight.GetComponent<Light>().intensity<=9 &&isLightActive)
		{
			isFlareDown=false;
			isFlareUp=true;
		}

	}


	void PlaySound(int clip)
	{
		AudioSource audio = GetComponent<AudioSource> ();

		audio.clip = audioClip [clip];
		audio.Play ();

	}

	public void DoorAlarm()
	{
		warningLight.GetComponent<Light>().enabled=true;
		isLightActive=true;
		isFlareUp=true;
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
		warningLight.GetComponent<Light>().enabled=false;
		isLightActive=false;
		PlaySound(1);
		anim.Play(anim.clip.name="ani_SideDoorOpen");
		AlertTheAmbushEnemies();
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
