using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_NewMusicManager : MonoBehaviour {

	[Header("References")]
	public GameObject passiveMusic;
	public GameObject combatMusic;
	public GameObject transitionSound;
	public GameObject finalRoomMusicIntro;
	public GameObject finalRoomMusicLoop;
	[Header("Floats")]
	public float fadeTime;
	public float finalRoomIntroTimer;
	[Header("Bools")]
	public bool crossFadeIn;
	public bool crossFadeOut;
	void Start () {
	

	}
	
	void Update()
	{

		//------TESTING
	if (Input.GetKeyDown(KeyCode.A))
	{
		PlayCombatMusic();
	}
	if (Input.GetKeyDown(KeyCode.S))
	{
		PlayPassiveMusic();
	}
	if (Input.GetKeyDown(KeyCode.W))
	{
		PlayFinalRoomMusicIntro();
	}


		if (crossFadeIn)
		{
			passiveMusic.GetComponent<AudioSource>().volume = passiveMusic.GetComponent<AudioSource>().volume-fadeTime;
			combatMusic.GetComponent<AudioSource>().volume = combatMusic.GetComponent<AudioSource>().volume +fadeTime;

			if (passiveMusic.GetComponent<AudioSource>().volume<=0 && combatMusic.GetComponent<AudioSource>().volume>=1)
			{
				crossFadeIn=false;
			}
		}

		if (crossFadeOut)
		{
			passiveMusic.GetComponent<AudioSource>().volume=passiveMusic.GetComponent<AudioSource>().volume +fadeTime;
			combatMusic.GetComponent<AudioSource>().volume =combatMusic.GetComponent<AudioSource>().volume-fadeTime;

			if (passiveMusic.GetComponent<AudioSource>().volume>=1 && combatMusic.GetComponent<AudioSource>().volume<=0)
			{
				crossFadeOut=false;
			}
		}

		if (passiveMusic.GetComponent<AudioSource>().volume>=1)
		{passiveMusic.GetComponent<AudioSource>().volume=1;}
		if (passiveMusic.GetComponent<AudioSource>().volume<=0)
		{passiveMusic.GetComponent<AudioSource>().volume=0;}

		if (combatMusic.GetComponent<AudioSource>().volume>=1)
		{combatMusic.GetComponent<AudioSource>().volume=1;}
		if (combatMusic.GetComponent<AudioSource>().volume<=0)
		{combatMusic.GetComponent<AudioSource>().volume=0;}

	}
	void PlayCombatMusic()
	{
		crossFadeIn=true;
		transitionSound.GetComponent<AudioSource>().Play();
		Debug.Log("StartCombatMusic");
	}
	void PlayPassiveMusic()
	{
		crossFadeOut =true;
		transitionSound.GetComponent<AudioSource>().Play();
		Debug.Log("Start Passive Music");
	}

	void PlayFinalRoomMusicIntro()
	{
		finalRoomMusicIntro.GetComponent<AudioSource>().Play();
		Debug.Log("Playing Final Intro");
		StartCoroutine (WaitForIntroToEnd());
	}

	IEnumerator WaitForIntroToEnd()
	{
		yield return new WaitForSeconds(finalRoomIntroTimer);
		PlayFinalRoomMusicRoomLoop();
	}

	void PlayFinalRoomMusicRoomLoop()
	{
		finalRoomMusicLoop.GetComponent<AudioSource>().Play();
		Debug.Log("Playing Final Loop");
	}
}
