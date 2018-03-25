using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Mod_Magazine : MonoBehaviour {
	public GameObject vMagazineToPop;
	public GameObject vBulletToUse;
	public int vMaxAmmo;
	public int vCurrentAmmo;
	//private  cMAR;
	private Scr_Mod_Statistics cMS;
	public Scr_GameEngine cGE;
	public Texture vTextureToUse;
	public Vector3 vPopDirection;
	// Use this for initialization
	void Start () {
		vCurrentAmmo = vMaxAmmo;
		//cMAR = GetComponent<Scr_Mod_AutoReload>();
		cMS = GetComponent<Scr_Mod_Statistics>();
		cGE = GameObject.FindGameObjectWithTag("GameController").GetComponent<Scr_GameEngine>();
	}

	public void fReload (){
		vCurrentAmmo = vMaxAmmo;
		//vMagazineToPop.GetComponent<Renderer>().enabled = true;
		vMagazineToPop.GetComponent<Collider>().enabled = true;
	}

	public void fNoAmmo (){
		if (GetComponent<Scr_Mod_AutoReload>() == null){
		GameObject tPoppedMagazine = Instantiate(vMagazineToPop.gameObject);
		//vMagazineToPop.GetComponent<Renderer>().enabled = false;
		vMagazineToPop.GetComponent<Collider>().enabled = false;
		Rigidbody cRB = tPoppedMagazine.AddComponent<Rigidbody>();
		Scr_Mod_AutoReload cMAR = this.gameObject.AddComponent<Scr_Mod_AutoReload>();
			cMAR.fStartReloading(this.gameObject,cMS.vTypeData,this,cGE,vTextureToUse);


		tPoppedMagazine.transform.position = vMagazineToPop.transform.position;
		tPoppedMagazine.transform.eulerAngles = vMagazineToPop.transform.eulerAngles;
		tPoppedMagazine.transform.localScale = vMagazineToPop.transform.lossyScale;
			cRB.velocity = tPoppedMagazine.transform.TransformDirection(vPopDirection*5f);
		}
	}
}
