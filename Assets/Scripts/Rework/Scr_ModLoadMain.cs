using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ModLoadMain : MonoBehaviour {
	//public string vOriginalString;
	public string[] vStringList = new string[0];
	public string vPartOfString;
	public Scr_System_SourceList cGE;
	// Use this for initialization
	void Start(){
		cGE = GameObject.FindGameObjectWithTag("GameController").GetComponent<Scr_System_SourceList>();

	}
	[ContextMenu("Convert")]
	public void fConvert (string tLoadthis) {
		vStringList = tLoadthis.Split("#"[0]);
		string[] tDivide = new string[0];
		for (int i = 0; i < vStringList.Length; i++) {
		Scr_ModSaverSocket[] tMSS = this.GetComponentsInChildren<Scr_ModSaverSocket>();
			tDivide = new string[0];
			tDivide = vStringList[i].Split("/"[0]);
			foreach(Scr_ModSaverSocket tSocket in tMSS){
				if (tSocket.vSocketID == tDivide[0]){
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
						this.GetComponent<Scr_ModSaverMain>().fInitialize(this.gameObject);
						if (tRootMSH != null){
							tRootMSH.lModsConnected.Add(vPrefab);
							if (tMalSocket.vIsMagazine)
								tRootMSH.lMagazineList.Add(vPrefab);
						}
					}
				}
			}
		}
	}
}
