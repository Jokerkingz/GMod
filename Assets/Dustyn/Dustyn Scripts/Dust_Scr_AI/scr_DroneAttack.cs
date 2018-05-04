using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_DroneAttack : MonoBehaviour {

	enum State {Moving, Switch, Attack, Dying}
	State currentState;
	public string droneCurrentState;

	[Header("Pattern Points")]
	public Transform centerPoint;
	public Transform[] switchPoints;
	public Transform[] validPoints;

	[Header ("Pattern Properties")]
	public int curSwitchPoint;
	public int maxSwitchPoint;
	public int usedSwitchPoint;
	public float switchSpeed;
	public float switchTime;
	public float switchTimer;
	[Header("Bools")]
	public bool isSwitching;
	public bool isMoving;
	public bool isDeadStopAll;

	[Header("References")]
	public Transform target;
	public Scr_EnemyShoot enemyShoot;
	//public Scr_HealthScript healthScript;

	void Awake () {

		target= GameObject.FindWithTag("MainOVR").transform;
		enemyShoot= this.gameObject.GetComponentInChildren<Scr_EnemyShoot>();
	//	healthScript = this.gameObject.GetComponent<Scr_HealthScript>();
		this.currentState= State.Moving;
	}

	void Start()
	{
		isMoving=true;
		maxSwitchPoint = switchPoints.Length;
		curSwitchPoint = Random.Range(0, maxSwitchPoint);
		//usedSwitchPoint = Random.Range(0, maxSwitchPoint);
	}
	
	void Update()
	{
		switch (this.currentState)
		{
			case State.Moving: this.Moving(); break;
			case State.Switch: this.Switch(); break;
			case State.Attack: this.Attack(); break;
			case State.Dying: this.Dying(); break;
		}
		droneCurrentState = "" + currentState;

		if (curSwitchPoint > maxSwitchPoint) 
		{curSwitchPoint = 0;}

		//PREVENTS DRONE FROM PICKING THE SAME SWITCHPOINT TWICE IN A ROW
		if (curSwitchPoint==usedSwitchPoint) {curSwitchPoint = Random.Range(0, maxSwitchPoint);}
		
		/*if (this.transform.position == switchPoints [curSwitchPoint].position)
		{Reset ();}*/
//--------------------move this shit around
		/*if (attackMode) {
			switchTimer += Time.deltaTime;
			transform.LookAt(target);

			

			if (switchTime <= switchTimer && !isSwitching) {
				isSwitching = true;
				switchTimer = 0;
			}


			
		
			if (isSwitching) {
				Switch ();
			}

		}

		if (!attackMode) 
		{
			transform.position = Vector3.MoveTowards (this.transform.position, centerPoint.position,Time.deltaTime*switchSpeed);
			switchTimer = 0;
		}*/
		
	}
	void Moving()
	{
		if (isDeadStopAll){currentState=State.Dying;}
		enemyShoot.isShooting=false;
		transform.position = Vector3.MoveTowards (this.transform.position, centerPoint.position,Time.deltaTime*switchSpeed);
		switchTimer = 0;
		//isSwitching=false;
		if (!isMoving){currentState = State.Switch;}

		return;
	}
	void Switch()
	{
		if (isDeadStopAll){currentState=State.Dying;}
		//isMoving=false;
		if (isMoving){currentState=State.Moving;}
		transform.LookAt(switchPoints[curSwitchPoint].position);
		enemyShoot.isShooting=false;
		transform.position = Vector3.MoveTowards (this.transform.position, switchPoints[curSwitchPoint].position,Time.deltaTime*switchSpeed);
		if (this.transform.position == switchPoints [curSwitchPoint].position)
		{
			usedSwitchPoint = curSwitchPoint;
			curSwitchPoint = Random.Range(0, maxSwitchPoint);
			currentState=State.Attack;
		}
		return;
	}

	void Attack()
	{
		if (isDeadStopAll){currentState=State.Dying;}
		//isMoving=false;
		if (isMoving){currentState=State.Moving;}
		if (0== switchTimer){isSwitching=false;}
		transform.LookAt(target);
		enemyShoot.isShooting=true;
		switchTimer += Time.deltaTime;
		if (switchTime <= switchTimer && !isSwitching) 
		{
			isSwitching = true;
			switchTimer = 0;
				
		}
		if (isSwitching)
		{
			currentState=State.Switch;
		}
		return;
	}

	void Dying()
	{
		enemyShoot.isShooting=false;
		isMoving=false;
		isSwitching=false;
		switchSpeed=0;
		
	}

	void Reset()
	{
		//isSwitching = false;
		//usedSwitchPoint = curSwitchPoint;
		curSwitchPoint = Random.Range(0, maxSwitchPoint);
		//if (curSwitchPoint==usedSwitchPoint) {Random.Range(0, maxSwitchPoint);}
		currentState=State.Switch;
		//switchTimer=0;
	}

		
}


