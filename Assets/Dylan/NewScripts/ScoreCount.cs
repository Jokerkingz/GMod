using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour {

    public Text MyText;
    public int score;

    void Start()
    {

        MyText.text = "";

    }


    void Update()
    {

        MyText.text = "" + score;


    }
    void AddPoints(){
        score = score + 1;

    }

}
