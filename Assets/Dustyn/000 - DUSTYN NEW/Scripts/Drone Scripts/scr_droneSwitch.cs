using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_droneSwitch : MonoBehaviour {

	private scr_droneStats droneStats;
	private scr_drone droneSource;
	private scr_droneAnchor droneAnchor;
	[Header("Switch Points")]
	public Transform[] switchPoints;
	public int curSwitchPoint;
	public int maxSwitchPoint;
	public int usedSwitchPoint;


	[Header("Floats")]
	public bool isSwitching;
	public float switchTime;
	public float switchTimer;
	void Start () {
		droneSource= GetComponentInChildren<scr_drone>();
		droneStats = GetComponentInParent<scr_droneStats>();
		droneAnchor = this.gameObject.GetComponent<scr_droneAnchor>();
		switchTime = Random.Range(droneStats.minSwitchTime, droneStats.maxSwitchTime);

		maxSwitchPoint = switchPoints.Length;
		curSwitchPoint = Random.Range(0, maxSwitchPoint);
	}
	
	void Update () {

		//CHECKITY CHECK
		if (curSwitchPoint > maxSwitchPoint) {curSwitchPoint = 0;}
		if (curSwitchPoint==usedSwitchPoint) {curSwitchPoint = Random.Range(0, maxSwitchPoint);}

		//Start timer
		if (droneAnchor.vHoverMode)
		{switchTimer+=Time.deltaTime;}

		if (switchTimer>=switchTime)
		{
			droneAnchor.vHoverMode=false;
			droneSource.isSwitching=true;
			droneSource.Reset();
			Switching();

		}
	}

	void Switching()
	{
		droneSource.transform.LookAt(switchPoints[curSwitchPoint].position);
		transform.position = Vector3.MoveTowards (this.transform.position, switchPoints[curSwitchPoint].position,Time.deltaTime*droneStats.switchSpeed);
		if (this.transform.position == switchPoints[curSwitchPoint].position)
			{
			
			switchTimer=0;
			switchTime = Random.Range(droneStats.minSwitchTime, droneStats.maxSwitchTime);
			usedSwitchPoint = curSwitchPoint;
			curSwitchPoint = Random.Range(0, maxSwitchPoint);
			droneSource.isSwitching=false;
			}
	}
}
