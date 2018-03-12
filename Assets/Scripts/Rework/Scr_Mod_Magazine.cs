using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Mod_Magazine : MonoBehaviour {
	public GameObject vMagazineToPop;
	public GameObject vBulletToUse;
	public int vMaxAmmo;
	public int vCurrentAmmo;

	// Use this for initialization
	void Start () {
		vCurrentAmmo = vMaxAmmo;
	}

	public void fReload (){
		vCurrentAmmo = vMaxAmmo;
		vMagazineToPop.GetComponent<Renderer>().enabled = true;
		vMagazineToPop.AddComponent<Collider>().enabled = true;
	}

	public void fNoAmmo (){
		GameObject tPoppedMagazine = Instantiate(vMagazineToPop.gameObject);
		vMagazineToPop.GetComponent<Renderer>().enabled = false;
		vMagazineToPop.GetComponent<Collider>().enabled = false;
		Rigidbody cRB = tPoppedMagazine.AddComponent<Rigidbody>();

		tPoppedMagazine.transform.position = vMagazineToPop.transform.position;
		tPoppedMagazine.transform.eulerAngles = vMagazineToPop.transform.eulerAngles;
		tPoppedMagazine.transform.localScale = vMagazineToPop.transform.lossyScale;
		cRB.velocity = tPoppedMagazine.transform.TransformDirection(Vector3.down*5f);
	}
}
