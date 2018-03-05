using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ModSaverMain : MonoBehaviour {
	[Header("System Connection")]
	public string vSavelist;

	void Start(){
		fInitialize(this.gameObject);

	}
	// Use this for initialization
	[ContextMenu("Initialize")]
	public string fInitialize(GameObject tReference){
		Scr_ModSaverPart tMSP = tReference.GetComponent<Scr_ModSaverPart>();
		tMSP.vOwnID = "1";
		vSavelist = "";
		fRenameID(tMSP,tMSP.vOwnID);
		return vSavelist;
		}

	[ContextMenu("See all sockets")]
	void fRenameID (Scr_ModSaverPart tObjectInQuestion, string tStartFrom) {
		int vAngle = Mathf.FloorToInt(tObjectInQuestion.transform.localEulerAngles.y);
		vSavelist += tObjectInQuestion.vOwnID+"/"+tObjectInQuestion.vPartType+"/"+vAngle.ToString()+"#";
		int tCount = 1;
		GameObject tObject;
		string tNewID;
		Scr_ModSaverPart tMSP;
		foreach (Scr_ModSaverSocket tMSS in tObjectInQuestion.vModSaverSocketList){
			tNewID = tStartFrom+tCount.ToString(); 
			tMSS.vSocketID = tNewID;
			tCount += 1;
			if (tMSS.vConnection != null){
				tObject = tMSS.vConnection;
				tMSP = tObject.GetComponent<Scr_ModSaverPart>();
				tMSP.vOwnID = tNewID; 
				fRenameID(tMSP, tNewID);
				}
			}
	}
}
