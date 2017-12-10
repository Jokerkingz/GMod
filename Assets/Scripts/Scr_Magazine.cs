using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Magazine : MonoBehaviour {
	public GameObject vBulletSource;
	public string vBulletType;
	public string vMagazineType;
	public int vMaxAmmo;
	public int vCurrentAmmo;

	public Renderer vModelToCancel;
	void Start(){
		vCurrentAmmo = vMaxAmmo;
	}
	void Update(){
		if (vCurrentAmmo <= 0)	{
			vModelToCancel.enabled = false;
		}
	}


	public void Reload(){
		BroadCastThis("NewEquiped");
		vCurrentAmmo = vMaxAmmo;
		vModelToCancel.enabled = true;
	}

	void BroadCastThis(string vAttachDetach){
		this.transform.root.gameObject.BroadcastMessage(vAttachDetach,this.gameObject,SendMessageOptions.DontRequireReceiver);
	}
}
