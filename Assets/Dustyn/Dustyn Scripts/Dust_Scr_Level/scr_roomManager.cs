using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_roomManager : MonoBehaviour {

	public int enemiesLeft;
	public GameObject doorGoal;
	private bool lockCheck;
	
	void Start () {
		enemiesLeft=999;
		lockCheck=false;
	}
	
	
	void Update () {

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("AI");
		enemiesLeft=enemies.Length;

		if (enemiesLeft==0 &&!lockCheck)
		{
			doorGoal.GetComponent<scr_doorGoal>().DoorUnlocked();
			lockCheck=true;
		}
	}
}
