using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Triggered : MonoBehaviour {
	private Scr_Magazine vMagazine;
	public GameObject vAmmunition;
	private float vShotCD;
	public int vAmmo;
	public int vMaxAmmo = 100;
	public bool vIsPlayer;

	public bool vUnlimited;
	public bool vAIOwned;
	public GameObject vTarget;

	public float vCoolDownTime = .25f;
	public float vProjectileSpeed = 5f;
	public List<Scr_Magazine> vMagazineList;
	// Use this for initialization
	void Start () {
		FindMagazine();
		if (vIsPlayer)
			Cursor.lockState = CursorLockMode.Locked;
		vAmmo = vMaxAmmo;
		if (vAIOwned)
			vTarget = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		if (vShotCD > 0f)
			vShotCD -= Time.deltaTime;
		if (vAIOwned)
			this.transform.LookAt(vTarget.transform.position);
	}
	void FindMagazine(){
		vMagazine = this.GetComponentInChildren<Scr_Magazine>();
	}

	public void Triggered (){ // Start Bang Bang
		Debug.Log("Triggered");
		if (vAmmo > 0 && vShotCD <= 0f){
			if (vAIOwned){
				GameObject tObj = Instantiate(vAmmunition);
				tObj.transform.position = this.transform.position;
				tObj.transform.eulerAngles = this.transform.eulerAngles;
				tObj.GetComponent<Scr_Bullet>().vSpeedMultiplier = vProjectileSpeed;
				GameObject tTemp = transform.root.gameObject;
					vShotCD = 1.5f;
				//vShotCD += .5f;
				if (!vUnlimited){
					vAmmo -= 1;}
				}
			else{if (vMagazineList.Count > 0){
					if (vMagazineList[0].vCurrentAmmo > 0){
						GameObject tObj = Instantiate(vMagazineList[0].vBulletSource);
						tObj.transform.position = this.transform.position;
						tObj.transform.eulerAngles = this.transform.eulerAngles;
						tObj.GetComponent<Scr_Bullet>().vSpeedMultiplier = vProjectileSpeed;

						vMagazineList[0].vCurrentAmmo -= 1;
						if (vMagazineList[0].vCurrentAmmo <= 0)
							vMagazineList.Remove(vMagazineList[0]);
						GameObject tTemp = transform.root.gameObject;
						vShotCD = vCoolDownTime;
						}
					else
						vMagazineList.Remove(vMagazineList[0]);
				}



			}

		}
	}

	public void Reload(){
		vAmmo = vMaxAmmo;
	}

	public void NewEquiped(GameObject tThis){
		if (tThis.GetComponent<Scr_Socket>().vPartType == "Magazine"){
			if (tThis.GetComponent<Scr_Magazine>() != null){
				vMagazineList.Add(tThis.GetComponent<Scr_Magazine>());
				//vConnectedWith = tThis.gameObject;
			}
			
		}
	}

	public void ReEquip(GameObject tThis){
		if (tThis.GetComponent<Scr_Socket>().vPartType == "Magazine"){
			if (tThis.GetComponent<Scr_Magazine>() != null){
				vMagazineList.Add(tThis.GetComponent<Scr_Magazine>());
				//vConnectedWith = tThis.gameObject;
			}
			
		}
	}
	public void fReload(){
		


	}
}
