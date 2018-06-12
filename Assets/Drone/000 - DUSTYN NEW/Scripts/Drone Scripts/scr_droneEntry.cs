using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_droneEntry : MonoBehaviour {

	public bool vEntering;
	private scr_droneStats droneStats;
	private scr_drone droneSource;
	void Start () {
		droneStats = GetComponentInParent<scr_droneStats>();
		droneSource = GetComponentInChildren<scr_drone>();
	}

	void Update () {
		if (vEntering)
		{	
		float step = droneStats.entrySpeed*Time.deltaTime;
		this.transform.position = Vector3.MoveTowards(this.transform.position, droneSource.player.position, step);
		}
	}
}
