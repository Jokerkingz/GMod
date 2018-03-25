using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Bullet : MonoBehaviour {
	public Rigidbody cRB; // must be public
	public float vSpeedMultiplier; // must be public
	public Vector3 vFixedAngle; // must be public
	public Collider vColliderToSkip; // must be public
	private Vector3 vPreviousPosition;
	public LayerMask vLayer;
	void Start(){
		cRB.velocity = (transform.TransformDirection(Vector3.up))*vSpeedMultiplier;
		vPreviousPosition = transform.position;
	}
	void FixedUpdate(){
//		vSpeed = cRB.velocity;
		Ray tRay = new Ray(this.transform.position,vPreviousPosition);
		RaycastHit tHit;
		//Physics.Raycast(this.transform.position,vPreviousPosition,out tHit,vLayer) // beefore using ray
		Physics.Raycast(tRay,out tHit,vLayer);
		}
	void fHit(Vector3 tPoint){
		cRB.AddExplosionForce(5f,tPoint,0f);
		Destroy(this.gameObject);
	}
	void OnCollisionEnter(Collision tOther){
		fHit(tOther.contacts[0].point);
		//Rigidbody tRB = tOther.attachedRigidbody;
	}
/*
	public Rigidbody cRB;
	public float vSpeedMultiplier = 10f;
	public GameObject vModelToTurn;
	private int vSolidify = 2;
	private AudioSource cAS;
	public AudioClip vSFX;
	// Use this for initialization
	void Start () {
		cAS = this.GetComponent<AudioSource>();
		cAS.PlayOneShot(vSFX,.025f);
		cRB = GetComponent<Rigidbody>();
		cRB.velocity = transform.TransformDirection(Vector3.forward * vSpeedMultiplier);
		Invoke("Dead",5f);
	}
	
	// Update is called once per frame
	void Update () {
		if (vSolidify > 0){
			vSolidify --;
			if (vSolidify==0)
				Solididy();
		}
		transform.LookAt(transform.position+cRB.velocity);
		cRB.velocity = transform.TransformDirection(Vector3.forward * vSpeedMultiplier);
	}

	void Solididy(){
		foreach (Collider tThat in this.GetComponentsInChildren<Collider>()){
			tThat.enabled = true;
		}
	}

	void OnCollisionEnter(Collision tOther){
		if (tOther.gameObject.tag == "Wall" || tOther.gameObject.tag == "Untagged")
			Destroy(this.gameObject);
		if (tOther.gameObject.tag == "Player" || tOther.gameObject.tag == "AI"){
			tOther.gameObject.BroadcastMessage("Hit",SendMessageOptions.DontRequireReceiver);
			Destroy(this.gameObject);
			}
	}
	void Dead(){
		Destroy(this.gameObject);

	}*/
}
