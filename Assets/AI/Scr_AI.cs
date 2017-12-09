using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scr_AI : MonoBehaviour {
	public GameObject vTarget;

	public Scr_Shoot cS;
	public GameObject vPlayer;
	public LayerMask vWallLayers;
	public bool tBool;

	// Animation Settings
	private Animator cAC;
	public bool vIsAlert;
	public float vAniSpeed; 
	public bool vIsDead;

	// AI
	public NavMeshAgent cNMA;
	public string vAIStatus;
	public float vMinDistance;
	public float vShootCD;
	public Vector3 vDestination;
	public LayerMask vViewLayerMask;
	public GameObject[] vPatrolList;
	public int vPrevious = -1;
	// Use this for initialization
	void Start () {
		vPlayer = GameObject.FindGameObjectWithTag("Player");
		cNMA = GetComponent<NavMeshAgent>();
		cAC = GetComponent<Animator>();
		cS = GetComponentInChildren<Scr_Shoot>();
		vAIStatus = "Idle";
	}
	
	// Update is called once per frame
	void Update () {
		cAC.SetFloat("Speed",cNMA.speed);
		switch (vAIStatus){
		case "Idle": fActIdle(); break;
		case "Patrol": fActPatrol(); break;
		case "Alert":  fActAlert(); break;
		case "GetCloser": fActGetCloser (); break;
		}
	}
	void fActIdle(){
		cNMA.speed = 0f;
		if (Random.value < .1f){
			vAIStatus = "Patrol";
			if (vPatrolList.Length > 0){
				int vRandomChoice = Random.Range(0,vPatrolList.Length);
				if (vPrevious != vRandomChoice){
					vDestination = vPatrolList[vRandomChoice].transform.position;
					vPrevious = vRandomChoice;
					}
				cNMA.destination = vDestination;
				cNMA.speed = 1.2f;
				}
			else
				vAIStatus = "Idle";
			}
		if (vIsAlert)
			vAIStatus = "Alert";
	}
	void fActPatrol (){
		cNMA.speed = 1.2f;
		if (cNMA.remainingDistance <= .5f){
			
			vAIStatus = "Idle";
			}
		if (vIsAlert)
			vAIStatus = "Alert";
	}
	void fActAlert (){
		cAC.SetBool("Alert",true);
		cNMA.destination = vPlayer.transform.position;
		cNMA.updateRotation = true;
		float tX = cNMA.destination.x - this.transform.position.x;
		float tY = cNMA.destination.z - this.transform.position.z;
		float tAngle = Mathf.Atan2(tX,tY)*180/Mathf.PI;
		Quaternion tTemp = Quaternion.Euler(0f,tAngle,0f);
		this.transform.rotation =	Quaternion.RotateTowards(this.transform.rotation,tTemp,15f);

		if (cNMA.remainingDistance > vMinDistance){
			cNMA.speed = 2f;
			vAIStatus = "GetCloser";
		}
		fIsInSightShoot();
	}
	void fIsInSightShoot(){
		Vector3 vDirection;
		vDirection = (vPlayer.transform.position-transform.position);
		if (!Physics.Raycast(transform.position,vDirection,25f,vWallLayers)){
			this.BroadcastMessage("Triggered");
			}
	}
	void fActGetCloser (){
		cNMA.destination = vPlayer.transform.position;
		cNMA.updateRotation = true;
		if (cNMA.remainingDistance <= vMinDistance){
			cNMA.speed = 0f;
			vAIStatus = "Alert";
			}
	}

	void ActFindPlayer(){
		cNMA.destination = vPlayer.transform.position;
		//Ray tRay = new Ray(transform.position,vPlayer.transform.position);
		Vector3 vDirection;
		vDirection = (vPlayer.transform.position-transform.position);
		Debug.DrawRay(transform.position,vDirection,Color.blue);
		if (!Physics.Raycast(transform.position,vDirection,25f,vWallLayers))
		{
			vAIStatus = "ShootPlayer";
			}
		if (cS.vAmmo <= 0){
			vAIStatus = "FindAmmo";
		} 
	}

	void ActShoot(){
		//Ray tRay = new Ray(transform.position,vPlayer.transform.position);
		Vector3 vDirection;
		vDirection = (vPlayer.transform.position-transform.position);
		Debug.DrawRay(transform.position,vDirection,Color.red);
		if (Physics.Raycast(transform.position,vDirection,25f,vWallLayers)){
			vAIStatus = "Idle";
			}
		cNMA.destination = transform.position;
		transform.LookAt(vPlayer.transform);
		if (!Physics.Raycast(transform.position,vDirection,25f,vWallLayers)){
			this.BroadcastMessage("Triggered");
		}
		if (cS.vAmmo <= 0){
			vAIStatus = "FindAmmo";
		}
		//if (Physics.Raycast(Vector3,
		
	}

	void ActReload(){
		if (GameObject.FindGameObjectsWithTag("Ammo").Length > 0)
		cNMA.destination = GameObject.FindGameObjectWithTag("Ammo").transform.position;
		if (cS.vAmmo > 0){
			vAIStatus = "Idle";
		}
	}

	void OnTriggerEnter(Collider tObject){
		if (tObject.tag == "Ammo"){
			vAIStatus = "Idle";
			this.BroadcastMessage("Reload");
			Destroy(tObject.gameObject);
		}
	}
	public void vGetAlerted(){
		vIsAlert = true;
	}
}
