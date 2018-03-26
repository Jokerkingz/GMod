using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_ambushManager : MonoBehaviour {

	public int enemiesLeftBeforeAmbush;
	public int enemiesAmmountForAmbush;
	public GameObject ambushDoor;
	
	private bool lockCheck;
	
	void Start () {
		enemiesLeftBeforeAmbush=999;
		lockCheck=false;
	}
	
	
	void Update () {

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("AI");
		enemiesLeftBeforeAmbush=enemies.Length;

		if (enemiesLeftBeforeAmbush==enemiesAmmountForAmbush &&!lockCheck)
		{
			ambushDoor.GetComponent<scr_doorAmbush>().DoorAlarm();
			lockCheck=true;
		}
	}
}

