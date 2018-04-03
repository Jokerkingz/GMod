using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_alertManager : MonoBehaviour {

	public GameObject[] enemiesToAlert;
	public bool alreadyAlerted;
	void Start () {

	}
	
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.A))
		{
			AlertEnemiesInArray();
		}
	}

	public void AlertEnemiesInArray()
	{
		if (!alreadyAlerted){
		foreach (GameObject enemy in enemiesToAlert)
		{
			enemy.GetComponent<Scr_BasicAI>().boolChase=true;
		}
		alreadyAlerted=true;
	}
	}
}
