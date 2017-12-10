using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Bullet : MonoBehaviour {
	public Rigidbody cRB;
	public float vSpeedMultiplier = 10f;
	public GameObject vModelToTurn;

	private int vSolidify = 2;
	// Use this for initialization
	void Start () {
		cRB = GetComponent<Rigidbody>();
		cRB.velocity = transform.TransformDirection(Vector3.forward * vSpeedMultiplier);

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
}
