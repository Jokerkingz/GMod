﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_elevatorTrigger : MonoBehaviour {

	public bool exteriorTrigger;
	public bool interiorTrigger;
	public GameObject elevatorPrefab;
	public GameObject elevatorMusicBox;
	private Collider trigCollider;
	void Start () {
		trigCollider = this.gameObject.GetComponent<Collider>();
	}
	
	
	void Update () {
		
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag=="MainOVR")
		{
			if (exteriorTrigger)
			{
				elevatorPrefab.GetComponent<scr_Elevator>().ElevatorDoorOpen();
			}
			if (interiorTrigger)
			{
				elevatorPrefab.GetComponent<scr_Elevator>().ElevatorDoorClose();
				StartCoroutine(WaitForDoorBeforeStartingElevator());
				elevatorMusicBox.GetComponent<AudioSource>().Play();
			}
		trigCollider.enabled=false;
		}
	}
	IEnumerator WaitForDoorBeforeStartingElevator()
	{
		yield return new WaitForSeconds(2);
		elevatorPrefab.GetComponent<scr_Elevator>().ElevatorStart();
	}
}
