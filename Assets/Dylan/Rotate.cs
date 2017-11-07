using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	public float rotateSpeed = 10.0f;
	public Material newMaterialRef;

	// Use this for initialization
	void Start () {
		Renderer rend = GetComponent<Renderer> ();

	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.down * (rotateSpeed * Time.deltaTime));
	}

	void OnTriggerEnter (Collider other){
		Renderer rend = GetComponent<Renderer> ();
		rend.material = newMaterialRef;

	}
}
