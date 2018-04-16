using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_roomManager : MonoBehaviour {

	public int enemiesLeft;
	public GameObject doorGoal;
	private bool lockCheck;
	//public GameObject musicManager;
	//public GameObject[] enemiesToAlert;

	void Start () {
		enemiesLeft=999;
		lockCheck=false;
		//musicManager = FindObjectOfType<Scr_NewMusicManager>();
		
	}
	
	
	void Update () {

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("AI");
		enemiesLeft=enemies.Length;

		if (enemiesLeft==0 &&!lockCheck)
		{
			doorGoal.GetComponent<Scr_Door>().DoorUnlocked();
			lockCheck=true;
		

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
