using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_RespawnStuff : MonoBehaviour {
	private Vector3 vSpotOriginal;
	private Vector3 vAnglOriginal;
	public bool vIsCan;
	// Use this for initialization
	void Start () {
		vSpotOriginal = transform.position;
		vAnglOriginal = transform.eulerAngles;
	}
	
	// Update is called once per frame
	public void Reset () {
		if (vIsCan){
			this.transform.position = vSpotOriginal;
			this.transform.eulerAngles = vAnglOriginal;
			}
		else
			this.gameObject.SetActive(true);
	}
}
