using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Bullet : MonoBehaviour {
	public Rigidbody cRB;
	public float vSpeedMultiplier = 10f;
	public GameObject vModelToTurn;
	// Use this for initialization
	void Start () {
		cRB = GetComponent<Rigidbody>();
		cRB.velocity = transform.TransformDirection(Vector3.forward * vSpeedMultiplier);
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(transform.position+cRB.velocity);
		cRB.velocity = transform.TransformDirection(Vector3.forward * vSpeedMultiplier);
	}
	void OnCollisionEnter(Collision tOther){
		if (tOther.gameObject.tag == "Wall")
			Destroy(this.gameObject);
		if (tOther.gameObject.tag == "Player" || tOther.gameObject.tag == "AI"){
			tOther.gameObject.SendMessage("TakeDamage",1);
			Destroy(this.gameObject);
			}
	}
}
