using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Guide_Particle : MonoBehaviour {
	public GameObject vOutwardParticle;
	public GameObject vInwardParticle;
	public GameObject[] vParticleList;

	public void fGiveMaleParticles(GameObject tSource){
		Scr_Male_Socket[] tMaleList = tSource.GetComponentsInChildren<Scr_Male_Socket>();
		Scr_Female_Socket[] tFemaleList = tSource.GetComponentsInChildren<Scr_Female_Socket>();
		int tCount = 0;
		fDestroyParticles();
		vParticleList = new GameObject[tMaleList.Length + tFemaleList.Length];

		foreach (Scr_Male_Socket tMale in tMaleList){
			if (tMale.vConnectedTo == null){
				GameObject tOut = Instantiate(vOutwardParticle);
				tOut.transform.SetParent(tMale.gameObject.transform);
				tOut.transform.localPosition = Vector3.zero;
				tOut.transform.localEulerAngles = new Vector3(-180,0,0);
				vParticleList[tCount] = tOut;
				tCount ++;
				}
		}
		foreach (Scr_Female_Socket tFemale in tFemaleList){
		if (tFemale.vConnectedObject == null){
			GameObject tOut = Instantiate(vInwardParticle);
			tOut.transform.SetParent(tFemale.gameObject.transform);
			tOut.transform.localPosition = Vector3.zero;
			tOut.transform.localEulerAngles = Vector3.zero;
			vParticleList[tCount] = tOut;
			tCount ++;
			}
		}

	}

	public void fDestroyParticles(){
		foreach(GameObject tTemp in vParticleList){
			Destroy(tTemp);


		}
		vParticleList = new GameObject[0];
	}
}
