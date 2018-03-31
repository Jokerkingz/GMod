using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ModModule : MonoBehaviour {
	public string vModuleType = "Rotation";
	public float vFloat; // Privatable
	public float vFloatSub; // Privatable

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch (vModuleType){
		case "Rotation":
			if (vFloatSub <= 0f)
				return;
			vFloatSub -= 10f*Time.deltaTime;
			vFloat += vFloatSub;
			if (vFloat > 360f)
				vFloat -= 360f;
			if (vFloatSub < 0)
				vFloatSub = 0f;
			this.transform.localEulerAngles = new Vector3(0f,vFloat,0f);
			break;
		case "RotationB":
			if (vFloatSub <= 0f)
				return;
			vFloatSub -= 10f*Time.deltaTime;
			vFloat -= vFloatSub;
			if (vFloat < 0f)
				vFloat += 360f;
			if (vFloatSub < 0)
				vFloatSub = 0f;
			this.transform.localEulerAngles = new Vector3(0f,vFloat,0f);
		break;

		}
	}
	public void fActivateMod(){
		switch (vModuleType){
		case "Rotation":
			vFloatSub += 20f*Time.deltaTime;
			if (vFloatSub > 10f)
				vFloatSub = 10f;
			break;
		case "RotationB":
			vFloatSub += 20f*Time.deltaTime;
			if (vFloatSub > 10f)
				vFloatSub = 10f;
		break;
		}
	}
}
