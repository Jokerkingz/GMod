using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ModBarrel : MonoBehaviour {
	public float vCoolDown;
	public GameObject vVFX;
	public Collider vColliderToSkip;

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
				Vector3 tTrajectory = new Vector3(this.transform.eulerAngles.x+Random.Range(-2f,2f),this.transform.eulerAngles.y+Random.Range(-2f,2f),this.transform.eulerAngles.z+Random.Range(-2f,2f));
				tObj.transform.eulerAngles = tTrajectory;
				//Scr_DestroyTime tDest = 
				tObj.AddComponent<Scr_DestroyTime>().fStartTimer(3f);
				vCoolDown = .1f;
				//tObj = Instantiate(vVFX);
				//tObj.transform.position = this.transform.position;
				//tObj.transform.eulerAngles = this.transform.eulerAngles;
				Scr_Bullet tCB = tObj.GetComponent<Scr_Bullet>();
				tCB.vGameObjectToSkip = vColliderToSkip.gameObject;
				Physics.IgnoreCollision(tCB.vColliderToSkip,vColliderToSkip);
			}
		}
	}
}
