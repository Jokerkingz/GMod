using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Player : MonoBehaviour {

	public float vHSpeedMod = 2f;
	public float vVSpeedMod = 2f;
	public float vVelocityMultiplier = 2f;



	private float vYaw = 0f;
	private float vPitch = 0f;

	private float vYSpeed = 0f;
	public Vector3 vVelocity;
	private GameObject vCamera;

	private CharacterController cCC;
	// Use this for initialization
	public bool vAlive = true;
	void Start () {
		vCamera = GameObject.FindGameObjectWithTag("MainCamera");
		cCC = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (vAlive){
		vYaw += vHSpeedMod * Input.GetAxis("Mouse X");
		vPitch -= vVSpeedMod * Input.GetAxis("Mouse Y");
		vCamera.transform.eulerAngles = new Vector3(vPitch, vYaw, 0f);
		transform.eulerAngles = new Vector3(0f, vYaw, 0f);

		//if (Input.GetMouseButton(0))
		//	this.BroadcastMessage("Triggered");
		InputCheck();
		}
		GroundCheck();
		CheckCycle();



	}
	void InputCheck(){
		vVelocity.x =vVelocityMultiplier * Input.GetAxis("Horizontal");
		vVelocity.z = vVelocityMultiplier * Input.GetAxis("Vertical");
		vVelocity = transform.TransformDirection(vVelocity);

	}
	void GroundCheck(){
		if (!cCC.isGrounded)
			vYSpeed = -1f;
		else
			vYSpeed = 0f;
	}
	void CheckCycle(){
		vPitch = Mathf.Clamp(vPitch,-90f,90f);
		vYSpeed = Mathf.Clamp(vYSpeed,-10f,10f);
		vVelocity.y = vYSpeed*vVelocityMultiplier;
		cCC.Move(vVelocity*Time.deltaTime);
	}
	void ShowLose(){
		vAlive = false;


	}
	void OnTriggerEnter(Collider tObject){
		if (tObject.tag == "Ammo"){
			this.BroadcastMessage("Reload");
			//GameObject[] tList = GameObject.FindGameObjectsWithTag("AmmoSpawner");
			//foreach (GameObject tThose in tList){
			//	tThose.SendMessage("StartTimer");
			//}
			Destroy(tObject.gameObject);
		}
	}
}
