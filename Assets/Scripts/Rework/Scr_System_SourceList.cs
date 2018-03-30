using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_System_SourceList : MonoBehaviour {
	[System.Serializable]
	public class Parts
	{	public string vName;
		public GameObject vSource;
		public string vMainCategory;
		public string vModName;
		public string vModType;

	}
	public Parts[] PartList;

	public GameObject fGetPrefab(string name){




	}
}
