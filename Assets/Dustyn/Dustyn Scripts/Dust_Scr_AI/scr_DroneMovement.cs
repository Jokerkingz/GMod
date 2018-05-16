using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scr_DroneMovement : MonoBehaviour {

	enum State {Idle, Patrol, Chase, AttackFormation, Dying}
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
	public Scr_HealthScript healthScript;
	private Animation anim;
	private ParticleSystem particleExplosion;
	public LayerMask vLayer;
	//private Rigidbody rgbd;

	[Header("Bools")]
	public bool boolChase;
	private bool isExploding;

	[Header("Floats")]
	public float speed;
	public float floatDistance;
	public float attackDistance;
	public float attackWiggleDistance;
	public float viewAngle;
	public float viewDistance;

	void Awake()
	{
		particleExplosion = GetComponentInChildren<ParticleSystem>();
		healthScript = GetComponentInChildren<Scr_HealthScript>();
		target = GameObject.FindWithTag("MainOVR").transform;
		droneAttack = this.gameObject.GetComponentInChildren<scr_DroneAttack>();
		droneAgent = this.gameObject.GetComponent<NavMeshAgent>();
		anim = this.gameObject.GetComponent<Animation>();
		//rgbd = this.gameObject.GetComponentInChildren<Rigidbody>();
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
			case State.Dying: this.Dying();break;
		}
		movementCurrentState = ""+currentState;

		floatDistance = Vector3.Distance (transform.position, target.transform.position);

		if (!isStationaryGuard){patrolPointDistance =Vector3.Distance (transform.position, patrolPointArray[curPatrolPoint].transform.position);}

		if (healthScript.curHealth< healthScript.maxHealth)
		{
			Alerted();
		}
		//Vector3 NewOwn = transform.position;

        Ray tRay = new Ray(transform.position+transform.up *-.75f, transform.TransformDirection(Vector3.forward*viewDistance));
		Debug.DrawRay (transform.position +transform.up *-.75f, transform.TransformDirection(Vector3.forward*viewDistance), Color.red);
        RaycastHit tHit;

        if (Physics.Raycast(tRay, out tHit, viewDistance, vLayer))
        	{if (tHit.collider.CompareTag("MainOVR"))
        		{Alerted();}
    		}
    	Vector3 direction = target.position - this.transform.position;
     	float angle = Vector3.Angle (direction, this.transform.forward);
    	if (Vector3.Distance (target.position, this.transform.position) < viewDistance && angle<viewAngle)
    		{transform.LookAt(target);}
	}

	void RemovedFixedUpdate(){
	     /*Vector3 direction = target.position - this.transform.position;
	     float angle = Vector3.Angle (direction, this.transform.forward);
        if (Vector3.Distance (target.position, this.transform.position) < viewDistance && angle<viewAngle)
        {
            boolChase=true;
        }*/

        /*RaycastHit hit;
        Debug.DrawRay (transform.position, transform.TransformDirection(Vector3.forward*viewDistance), Color.red);
        if (Physics.Raycast (transform.position, transform.TransformDirection(Vector3.forward*viewDistance), out hit))
        if (hit.collider.CompareTag("MainOVR")) {
        boolChase=true;*/

        Ray tRay = new Ray(transform.position+transform.up *0.75f, transform.TransformDirection(Vector3.forward*viewDistance));
        Debug.DrawRay (transform.position +transform.up *0.75f, transform.TransformDirection(Vector3.forward*viewDistance), Color.red);
        RaycastHit tHit;

        if (Physics.Raycast(tRay, out tHit, viewDistance, vLayer))
        	{if (tHit.collider.CompareTag("MainOVR"))
        		{Alerted();}
    		}
    	Vector3 direction = target.position - this.transform.position;
     	float angle = Vector3.Angle (direction, this.transform.forward);
    	if (Vector3.Distance (target.position, this.transform.position) < viewDistance && angle<viewAngle)
    		{transform.LookAt(target);}
	}

	void Idle()
	{	
		if (healthScript.curHealth <=0) {currentState=State.Dying;}
	
		droneAttack.isMoving=true;
		if(boolChase){currentState=State.Chase;}
		return;
	}
	void Patrol()
	{
		//navyMeshy.speed = floatSpeedHolder;
		if (healthScript.curHealth <=0) {currentState=State.Dying;}
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
		if (healthScript.curHealth <=0) {currentState=State.Dying;}
		if (floatDistance<=attackDistance) {currentState= State.AttackFormation;}
		gameObject.GetComponent<NavMeshAgent>().SetDestination (target.transform.position);
		droneAttack.isMoving=true;
		droneAgent.speed =speed;
		return;
	}

	void AttackFormation()
	{
		if (healthScript.curHealth <=0) {currentState=State.Dying;}
		if (floatDistance> attackWiggleDistance){currentState= State.Chase;}
		droneAttack.isMoving=false;
		droneAgent.speed =0;
		transform.LookAt(target);
		return;
	}

	void Dying()
	{		
		anim.Play("ani_droneDead2");
		//rgbd.isKinematic=false;
		//rgbd.useGravity=true;
		droneAttack.isDeadStopAll=true;
		boolChase =false;
		droneAgent.speed=0;
		droneAttack.enemyShoot.enabled=false;
		this.gameObject.GetComponent<Collider>().enabled=false;
		//StartCoroutine(ExplosionTimer());
		Destroy(this.gameObject, 3f);
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

	public void PlayExplosion()
	{
		if(!isExploding)
		{particleExplosion.Play();
		isExploding=true;}
	}
	IEnumerator ExplosionTimer()
	{
		yield return new WaitForSeconds (1.3f);
		PlayExplosion();
		
	}

}
