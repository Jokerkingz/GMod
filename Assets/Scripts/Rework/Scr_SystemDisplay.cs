using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_SystemDisplay : MonoBehaviour {
	public string vStatus;
	private bool vShowMenu;
	public GameObject vDisplayAnchor;
	public GameObject vDisplayPrefabSource;
	public GameObject vDisplay;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch (vStatus){
			case "Main":
				

			break;
		}
	}
	void fShowMenu(){
		if (vShowMenu){
			fCloseDisplay();
		} else {
			vShowMenu = true;
			vDisplayAnchor = Instantiate(vDisplayPrefabSource);
			vStatus = "Main";
		}

	}

	void fCloseDisplay(){
			fCloseDisplay();
			vShowMenu = false;
			vStatus = "Hidden";
	}

	public void fReceiveButton(string tMessage){
		switch (tMessage){
			case "Setting":
				fShowMenu();
			break;
			case "Close":
				fCloseDisplay();
			break;

		}
	}
}
