using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStart : MonoBehaviour {
    public GameObject spawnScriptObj;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       

    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject spawner = GameObject.Find("SpawnPoint");
        NewSpawnWave newSpawn = spawner.GetComponent<NewSpawnWave>();
        if (other.tag == "FingerTip")
        {
            newSpawn.canStartWave = false;
             
        }
    }
}
