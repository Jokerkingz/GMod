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

	public void NewEquiped(GameObject tThis){
		if (tThis.GetComponent<Scr_Socket>().vPartType == "Barrel"){
			//Debug.Log(this.GetComponent<Scr_Socket>().vPartType +" Received " + tThis.GetComponent<Scr_Socket>().vPartType);
			if (vCurrentAmmo > 0)
				BroadCastThis("NewEquiped");
				//tThis.BroadcastMessage("ReEquip",this.gameObject,SendMessageOptions.DontRequireReceiver);
			//if (tThis.GetComponent<Scr_Magazine>() != null){

				//ReEquip(GameObject tThis)
				//vMagazineList.Add(tThis.GetComponent<Scr_Magazine>());
				//vConnectedWith = tThis.gameObject;
			}
			
	}

    public void TurnOff(string vWhy){
    	switch (vWhy){
    	case "Hollow":
    		this.enabled = false;
    		this.tag = "Hollow";
    	break;
    	}
    }
}
