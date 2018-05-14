using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Scr_ModHandle : MonoBehaviour {
	public List<Scr_ModBarrel> lBarrelList = new List<Scr_ModBarrel>();
	public List<Scr_Mod_Magazine> lMagazineList = new List<Scr_Mod_Magazine>();
	public List<Scr_Mod_Battery> lBatteryList = new List<Scr_Mod_Battery>();
	public List<Scr_ModModule> lModuleList = new List<Scr_ModModule>();

	public int tCount;
	// Update is called once per frame
	public void fUpdateList() {
		lBarrelList.Clear();
		lBarrelList = GetComponentsInChildren<Scr_ModBarrel>().ToList();
		lModuleList.Clear();
		lModuleList = GetComponentsInChildren<Scr_ModModule>().ToList();
		lMagazineList.Clear();
		//lMagazineList = GetComponentsInChildren<Scr_Mod_Magazine>().ToList();
		Scr_Mod_Magazine[] tList = GetComponentsInChildren<Scr_Mod_Magazine>();
		foreach (Scr_Mod_Magazine tTemp in tList){
			if (tTemp.vCurrentAmmo > 0)
					lMagazineList.Add(tTemp);
			}
		tCount = lMagazineList.Count;
		lBatteryList.Clear();
		lBatteryList = GetComponentsInChildren<Scr_Mod_Battery>().ToList();
	}

	public void fTriggerPressed(){
		fUpdateList();
		foreach (Scr_ModBarrel tBarrel in lBarrelList){
			tBarrel.fCallToShot(this);
		}
		foreach (Scr_ModModule tBarrel in lModuleList){
			tBarrel.fActivateMod();
		}
	}

	public Scr_Data_Bullet fGetBullet(){
		//GameObject tBulletToUse = null;
		Scr_Data_Bullet tData = new Scr_Data_Bullet();
		tData.vBulletPrefab = null;
		foreach (Scr_Mod_Magazine tMagazine in lMagazineList){
			if (tMagazine.vCurrentAmmo > 0){
				tMagazine.vCurrentAmmo -= 1;
				tData.vBulletPrefab = tMagazine.vBulletToUse;
				tData.vCoolDownMultiplier = tMagazine.vCoolDownMultiplier;
				tData.vType = tMagazine.vType;
				tData.vAccuracy = tMagazine.vAccuracy;
				tData.vCopies = tMagazine.vCopies;
				if (tMagazine.vCurrentAmmo <= 0){
					tMagazine.fNoAmmo();
				}
				break;
			}
		}
		return tData;
	}

	public bool fCheckBattery(float tCost){
		float tTotal = 0f;
		foreach(Scr_Mod_Battery tThat in lBatteryList){
			tTotal += tThat.vCurrentBattery;
		}
		if (tTotal > tCost)
			return true;
		else
			return false;
	}

	public bool fGetBattery(float tCost){{
		foreach(Scr_Mod_Battery tThat in lBatteryList){
			tThat.vCurrentBattery -= tCost;
			if (tThat.vCurrentBattery  < 0){
				tCost = tThat.vCurrentBattery *-1;
				tThat.vCurrentBattery  = 0;
				}
			else 
				return true;
			}
		}
		return false;
	}

}
