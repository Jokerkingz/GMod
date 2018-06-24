using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_SpawnSystem : MonoBehaviour {
    public GameObject vSpawnSource;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GameObject tObj;
        if (Input.GetKeyDown(KeyCode.T))
            tObj = Instantiate(vSpawnSource); 
	}
}
