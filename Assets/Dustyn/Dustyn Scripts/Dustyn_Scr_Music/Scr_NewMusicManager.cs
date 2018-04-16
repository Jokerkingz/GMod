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
	public float slowFadeTime;
	public float maxVol;
	public float finalRoomIntroTimer;
	[Header("Bools")]
	public bool crossFadeIn;
	public bool crossFadeOut;
	public bool simpleFadeOut;
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

			if (passiveMusic.GetComponent<AudioSource>().volume<=0 && combatMusic.GetComponent<AudioSource>().volume>=maxVol)
			{
				crossFadeIn=false;
				passiveMusic.GetComponent<AudioSource>().Stop();
			}
		}

		if (crossFadeOut)
		{
			passiveMusic.GetComponent<AudioSource>().volume=passiveMusic.GetComponent<AudioSource>().volume +fadeTime;
			combatMusic.GetComponent<AudioSource>().volume =combatMusic.GetComponent<AudioSource>().volume-fadeTime;

			if (passiveMusic.GetComponent<AudioSource>().volume>=maxVol && combatMusic.GetComponent<AudioSource>().volume<=0)
			{
				crossFadeOut=false;
				combatMusic.GetComponent<AudioSource>().Stop();
			}
		}

		if (simpleFadeOut)
		{
			passiveMusic.GetComponent<AudioSource>().volume = passiveMusic.GetComponent<AudioSource>().volume-fadeTime;
			combatMusic.GetComponent<AudioSource>().volume =combatMusic.GetComponent<AudioSource>().volume-fadeTime;
			finalRoomMusicLoop.GetComponent<AudioSource>().volume =finalRoomMusicLoop.GetComponent<AudioSource>().volume-slowFadeTime;
			if (passiveMusic.GetComponent<AudioSource>().volume<=0 && combatMusic.GetComponent<AudioSource>().volume<=0 &&finalRoomMusicLoop.GetComponent<AudioSource>().volume <=0)
			{
				simpleFadeOut=false;
				combatMusic.GetComponent<AudioSource>().Stop();
				passiveMusic.GetComponent<AudioSource>().Stop();
				finalRoomMusicLoop.GetComponent<AudioSource>().Stop();
			}
		}
		if (passiveMusic.GetComponent<AudioSource>().volume>=maxVol)
		{passiveMusic.GetComponent<AudioSource>().volume=maxVol;}
		if (passiveMusic.GetComponent<AudioSource>().volume<=0)
		{passiveMusic.GetComponent<AudioSource>().volume=0;}

		if (combatMusic.GetComponent<AudioSource>().volume>=maxVol)
		{combatMusic.GetComponent<AudioSource>().volume=maxVol;}
		if (combatMusic.GetComponent<AudioSource>().volume<=0)
		{combatMusic.GetComponent<AudioSource>().volume=0;}

	}
	public void PlayCombatMusic()
	{
		combatMusic.GetComponent<AudioSource>().Play();
		crossFadeIn=true;
		transitionSound.GetComponent<AudioSource>().Play();
		Debug.Log("StartCombatMusic");
	}
	public void PlayPassiveMusic()
	{
		passiveMusic.GetComponent<AudioSource>().Play();
		crossFadeOut =true;
		transitionSound.GetComponent<AudioSource>().Play();
		Debug.Log("Start Passive Music");
	}

	public void PlayFinalRoomMusicIntro()
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
		finalRoomMusicLoop.GetComponent<AudioSource>().volume= maxVol;
		Debug.Log("Playing Final Loop");
	}
	public void StopFinalRoomMusicLoop()
	{
		simpleFadeOut=true;
	}

	public void StopMusic()
	{
		simpleFadeOut=true;
	}
}
