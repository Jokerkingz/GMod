using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_musicManager : MonoBehaviour {

	public GameObject firstLayer;
	public GameObject secondLayer;
	public bool secondLayerFadeIn;
	public bool secondLayerFadeOut;
	void Start () {
		
		firstLayer.GetComponent<AudioSource>().Play();
		secondLayer.GetComponent<AudioSource>().volume=0;
	}
	
	void Update () {
		if (secondLayerFadeIn)
		{
			secondLayer.GetComponent<AudioSource>().volume =secondLayer.GetComponent<AudioSource>().volume +0.01f;
		}

		if (secondLayer.GetComponent<AudioSource>().volume ==1)
		{
			secondLayerFadeIn=false;
		}
	}

	public void PlaySecondLayer()
	{
		secondLayerFadeIn=true;
	}
}
