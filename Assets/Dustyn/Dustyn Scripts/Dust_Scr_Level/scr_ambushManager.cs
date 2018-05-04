using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_ambushManager : MonoBehaviour {

	public int enemiesLeftBeforeAmbush;
	public int enemiesAmmountForAmbush;
	public GameObject ambushDoor;
	public bool loadRoomToBeAmbush;
	public string AmbushRoomToLoad;
	public GameObject doorToDelete;
	
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
			
			lockCheck=true;
			if (!loadRoomToBeAmbush)
			{ambushDoor.GetComponent<scr_doorAmbush>().DoorAlarm();}
			if(loadRoomToBeAmbush)
			{
				Scr_SceneManager.Instance.LoadNext(AmbushRoomToLoad);
				Destroy(doorToDelete);
			}
		}
	}
}

