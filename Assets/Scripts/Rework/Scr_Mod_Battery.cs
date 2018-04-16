using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Scr_Mod_Battery : MonoBehaviour {
	public float vMaxBattery;
	public float vCurrentBattery;
	public float vRegenerationRate;
	public GameObject vSource;
	public Vector3 vScaleSource;
	public float vOriginalScale;

	//private  cMAR;
	//private Scr_Mod_Statistics cMS;
	//public Scr_GameEngine cGE;
	//public Texture vTextureToUse;
	// Use this for initialization
	void Start () {
		vCurrentBattery = vMaxBattery;
		vScaleSource = vSource.transform.localScale;
		vOriginalScale = vScaleSource.x;
		//cMAR = GetComponent<Scr_Mod_AutoReload>();
		//cMS = GetComponent<Scr_Mod_Statistics>();
		//cGE = GameObject.FindGameObjectWithTag("GameController").GetComponent<Scr_GameEngine>();
	}
	void Update(){
		if (vCurrentBattery < vMaxBattery){
			vCurrentBattery += vRegenerationRate;
			if (vCurrentBattery > vMaxBattery)
			vCurrentBattery = vMaxBattery;
			}
		vCurrentBattery = Mathf.Clamp(vCurrentBattery,0f,vMaxBattery);
		vScaleSource.x = (vCurrentBattery/vMaxBattery)*vOriginalScale;
		vSource.transform.localScale = vScaleSource;
	}
}
