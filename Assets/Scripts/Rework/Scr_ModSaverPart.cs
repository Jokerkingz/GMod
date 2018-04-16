using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ModSaverPart : MonoBehaviour {
	public string vOwnID;
	public string vPartType;
	public Scr_ModSaverSocket[] vModSaverSocketList = new Scr_ModSaverSocket[0];
	public GameObject vAnchor;
	public string vMainInformation;
	public int vImageToUse;

	[ContextMenu("Start Script")]
	void Awake(){
		vModSaverSocketList = GetComponentsInChildren<Scr_ModSaverSocket>();
	}
}
