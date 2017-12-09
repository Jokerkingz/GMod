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

		if (Input.GetAxis("OGVR_RIndexTrigger") > 0 && !vAIOwned){
		Debug.Log("Triggered");
			Triggered ();
			}
		if (vAIOwned)
			this.transform.LookAt(vTarget.transform.position);
    		//this.gameObject.BroadcastMessage("Triggered");
	}
	void FindMagazine(){
		vMagazine = this.GetComponentInChildren<Scr_Magazine>();
	}

	public void Triggered (){ // Start Bang Bang
		if (vAmmo > 0 && vShotCD <= 0f){
			GameObject tObj = Instantiate(vAmmunition);
			tObj.transform.position = this.transform.position;
			tObj.transform.eulerAngles = this.transform.eulerAngles;
			tObj.GetComponent<Scr_Bullet>().vSpeedMultiplier = 5f;
			vShotCD += .5f;
			if (!vUnlimited)
				vAmmo -= 1;
			vShotCD = .5f;
		}
	}
	public void Reload(){
		vAmmo = vMaxAmmo;
	}

}
