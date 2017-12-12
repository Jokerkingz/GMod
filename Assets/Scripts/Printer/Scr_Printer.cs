using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Printer : MonoBehaviour {
	public string[] vModType = new string[]{"Handle","Base","Barrel","Extension","Sword","Shield"};
	public int vModTypIndex;
	public string vSubType = "A"; //"B"
	public float vCoolDown;
	public GameObject vSpawnSpot;
	public Text vMiddle;
	public Text vBrand;
	public float vCD;
	// Use this for initialization
	void Start () {
		vModType = new string[]{"Handle","Base","Barrel","Magazine","Extension","Sword","Shield"};
		vSubType = "A";
	}
	
	// Update is called once per frame
	void Update () {
		if (vCD > 0)
			vCD -= Time.deltaTime;
		vMiddle.text = vModType[vModTypIndex]+ " " + vSubType;
	}

	public void vReceiveButton(string vSource){
		if (vCD <= 0){
			vCD +=.4f;
			string tTemp;
			GameObject vPrefab;
			switch (vSource){
			case "Next":
				vModTypIndex ++;
				if (vModTypIndex >= vModType.Length)
					vModTypIndex = 0;
				vSubType = "A";
				break;
			case "Previous":
				vModTypIndex --;
				if (vModTypIndex < 0 )
					vModTypIndex = vModType.Length-1;
				vSubType = "A";

				break;
			case "Brand":
				switch(vSubType){
					case "A":
						vSubType = "B";
						break;
					case "B":
						vSubType = "C";
						break;
					default:
						vSubType = "A";
					break;

				}
				break;
			case "Print":
				tTemp = "Pre_Mod_"+vModType[vModTypIndex]+"_"+vSubType;
				Debug.Log(tTemp);
				vPrefab = Resources.Load(tTemp) as GameObject;
				if (vPrefab != null){
					vPrefab = Instantiate(vPrefab) ;
					vPrefab.transform.position = vSpawnSpot.transform.position;
					}
				break;
			}
			tTemp = "Pre_Mod_"+vModType[vModTypIndex]+"_"+vSubType;
			vPrefab = Resources.Load(tTemp) as GameObject;
			if (vPrefab == null)
				vSubType = "A";
			vMiddle.text = vModType[vModTypIndex]+ " " + vSubType;
			}
	}
}
