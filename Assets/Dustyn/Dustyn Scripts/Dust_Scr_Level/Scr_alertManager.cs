using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_alertManager : MonoBehaviour {

	public GameObject[] enemiesToAlert;
	public bool alreadyAlerted;
	public bool dronesInMix;
	void Start () {

	}
	

	public void AlertEnemiesInArray()
	{
		if (!alreadyAlerted){
		foreach (GameObject enemy in enemiesToAlert)
		{
			if(!dronesInMix)
			{enemy.GetComponent<Scr_BasicAI>().boolChase=true;}
			if(dronesInMix)
			{enemy.GetComponent<scr_DroneMovement>().boolChase=true;}
		}
		alreadyAlerted=true;
	}
	}
}
