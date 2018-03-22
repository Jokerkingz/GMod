using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_lookAt : MonoBehaviour {

	public Transform target;
	void Start () {
		
	}

	void Update () {
		transform.LookAt(target);
	}
}
