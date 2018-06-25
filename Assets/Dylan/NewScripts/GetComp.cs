using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetComp : MonoBehaviour {
    private ScoreCount kill;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        GameObject theScore = GameObject.Find("ScoreCounter");
        ScoreCount scoreBoard = theScore.GetComponent<ScoreCount>();
        if (Input.GetKeyDown("space"))
        {
            scoreBoard.score += 10;

        }
	}
}