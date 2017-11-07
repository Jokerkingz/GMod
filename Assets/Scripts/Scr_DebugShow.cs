using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_DebugShow : MonoBehaviour {
public GameObject vPrefab;
	public void ShowText(GameObject tThis, string tMessage){
		GameObject tPrefab = (GameObject)Resources.Load("Prefab/Pre_Debug", typeof(GameObject));
		GameObject tTemp = Instantiate(vPrefab);
		tTemp.transform.position = tThis.transform.position;
		tTemp.GetComponentInChildren<Text>().text = tMessage;
	}
}
