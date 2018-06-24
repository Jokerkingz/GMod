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
    public Scr_GrabSystem_Item cGrabSystItem;
    public string vCreation = "Editor";
    public Rigidbody cRB;

    [ContextMenu("Start Script")]
	void Awake(){
		vModSaverSocketList = GetComponentsInChildren<Scr_ModSaverSocket>();
    }
}
