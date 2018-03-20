using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_roomSwitch : MonoBehaviour {

	//public GameObject invisibleWall;
	public GameObject prevDoor;
	public GameObject nextDoor;
	public float waitTimer;
	private Collider trigCollider;
	void Start () {
		trigCollider = this.gameObject.GetComponent<Collider>();
	}
	

	void OnTriggerEnter(Collider col)
	{
		if (col.tag=="Player")
		{
		prevDoor.GetComponent<scr_doorGoal>().DoorClose();
		prevDoor.GetComponent<Collider>().enabled=true;
		//invisibleWall.GetComponent<Collider>().enabled=true;
		trigCollider.enabled=false;
		StartCoroutine(UnlockNextDoorTimer());
		
		}
	}

	IEnumerator UnlockNextDoorTimer()
	{
		yield return new WaitForSeconds(waitTimer);
		nextDoor.GetComponent<scr_doorGoal>().DoorUnlocked();

	}
}
