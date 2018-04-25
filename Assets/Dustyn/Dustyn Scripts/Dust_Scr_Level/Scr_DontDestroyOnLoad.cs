using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DontDestroyOnLoad : MonoBehaviour {

	
	void Start () {
		DontDestroyOnLoad(this.gameObject);
		Destroy(this);
	}

}
