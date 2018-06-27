using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Bullet : MonoBehaviour {
	public Rigidbody cRB; // must be public
	public float vSpeedMultiplier; // must be public
	public Vector3 vFixedAngle; // must be public
	public Collider vColliderToSkip; // must be public
	public GameObject vGameObjectToSkip; // must be public
	public Vector3 vPreviousPosition; // must be public
	public LayerMask vLayer;
	public GameObject vSpark;
	public GameObject vTrailSource;
	private GameObject vTrail;
    public float vDamage = 3f;
	public Vector3 vTilt;
	void Start(){
		cRB.velocity = (transform.TransformDirection(Vector3.up))*vSpeedMultiplier*2f;
		vPreviousPosition = transform.position;
			vTrail = Instantiate(vTrailSource,this.transform);
		//vTilt = transform.eulerAngles;
	}
	void Update(){
		if (vTrail==null){
			vTrail = Instantiate(vTrailSource,this.transform);
			//vTrail = Instantiate(vTrailSource);
			}
		Ray tRay = new Ray(this.transform.position,this.transform.position-vPreviousPosition);
		RaycastHit tHit;
		float tDistance = Vector3.Distance(vPreviousPosition,this.transform.position);
		if (Physics.Raycast(tRay,out tHit,tDistance,vLayer)){
			if (tHit.collider.gameObject != vGameObjectToSkip && tHit.collider.tag != "Bullet"){
				fHit(tHit.collider.gameObject,tHit.point,tHit.rigidbody);
				}
			}

		vPreviousPosition = this.transform.position;
		}

    void fHit(GameObject tObj, Vector3 tPoint, Rigidbody tOther) {
        this.transform.position = tPoint;
        if (tObj.tag == "Target" || tObj.tag == "AI") { 
            tObj.SendMessage("Damage", vDamage, SendMessageOptions.DontRequireReceiver);
            tObj.SendMessage("fHit", SendMessageOptions.DontRequireReceiver); }
        GameObject tTEmp = Instantiate(vSpark);
		tTEmp.transform.position = tPoint;
		tTEmp.GetComponent<Scr_DestroyTime>().fStartTimer(.9f);
		//Rigidbody tRB = tOther.GetComponent<Rigidbody>();
		if (tOther != null){
			//tOther.AddForce(Vector3.up);
            if (tOther.tag != "AI")
			tOther.AddForce(cRB.velocity*vSpeedMultiplier);
			//tOther.AddExplosionForce(10f,tPoint,10f,5f);
			}
		Destroy(this.gameObject);
	}
	void OnCollisionEnter(Collision tOther){
		Debug.Log("Collision with" + tOther.gameObject.name);
		fHit(tOther.collider.gameObject,tOther.contacts[0].point,tOther.rigidbody);
		//
	}
	void OnDestroy(){
		vTrail.transform.SetParent(null);
		vTrail.AddComponent<Scr_DestroyTime>().fStartTimer(1f);

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
