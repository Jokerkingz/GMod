using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_roomManager : MonoBehaviour {

	public int enemiesLeft;
	public GameObject doorGoal;
	private bool lockCheck;
	//public GameObject[] enemiesToAlert;

	public bool isPlayingSpecialMusic;
	public bool combatMusicAlreadyPlaying;
	public bool passiveMusicAlreadyPlaying;
	public bool musicAlreadyStopped;
	public Scr_NewMusicManager musicManager;

	void Start () {
		enemiesLeft=999;
		lockCheck=false;
		musicManager = FindObjectOfType<Scr_NewMusicManager>();
	}
	
	
	void Update () {

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("AI");
		enemiesLeft=enemies.Length;

		if (enemiesLeft==0 &&!lockCheck)
		{
			doorGoal.GetComponent<Scr_Door>().DoorUnlocked();
			lockCheck=true;
		}

		if (enemiesLeft==0 &&!passiveMusicAlreadyPlaying &&!isPlayingSpecialMusic)
		{
			musicManager.PlayPassiveMusic();
			passiveMusicAlreadyPlaying=true;
		}
		if (enemiesLeft==0 &&!musicAlreadyStopped &&isPlayingSpecialMusic)
		{
			musicManager.StopMusic();
			musicAlreadyStopped=true;
		}
		if (enemiesLeft>= 1 &&!combatMusicAlreadyPlaying &&!isPlayingSpecialMusic)
		{
			musicManager.PlayCombatMusic();
			combatMusicAlreadyPlaying=true;
		}

	}

	public void RoomAlerted()
	{
		/*foreach (GameObject enema in enemiesToAlert)
		{
			if (GetComponent<Scr_BasicAI>().boolChase ==false)
			{GetComponent<Scr_BasicAI>().boolChase=true;}
		}*/
		/*Scr_BasicAI enemiesToAlert = gameObject.GetComponent(typeof(Scr_BasicAI)) as Scr_BasicAI;
		enemiesToAlert.Alerted();*/
	}
}
