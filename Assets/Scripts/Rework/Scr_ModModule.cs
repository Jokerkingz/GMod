using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ModModule : MonoBehaviour {
	public string vModuleType = "Rotation";
	public float vFloat; // Privatable
	public float vFloatSub; // Privatable
	public string vData;
	// Use this for initialization
	void fStart () {

		switch (vModuleType) {
		case "Loader":
			this.gameObject.AddComponent<Scr_ModLoadMain>();
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		switch (vModuleType){
		case "RotationB":
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
		case "Rotation":
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
		case "Loader":
			//fLoadData(vData);
			Scr_Male_Socket tOrigin = this.GetComponent<Scr_Male_Socket>();
			if (tOrigin == null)
				return;
			if (tOrigin.vConnectedTo == null)
				return;
			Scr_ModLoadMain tMain = tOrigin.GetComponentInParent<Scr_ModLoadMain>();
			Scr_Female_Socket vFem = tMain.GetComponentInChildren<Scr_Female_Socket>();
			tOrigin.Detach(this.gameObject);
			tMain.fConvert(vData);
			//tOrigin.gameObject.AddComponent

			Scr_FabricationCollective tTemp = vFem.gameObject.AddComponent<Scr_FabricationCollective>();
			tTemp.vIsLocked = false;
			tTemp.vDontChange = true;
			tTemp.Start();

			Destroy(this.gameObject);
		break;
		}
	}

	/*
	void fLoadData(string tLoadthis){
		{Scr_System_SourceList cGE = GameObject.FindGameObjectWithTag("GameController").GetComponent<Scr_System_SourceList>();
		string[] vStringList = tLoadthis.Split("#"[0]);
		string[] tDivide = new string[0];

		// take data from socket
		Scr_Male_Socket tOrigin = this.GetComponent<Scr_Male_Socket>();
			if (tOrigin.vConnectedTo == null)
			return;
			Scr_ModSaverMain tMain = tOrigin.GetComponentInParent<Scr_ModSaverMain>();

		GameObject vFirst = null ;
		
		tOrigin.Detach(this.gameObject);
		for (int i = 0; i < vStringList.Length; i++) {
			Debug.Log("For rounds " + i.ToString());
			Scr_ModSaverSocket[] tMSS = tMain.GetComponentsInChildren<Scr_ModSaverSocket>();
			//tOrigin = this.gameObject;
			tDivide = new string[0];
			tDivide = vStringList[i].Split("/"[0]);
			foreach(Scr_ModSaverSocket tSocket in tMSS){
				if (tSocket.vSocketID == tDivide[0]){
					Debug.Log(tDivide[1] + " Is being Tried to be made");
					GameObject vPrefab = cGE.fGetPrefab(tDivide[1]);
					//GameObject vPrefab = Resources.Load(tDivide[1]) as GameObject;
					if (vPrefab != null){
						if (tSocket.vConnection != null)
							tSocket.vConnection.GetComponent<Scr_Male_Socket>().Detach(tSocket.vConnection);
						vPrefab = Instantiate(vPrefab) ;
						vPrefab.GetComponent<Scr_Male_Socket>().Start();
						vPrefab.transform.position = this.transform.position;
						tSocket.vConnection = vPrefab;
						vPrefab.transform.SetParent(tSocket.transform);
						vPrefab.transform.localPosition= Vector3.zero;

						//fabrication setter
						if (vFirst == null)
							vFirst = vPrefab;
						// Take Components
						Scr_Male_Socket tMalSocket = vPrefab.GetComponent<Scr_Male_Socket>();

						// Connecting 
						tSocket.GetComponent<Scr_Female_Socket>().vConnectedObject = vPrefab;
						tSocket.GetComponent<Scr_ModSaverSocket>().vConnection = vPrefab;
						tMalSocket.vConnectedTo = tSocket.gameObject;
						//
						// Set Transform and Rigidbody
						int tNewAngle = int.Parse(tDivide[2]);
						vPrefab.transform.localEulerAngles = new Vector3(0,tNewAngle,0);//Reorientate(tReference);//+this.transform.eulerAngles;
						vPrefab.GetComponent<Rigidbody>().useGravity = false;
						vPrefab.GetComponent<Rigidbody>().isKinematic = true; 

						// Parenting
						foreach (GameObject tThat in tMalSocket.vOriginalParts) {
							tThat.transform.SetParent(tSocket.transform);
						}
						vPrefab.transform.SetParent(tSocket.transform);


						Scr_ModSystem_Handler tRootMSH = this.GetComponent<Scr_ModSystem_Handler>();
						tMain.GetComponent<Scr_ModSaverMain>().fInitialize(tMain.gameObject);
						if (tRootMSH != null){
							tRootMSH.lModsConnected.Add(vPrefab);
							if (tMalSocket.vIsMagazine)
								tRootMSH.lMagazineList.Add(vPrefab);
						}
					}
				}
			}
		}

		//Scr_FabricationCollective tTemp = vFirst.gameObject.AddComponent<Scr_FabricationCollective>();
		//tTemp.vIsLocked = false;
		//tTemp.Start();
		//Destroy(this.gameObject);
	}


	}*/
}
