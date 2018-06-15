using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_stageMusicManager : MonoBehaviour {

	// NEWEST MUSIC MANAGER FOR STATIONARY STAGE

	[Header("References")]
	public GameObject MusicIntro;
	public GameObject MusicLoop;

	[Header("Floats")]	
	public float MusicIntroTimer;
	void Start () 
	{
		PlayMusicIntro();
	}

	void PlayMusicIntro()
	{
		MusicIntro.GetComponent<AudioSource>().Play();
		//Debug.Log("Playing Final Intro");
		StartCoroutine (WaitForIntroToEnd());
	}
	IEnumerator WaitForIntroToEnd()
	{
		yield return new WaitForSeconds(MusicIntroTimer);
		PlayFinalRoomMusicRoomLoop();
	}
		void PlayFinalRoomMusicRoomLoop()
	{
		MusicLoop.GetComponent<AudioSource>().Play();
		//Debug.Log("Playing Final Loop");
	}
}
