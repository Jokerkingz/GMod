using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_drone : MonoBehaviour {

	enum State {Enter, RotatingToPlayer, RotatingToSwitch, Hover, Attack, Switch, Flinch, Reset, Death}
	State currentState;
	public string enemyCurrentState;

	[Header("References")]
	public Transform player;
	public Transform homePosition;
    public Transform lookAtSystem;

	public scr_droneAnchor droneAnchor;
	public scr_droneEntry droneEntry;
	public scr_droneStats droneStats;
	public scr_droneSwitch droneSwitch;

	[Header ("Hover Pattern Properties")]
	public Transform[] hoverPoints;
	public int curHoverPoint;
	public int maxHoverPoint;
	public int usedHoverPoint;

	[Header ("SIMPLIFIED Hover Pattern Properties")]
	public Transform[] simpleHoverPoints;
	public int curSimpleHoverPoint;
	public int maxSimpleHoverPoint;


    /*[Header("Speed Settings")]
	public float vHoverSpeed;*/
    [Header("Guns")]
    public Scr_ModHandle leftGun;
    public Scr_ModHandle rightGun; 

	[Header("Entry State")]
	public float vCurDistance;
	public float vEngageDistance;

	[Header("Switch State")]
	public bool isSwitching;

	void Start () {
		player = GameObject.FindWithTag("MainOVR").transform;
		if (player ==null){return;}
		
		droneAnchor = GetComponentInParent<scr_droneAnchor>();
		droneEntry =GetComponentInParent<scr_droneEntry>();
		droneStats = GetComponentInParent<scr_droneStats>();
		//droneSwitch= this.gameObject.GetComponent<scr_droneSwitch>();
		vEngageDistance = Random.Range(droneStats.minDistancefromPlayer, droneStats.maxDistancefromPlayer);
		
		this.currentState = State.Enter;

		//HOVER POINTS
		if (!droneStats.simplifiedHover){
			maxHoverPoint = hoverPoints.Length;
			curHoverPoint = Random.Range(0, maxHoverPoint);
		}
		if(droneStats.simplifiedHover){
			maxSimpleHoverPoint= simpleHoverPoints.Length;
			curSimpleHoverPoint= Random.Range(0, maxSimpleHoverPoint);
		}
		vCurDistance=999f;

       // leftGun = GetComponent<Scr_ModHandle>();
        //rightGun = GetComponent<Scr_ModHandle>();
	}
	
	void Update () {

		switch (this.currentState)
		{
			case State.Enter: this.Enter(); break;
			case State.RotatingToPlayer: this.RotatingToPlayer();break;
			case State.Hover: this.Hover(); break;
            case State.RotatingToSwitch: this.RotatingToSwitch();break;
			//case State.Attack: this.Attack(); break;
			case State.Switch: this.Switch();break;
			//case State.Flinch: this.Flinch();break;
			case State.Reset: this.Reset();break;
			//case State.Death: this.Death();break;
		}

		enemyCurrentState = "" + currentState;
		
		//TESTING
		if (Input.GetKeyDown(KeyCode.Space))
		{this.currentState= State.Reset;}


		if (!droneStats.simplifiedHover){
			if (curHoverPoint > maxHoverPoint) {curHoverPoint = 0;}
			if (curHoverPoint==usedHoverPoint) {curHoverPoint = Random.Range(0, maxHoverPoint);}
		}
		
	}

	void Enter()
	{
		this.transform.LookAt(player);
		droneEntry.vEntering=true;
		vCurDistance = Vector3.Distance (this.transform.position, player.transform.position);
		
		if (vCurDistance <= vEngageDistance) {this.currentState = State.RotatingToPlayer;}
		return;
	}
	void Hover()
	{
        //SHOOTING IN THIS STATE
        leftGun.fTriggerPressed();
        rightGun.fTriggerPressed();
		droneEntry.vEntering=false;
		droneAnchor.vHoverMode=true;
        //this.transform.LookAt(player);
        float step = droneStats.rotateTowardsSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAtSystem.rotation, step);

        if (!droneStats.simplifiedHover){
			transform.position = Vector3.MoveTowards (this.transform.position, hoverPoints[curHoverPoint].position,Time.deltaTime*droneStats.hoverSwitchSpeed);
			if (this.transform.position == hoverPoints[curHoverPoint].position)
			{
			usedHoverPoint = curHoverPoint;
			curHoverPoint = Random.Range(0, maxHoverPoint);
			//this.currentState=State.Reset;
			}
		}

		if (droneStats.simplifiedHover)
		{
			transform.position = Vector3.MoveTowards (this.transform.position, simpleHoverPoints[curSimpleHoverPoint].position,Time.deltaTime*droneStats.hoverSwitchSpeed);
		}

		if (isSwitching){this.currentState= State.Switch;}
		return;
	}
	
	void RotatingToPlayer()
	{
        float step = droneStats.rotateTowardsSpeed * Time.deltaTime;
        lookAtSystem.LookAt(player);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAtSystem.rotation , step);


        RaycastHit hit;
        Debug.DrawRay(transform.position + transform.up * 0.75f, transform.TransformDirection(Vector3.forward * 10), Color.red);
        if (Physics.Raycast(transform.position + transform.up * 0.75f, transform.TransformDirection(Vector3.forward * 10), out hit))
            if (hit.collider.CompareTag("MainOVR")) { this.currentState = State.Hover; }
        return;
    }

    void RotatingToSwitch()
    {
        /*float step = droneStats.rotateTowardsSpeed * Time.deltaTime;
        lookAtSystem.LookAt(player);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAtSystem.rotation, step);


        RaycastHit hit;
        Debug.DrawRay(transform.position + transform.up * 0.75f, transform.TransformDirection(Vector3.forward * 10), Color.red);
        if (Physics.Raycast(transform.position + transform.up * 0.75f, transform.TransformDirection(Vector3.forward * 10), out hit))
            if (hit.collider.CompareTag("MainOVR")) { this.currentState = State.Hover; }*/
        return;
    }

	void Switch()
	{
        float step = droneStats.rotateTowardsSpeed * Time.deltaTime;
        if (droneSwitch == null)
            Debug.Log("PENIS");
        Debug.Log(droneSwitch.name + " Found");
        Debug.Log(droneSwitch.switchPoints[droneSwitch.curSwitchPoint].name + " point Found");
        lookAtSystem.LookAt(droneSwitch.switchPoints[droneSwitch.curSwitchPoint].position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAtSystem.rotation, step);

		//droneSwitch.Switching();
		if (!isSwitching){this.currentState=State.RotatingToPlayer;}
		return;
	}
	public void Reset()
	{
		droneAnchor.vHoverMode=false;
		transform.position = Vector3.MoveTowards (this.transform.position, homePosition.position,Time.deltaTime*droneStats.resetSpeed);
		if (this.transform.position == homePosition.position &&!isSwitching){ this.currentState = State.RotatingToPlayer; }
		if (this.transform.position == homePosition.position &&isSwitching){ this.currentState = State.Switch;}
		return;
	}
}
