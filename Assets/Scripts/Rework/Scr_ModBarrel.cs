using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ModBarrel : MonoBehaviour {
	public enum BarrelType {Void,Simple,Plasma,Rail,Curve}
	[Header("Common")]
	public float vCoolDown;
	public GameObject vVFX;
	public Collider vColliderToSkip;

	[Header("Barrel Information")]
	public BarrelType vBarrelType;
	public GameObject vBulletSource;
	public Scr_ModBarrel vOtherBarrel;
	public float vBatteryCost;
	public float vAccuracyMultiplier;
	public float vCoolDownTime;
	// Curve Information
	public Vector3 vVectCurrent;
	public Vector3 vVectPrevious;
	public Vector3 vTilt;
	[Header("Audio Control")]
	public GameObject vAudioPlay;
	void Start(){
		vVectCurrent = transform.position;
		vVectPrevious = vVectCurrent;
	}
	void Update () {
		if (vCoolDown > 0f)
			vCoolDown -= Time.deltaTime;
		switch (vBarrelType){
		case BarrelType.Curve:
			vVectPrevious = vVectCurrent;
			vVectCurrent = transform.position;
			Vector3 tTemp =  vVectCurrent-vVectPrevious;
			if (tTemp.magnitude > 0)
				vTilt = tTemp;
			break;
		}
		/*
		vAnglePrevious = vAngleCurrent;
		vAngleCurrent = transform.eulerAngles;
		vAngleTilt = 
		//Vector3.Angle(vAngleCurrent,vAnglePrevious);
		vTilt = transform.TransformDirection(vAngleTilt)*.01f;
		/*
		vTilt.x = Mathf.Round(vTilt.x*100f)*.01f;
		vTilt.y = Mathf.Round(vTilt.y*100f)*.01f;
		vTilt.z = Mathf.Round(vTilt.z*100f)*.01f;
		*/
	}
	public void fCallToShot (Scr_ModHandle tBullet){
		switch (vBarrelType){
		case BarrelType.Simple: fTypeSimple(tBullet); break;
		case BarrelType.Plasma: fTypePlasma(tBullet); break;
		case BarrelType.Rail: fTypeRail(tBullet);break;
		case BarrelType.Curve: fTypeCurve(tBullet);break;
		}
	}

	void fTypeSimple(Scr_ModHandle tBullet){
		if (tBullet != null && vCoolDown <= 0f){
			Scr_Data_Bullet tTemp = tBullet.fGetBullet();
			if (tTemp.vBulletPrefab != null){
				for (int i = 0; i < tTemp.vCopies; i++) {
					fShootAbullet(tTemp,vBarrelType);
				}
				//tTrail
			}
		}
	}

	void fTypePlasma(Scr_ModHandle tSource){
		if (tSource != null && vCoolDown <= 0f){
			GameObject tTemp = vBulletSource;
			//Scr_Data_Bullet tTemp;
			//tTemp.vBulletPrefab =  vBulletSource;
			if (tSource.fCheckBattery(vBatteryCost)){
				tSource.fGetBattery(vBatteryCost);
				GameObject tObj = Instantiate(tTemp);
				tObj.transform.position = this.transform.position;
				tObj.GetComponent<Scr_Bullet>().vPreviousPosition = this.transform.position;
				Vector3 tTrajectory = new Vector3(this.transform.eulerAngles.x+Random.Range(-vAccuracyMultiplier,vAccuracyMultiplier),this.transform.eulerAngles.y+Random.Range(-vAccuracyMultiplier,vAccuracyMultiplier),this.transform.eulerAngles.z+Random.Range(-vAccuracyMultiplier,vAccuracyMultiplier));
				tObj.transform.eulerAngles = tTrajectory;
				tObj.AddComponent<Scr_DestroyTime>().fStartTimer(3f);
				vCoolDown = vCoolDownTime;
				vOtherBarrel.vCoolDown += .15f;
				Scr_Bullet tCB = tObj.GetComponent<Scr_Bullet>();
				tCB.vGameObjectToSkip = vColliderToSkip.gameObject;
				Physics.IgnoreCollision(tCB.vColliderToSkip,vColliderToSkip);
			}
		}
	}

	void fTypeRail(Scr_ModHandle tSource){
		if (tSource != null && vCoolDown <= 0f){
			//GameObject tTemp = vBulletSource;
			if (tSource.fCheckBattery(vBatteryCost)){
				GameObject tTemp = tSource.fGetBullet().vBulletPrefab;
				if (tTemp != null){
					tSource.fGetBattery(vBatteryCost);
					GameObject tObj = Instantiate(tTemp);
					tObj.transform.position = this.transform.position;
					tObj.GetComponent<Scr_Bullet>().vPreviousPosition = this.transform.position;
					Vector3 tTrajectory = new Vector3(this.transform.eulerAngles.x+Random.Range(-vAccuracyMultiplier,vAccuracyMultiplier),this.transform.eulerAngles.y+Random.Range(-vAccuracyMultiplier,vAccuracyMultiplier),this.transform.eulerAngles.z+Random.Range(-vAccuracyMultiplier,vAccuracyMultiplier));
				tObj.transform.eulerAngles = tTrajectory;
				tObj.AddComponent<Scr_DestroyTime>().fStartTimer(3f);
					vCoolDown = vCoolDownTime;
					Scr_Bullet tCB = tObj.GetComponent<Scr_Bullet>();
					tCB.vGameObjectToSkip = vColliderToSkip.gameObject;
					tCB.vSpeedMultiplier = 100f;
					Physics.IgnoreCollision(tCB.vColliderToSkip,vColliderToSkip);
				}
			}
		}
	}
	void fTypeCurve(Scr_ModHandle tBullet){
		if (tBullet != null && vCoolDown <= 0f){
			//GameObject tTemp = tBullet.fGetBullet();
			Scr_Data_Bullet tTemp = tBullet.fGetBullet();
			if (tTemp.vBulletPrefab != null){
				for (int i = 0; i < tTemp.vCopies; i++) {
					fShootAbullet(tTemp,vBarrelType);
				/*
				GameObject tObj = Instantiate(tTemp.vBulletPrefab);
				tObj.transform.position = this.transform.position;
				Scr_Bullet tCB = tObj.GetComponent<Scr_Bullet>();

				tCB.vPreviousPosition = this.transform.position;
				Vector3 tTrajectory = this.transform.eulerAngles;//new Vector3(this.transform.eulerAngles.x+Random.Range(-2f,2f),this.transform.eulerAngles.y+Random.Range(-2f,2f),this.transform.eulerAngles.z+Random.Range(-2f,2f));
				tObj.transform.eulerAngles = tTrajectory;

				tObj.AddComponent<Scr_DestroyTime>().fStartTimer(3f);
				vCoolDown = vCoolDownTime;
				tCB.vGameObjectToSkip = vColliderToSkip.gameObject;
				Physics.IgnoreCollision(tCB.vColliderToSkip,vColliderToSkip);
				//tTrail
				*/
				}
			}
		}
	}

	void fShootAbullet(Scr_Data_Bullet tData,BarrelType tBar){
		GameObject tObj = Instantiate(tData.vBulletPrefab);
		tObj.transform.position = this.transform.position;
		Scr_Bullet tCB = tObj.GetComponent<Scr_Bullet>();
		float tOffset = tData.vAccuracy*vAccuracyMultiplier;
		tCB.vPreviousPosition = this.transform.position;
		Vector3 tTrajectory = new Vector3(this.transform.eulerAngles.x+Random.Range(-tOffset,tOffset),this.transform.eulerAngles.y+Random.Range(-tOffset,tOffset),this.transform.eulerAngles.z+Random.Range(-tOffset,tOffset));
		tObj.transform.eulerAngles = tTrajectory;
		tCB.vTilt = tTrajectory;
		tObj.AddComponent<Scr_DestroyTime>().fStartTimer(3f);
		vCoolDown = vCoolDownTime*tData.vCoolDownMultiplier;
		tCB.vGameObjectToSkip = vColliderToSkip.gameObject;
		Physics.IgnoreCollision(tCB.vColliderToSkip,vColliderToSkip);
		switch (tBar){
			case BarrelType.Curve:
					Scr_BulFX_Curve tBFX = tObj.AddComponent<Scr_BulFX_Curve>();
					tBFX.vTilt = vTilt;
					tBFX.cRB = tObj.GetComponent<Rigidbody>();
			break;
		}
	}
}
