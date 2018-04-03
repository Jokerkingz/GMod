using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_alertTrigger : MonoBehaviour {

	public GameObject[] alertTriggerEnemies;
	public bool isAlreadyAlerted;
	private Collider trigCol;

	void Start()
	{
		trigCol= this.gameObject.GetComponent<Collider>();
	}
	void OnTriggerEnter(Collider col)
	{
		if (col.tag=="MainOVR")
		{
			trigCol.enabled=false;
			AlertEnemiesInTriggerArray();
		}

	}
	 void AlertEnemiesInTriggerArray()
	{
		if (!isAlreadyAlerted){
		foreach (GameObject enemy in alertTriggerEnemies)
		{
			enemy.GetComponent<Scr_BasicAI>().boolChase=true;
		}
		isAlreadyAlerted = true;
	}
	}
}
