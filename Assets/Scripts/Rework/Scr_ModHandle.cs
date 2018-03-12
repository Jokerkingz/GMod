using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Scr_ModHandle : MonoBehaviour {
	public List<Scr_ModBarrel> lBarrelList = new List<Scr_ModBarrel>();
	public List<Scr_Mod_Magazine> lMagazineList = new List<Scr_Mod_Magazine>();

	public int tCount;
	// Update is called once per frame
	public void fUpdateList() {
		lBarrelList.Clear();
		lBarrelList = GetComponentsInChildren<Scr_ModBarrel>().ToList();
		lMagazineList.Clear();
		//lMagazineList = GetComponentsInChildren<Scr_Mod_Magazine>().ToList();
		Scr_Mod_Magazine[] tList = GetComponentsInChildren<Scr_Mod_Magazine>();
		foreach (Scr_Mod_Magazine tTemp in tList){
			if (tTemp.vCurrentAmmo > 0)
					lMagazineList.Add(tTemp);
			}
		tCount = lMagazineList.Count;
	}

	public void fTriggerPressed(){
		fUpdateList();
		foreach (Scr_ModBarrel tBarrel in lBarrelList){
			tBarrel.fCallToShot(this);
		}
	}
	public GameObject fGetBullet(){
		GameObject tBulletToUse = null;
		foreach (Scr_Mod_Magazine tMagazine in lMagazineList){
			if (tMagazine.vCurrentAmmo > 0){
				tMagazine.vCurrentAmmo -= 1;
				tBulletToUse = tMagazine.vBulletToUse;
				if (tMagazine.vCurrentAmmo <= 0){
					tMagazine.fNoAmmo();
				}
				break;
			}
		}
		return tBulletToUse;

	}
}
