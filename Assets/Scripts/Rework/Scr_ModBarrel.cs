using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ModBarrel : MonoBehaviour {
	public float vCoolDown;
	public GameObject vVFX;
	// Update is called once per frame
	void Update () {
		if (vCoolDown > 0f)
			vCoolDown -= Time.deltaTime;
	}
	public void fCallToShot (Scr_ModHandle tBullet){
		if (tBullet != null && vCoolDown <= 0f){
			GameObject tTemp = tBullet.fGetBullet();
			if (tTemp != null){
				GameObject tObj = Instantiate(tTemp);
				//tBullet.vCurrentAmmo -= 1;
				tObj.transform.position = this.transform.position;
				tObj.transform.eulerAngles = this.transform.eulerAngles;
				vCoolDown = .4f;
				//tObj = Instantiate(vVFX);
				tObj.transform.position = this.transform.position;
				tObj.transform.eulerAngles = this.transform.eulerAngles;
			}
		}
	}
}
