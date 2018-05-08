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
	private Scr_KillCountManager killCountManager;
	
	private bool lockCheck;
	private float deleteBufferTime =0.5f;
	
	void Start () {
		enemiesLeftBeforeAmbush=999;
		lockCheck=false;
		if (loadRoomToBeAmbush)
		{killCountManager = FindObjectOfType<Scr_KillCountManager>();}
	}
	
	
	void Update () {

		if (!loadRoomToBeAmbush)
		{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("AI");
		enemiesLeftBeforeAmbush=enemies.Length;

		if (enemiesLeftBeforeAmbush==enemiesAmmountForAmbush &&!lockCheck)
		{
			
			lockCheck=true;
			
			ambushDoor.GetComponent<scr_doorAmbush>().DoorAlarm();
		}
		}
			
		if(loadRoomToBeAmbush && enemiesAmmountForAmbush==killCountManager.killCount &&!lockCheck)

			{
				lockCheck=true;
				Scr_SceneManager.Instance.LoadNext(AmbushRoomToLoad);
				StartCoroutine(BufferTimeToDelete());
				
			}
			
		}

	IEnumerator BufferTimeToDelete()
	{
		yield return new WaitForSeconds(deleteBufferTime);
		Destroy(doorToDelete);
	}
}

