using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_roomSwitch : MonoBehaviour {

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
		if (prevDoor !=null){
		prevDoor.GetComponent<Scr_Door>().DoorClose();
		prevDoor.GetComponent<Collider>().enabled=true;
		}
		if (nextDoor !=null){
		StartCoroutine(UnlockNextDoorTimer());
		}
		trigCollider.enabled=false;

		}
	}

	IEnumerator UnlockNextDoorTimer()
	{
		yield return new WaitForSeconds(waitTimer);
		nextDoor.GetComponent<Scr_Door>().DoorUnlocked();

	}
}
