using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_System_SourceList : MonoBehaviour {
	[System.Serializable]
	public class Mats
		{	
		public string vName;
		public Material vSource;
		}
	public Mats[] MatList;

	[System.Serializable]
	public class Parts
	{	public string vName;
		public GameObject vSource;
		public string vMainCategory;
		public string vModName;
		public string vModType;
		public string vNote;
	}
	public Parts[] PartList;

	public GameObject fGetPrefab(string tName){
		Debug.Log(tName);
		for (int i = 0; i < PartList.Length; i++) {
			if (PartList[i].vName == tName)
				return PartList[i].vSource;			
		}
		Debug.Log("Missing " + tName +" in Source List");
		return null;
	}

	public Material fGetMaterial(string tName){
		for (int i = 0; i < MatList.Length; i++) {
			if (MatList[i].vName == tName)
				return MatList[i].vSource;			
		}
		Debug.Log("Missing " + tName +" in Source List");
		return null;
	}
	void Update(){
		if (Input.GetKeyDown(KeyCode.Space)){
			Debug.Log("Space Pressed");
			if (fGetPrefab("Base_Magazine_A") == null)
				Debug.Log("I got nothing");
				

		}

	}
}
