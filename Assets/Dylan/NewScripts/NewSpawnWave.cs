using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawnWave : MonoBehaviour {

    bool isSpawning = false;
    public float minTime = 5.0f;
    public float maxTime = 15.0f;
    public GameObject[] enemies;  // Array of enemy prefabs.
    public Transform[] spawnPoints;
    public GameObject self;
    public bool canStartWave = true;
    public int waveTimeLength = 60;

    

    IEnumerator SpawnObject(int index, float seconds)
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        yield return new WaitForSeconds(seconds);
        Instantiate(enemies[index], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

        //We've spawned, so now we could start another spawn     
        isSpawning = false;

    }

    void Update()
    {
        //We only want to spawn one at a time, so make sure we're not already making that call
        if (!isSpawning && !canStartWave)
        {
            isSpawning = true; //Yep, we're going to spawn
            int enemyIndex = Random.Range(0, enemies.Length);

            StartCoroutine(SpawnObject(enemyIndex, Random.Range(minTime, maxTime)));

            if (canStartWave == false){
                StartCoroutine("waveTime");
            }
        }
    }
    IEnumerator waveTime()
    {
        yield return new WaitForSeconds(waveTimeLength);
        canStartWave = true;
        
    }
}