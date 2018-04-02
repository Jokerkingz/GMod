using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_roomSwitch : MonoBehaviour {

	public GameObject prevDoor;
	
	public GameObject nextDoor;
	public float waitTimer;
	private Collider trigCollider;
	
	public bool doesUntagFloors;
	public GameObject[] floorTiles;

	void Start () {
		trigCollider = this.gameObject.GetComponent<Collider>();

		/*if (doesUntagFloors)
		{floorTiles = GameObject.FindGameObjectsWithTag("Floor");}*/
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.tag=="MainOVR")
		{
		if (prevDoor !=null){
		prevDoor.GetComponent<Scr_Door>().DoorClose();
		prevDoor.GetComponent<Collider>().enabled=true;
		}
		if (nextDoor !=null){
		StartCoroutine(UnlockNextDoorTimer());
		}
		trigCollider.enabled=false;

		if (doesUntagFloors)
		{UntagFloor();}

		}
	}

	IEnumerator UnlockNextDoorTimer()
	{
		yield return new WaitForSeconds(waitTimer);
		nextDoor.GetComponent<Scr_Door>().DoorUnlocked();

	}

	void UntagFloor()
	{
		foreach (GameObject floorTile in floorTiles )
		if (floorTile.tag == "Floor")
		{floorTile.tag  = "Untagged";}
	}
}

