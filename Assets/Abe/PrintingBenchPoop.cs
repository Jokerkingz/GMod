using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintingBenchPoop : MonoBehaviour {

    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

	void Update() 
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            anim.Play("Poop");
        }
	}
}
