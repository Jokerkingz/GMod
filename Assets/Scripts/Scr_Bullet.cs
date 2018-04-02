using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Bullet : MonoBehaviour {
	public Rigidbody cRB; // must be public
	public float vSpeedMultiplier; // must be public
	public Vector3 vFixedAngle; // must be public
	public Collider vColliderToSkip; // must be public
	public GameObject vGameObjectToSkip; // must be public
	private Vector3 vPreviousPosition;
	public LayerMask vLayer;
	public GameObject vSpark;
	void Start(){
		cRB.velocity = (transform.TransformDirection(Vector3.up))*vSpeedMultiplier;
		vPreviousPosition = transform.position;
	}
	void Update(){
//		vSpeed = cRB.velocity;
		Ray tRay = new Ray(this.transform.position,vPreviousPosition-this.transform.position);
		RaycastHit tHit;
		float tDistance = Vector3.Distance(vPreviousPosition,this.transform.position);
		//Physics.Raycast(this.transform.position,vPreviousPosition,out tHit,vLayer) // beefore using ray
		Debug.DrawRay(this.transform.position,vPreviousPosition-this.transform.position,Color.white);
		if (Physics.Raycast(tRay,out tHit,tDistance,vLayer)){
			if (tHit.collider.gameObject != vGameObjectToSkip && tHit.collider.tag != "Bullet"){
				fHit(tHit.collider.gameObject,tHit.point,tHit.rigidbody);
				}
			}
		vPreviousPosition = this.transform.position;
		}
	void fHit(GameObject tObj, Vector3 tPoint, Rigidbody tOther){
		if (tObj.tag == "Target")
			tObj.SendMessage("fHit");
		GameObject tTEmp = Instantiate(vSpark);
		tTEmp.transform.position = tPoint;
		tTEmp.GetComponent<Scr_DestroyTime>().fStartTimer(.9f);
		//Rigidbody tRB = tOther.GetComponent<Rigidbody>();
		if (tOther != null)
			tOther.AddExplosionForce(10f,tPoint,10f,5f);
		Destroy(this.gameObject);
	}
	void OnCollisionEnter(Collision tOther){
		fHit(tOther.collider.gameObject,tOther.contacts[0].point,tOther.rigidbody);
		//
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
