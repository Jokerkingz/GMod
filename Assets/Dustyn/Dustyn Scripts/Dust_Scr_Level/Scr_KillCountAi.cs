using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_KillCountAi : MonoBehaviour {

	private Scr_BasicAI basicAI;
	private Scr_KillCountManager killCountManager;
	private bool alreadyCounted;
	void Start () {
		basicAI= this.gameObject.GetComponent<Scr_BasicAI>();
		killCountManager = FindObjectOfType<Scr_KillCountManager>();
	}
	

	void Update () {
		if (!alreadyCounted && basicAI.isDead)
		{
			alreadyCounted=true;
			killCountManager.killCount++;
		}
	}
}
