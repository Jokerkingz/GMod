using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GameEngine : MonoBehaviour {
	public static Scr_GameEngine gGE;
	public Material gMat_Fabricate;

	void Awake(){
		if (gGE != null){
			GameObject.Destroy(this);
			return;
			}
		else{
			gGE = this;
			DontDestroyOnLoad(this);}
	}
}
