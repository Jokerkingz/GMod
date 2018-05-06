using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ExplosionPrefab : MonoBehaviour {
	public float vTimer;
	public GameObject vExlposionSource;
	// Use this for initialization
	void Start () {
		Invoke("fExplode",vTimer);
	}
	void fExplode(){
		GameObject tTemp = Instantiate(vExlposionSource);
		tTemp.transform.position = this.transform.position;
		Destroy(this.gameObject);
	}
}
