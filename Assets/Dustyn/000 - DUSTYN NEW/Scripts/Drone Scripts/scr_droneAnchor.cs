using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_droneAnchor : MonoBehaviour {
	//enum State {Idle, HoverCycle}
	private scr_droneStats droneStats;
	public bool vHoverMode;
	public float vRotateSpeed;

	public float reverseTimer;
	public float reverseTime;

	void Start () {
		droneStats = GetComponentInParent<scr_droneStats>();
		vRotateSpeed = droneStats.hoverRotationalSpeed;
		reverseTime = Random.Range(droneStats.minReverseTime,droneStats.maxReverseTime);
	}
	void Update () {

		if(vHoverMode && !droneStats.simplifiedHover)
		{
		this.gameObject.transform.Rotate(Vector3.up * vRotateSpeed* Time.deltaTime);
		this.gameObject.transform.Rotate(Vector3.right * vRotateSpeed* Time.deltaTime);

		reverseTimer+=Time.deltaTime;
		}

		if (reverseTimer>=reverseTime)
		{
			vRotateSpeed = vRotateSpeed*-1;
			reverseTimer=0;
			reverseTime = Random.Range(droneStats.minReverseTime,droneStats.maxReverseTime);
		}


		//	SIMPLE HOVERING
		if(vHoverMode && droneStats.simplifiedHover)
		{
		this.gameObject.transform.Rotate(Vector3.up * vRotateSpeed* Time.deltaTime);
		}
	}
}
