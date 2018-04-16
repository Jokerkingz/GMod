using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Give_Label : MonoBehaviour {
	public GameObject vCurrentTarget;
	public GameObject vPrefabSource;
	public GameObject vLabelProjected;
	public float vMeter;
	public GameObject vAnchor;
	// Use this for initialization
	// Update is called once per frame
	void Update () {
		if (vMeter > 0){
			vMeter -= .4f;
			if (vMeter <= 0){
				if (vLabelProjected != null){
					vLabelProjected.GetComponent<Scr_Part_Information>().vStatus = "Death";
					vLabelProjected = null;
					}
				}
			}
	}
	public void fReposition(){
		if (vAnchor != null)
			vLabelProjected.gameObject.transform.position = vAnchor.transform.position;
	}

	public void fShowLabel(GameObject tSource){
		if (tSource != null){
		vMeter = 1f;
		if (vCurrentTarget != tSource){
			vCurrentTarget = tSource;
			if (vLabelProjected != null)
				vLabelProjected.GetComponent<Scr_Part_Information>().vStatus = "Death";
			vLabelProjected = Instantiate(vPrefabSource);
			vLabelProjected.transform.position = tSource.transform.position;
			Scr_ModSaverPart tSourceMSP = tSource.GetComponent<Scr_ModSaverPart>();
			vAnchor = tSourceMSP.vAnchor;

			Scr_Part_Information tPartI = vLabelProjected.GetComponent<Scr_Part_Information>();
			tPartI.vSource = tSource;
			tPartI.vTextToSay = tSourceMSP.vMainInformation;
			tPartI.fImageUpdate(tSourceMSP.vImageToUse);

		}
		else if (vLabelProjected != null){
				fReposition();
			//vLabelProjected.transform.position = tSource.transform.position;
			}
		}
		else 
			vCurrentTarget = null;
	}
}
