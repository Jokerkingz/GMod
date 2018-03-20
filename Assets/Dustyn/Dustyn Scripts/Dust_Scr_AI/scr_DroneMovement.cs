﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scr_DroneMovement : MonoBehaviour {

	enum State {Idle, Patrol, Chase, AttackFormation}
	State currentState;
	public string movementCurrentState;

	[Header("Guard Style: Stationary")]
	public bool isStationaryGuard;

	[Header("Guard Style: PatrolPoints")]
	public float patrolPointDistance; 
	public float patrolPointSwitchDistance;
	public int curPatrolPoint;
	public int maxPatrolPoint;
	public Transform[] patrolPointArray;
	[Tooltip("For the Patrol Guard, only change the array below. Do not touch the values above")]
	
	
	[Header("References")]
	public Transform target;
	public scr_DroneAttack droneAttack;
	public NavMeshAgent droneAgent;

	[Header("Bools")]
	public bool boolChase;

	[Header("Floats")]
	public float speed;
	public float floatDistance;
	public float attackDistance;
	public float attackWiggleDistance;
	public float viewAngle;
	public float viewDistance;

	void Awake()
	{
		target = GameObject.FindWithTag("Player").transform;
		droneAttack = this.gameObject.GetComponentInChildren<scr_DroneAttack>();
		droneAgent = this.gameObject.GetComponent<NavMeshAgent>();
	}
	void Start () {
		if (isStationaryGuard){currentState=State.Idle;}
		else if (!isStationaryGuard){currentState=State.Patrol;}
		attackWiggleDistance= attackDistance+2;
		droneAgent.speed = speed;

		curPatrolPoint=0;
		maxPatrolPoint = patrolPointArray.Length;
		patrolPointSwitchDistance=2;
	}
	
	
	void Update () {
		switch (this.currentState)
		{	
			case State.Idle: this.Idle(); break;
			case State.Patrol: this.Patrol(); break;
			case State.Chase: this.Chase(); break;
			case State.AttackFormation: this.AttackFormation();break;
		}
		movementCurrentState = ""+currentState;

		floatDistance = Vector3.Distance (transform.position, target.transform.position);

		if (!isStationaryGuard){patrolPointDistance =Vector3.Distance (transform.position, patrolPointArray[curPatrolPoint].transform.position);}
	}

	void FixedUpdate()
	{
	 Vector3 direction = target.position - this.transform.position;
	 float angle = Vector3.Angle (direction, this.transform.forward);
		if (Vector3.Distance (target.position, this.transform.position) < viewDistance && angle<viewAngle)
		{
			boolChase=true;
		}
	}
	void Idle()
	{	
		droneAttack.isMoving=true;
		if(boolChase){currentState=State.Chase;}
		return;
	}
	void Patrol()
	{
		//navyMeshy.speed = floatSpeedHolder;
		droneAttack.isMoving=true;
		if ( boolChase){currentState= State.Chase;}
		
		if (patrolPointDistance<=patrolPointSwitchDistance)
		{
			Debug.Log ("Off to the next point");
			curPatrolPoint++;
			GoToNextPoint();
		}
		if (curPatrolPoint>= maxPatrolPoint)
		{
			curPatrolPoint=0;
		}
		return;
	}

	void Chase()
	{	
		if (floatDistance<=attackDistance) {currentState= State.AttackFormation;}
		gameObject.GetComponent<NavMeshAgent>().SetDestination (target.transform.position);
		droneAttack.isMoving=true;
		droneAgent.speed =speed;
		return;
	}

	void AttackFormation()
	{
		if (floatDistance> attackWiggleDistance){currentState= State.Chase;}
		droneAttack.isMoving=false;
		droneAgent.speed =0;
		transform.LookAt(target);
		return;
	}


	void GoToNextPoint()
	{
		if (curPatrolPoint<maxPatrolPoint)
		{ droneAgent.destination = patrolPointArray[curPatrolPoint].position;}
		else if (curPatrolPoint>= maxPatrolPoint)
		{
			droneAgent.destination = patrolPointArray[0].position;
			curPatrolPoint=0;
		}
	
	
	}
	public void Alerted()
	{
		boolChase=true;
	}

}
