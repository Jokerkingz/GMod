using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSymbolState : MonoBehaviour {
	public Color activeColor;
	public Color inactiveColor;
	public bool active = true;

	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
		if (!active) {
			ParticleSystem.MainModule settings = GetComponent<ParticleSystem>().main;
			settings.startColor = new ParticleSystem.MinMaxGradient(inactiveColor);

		}
		else {
			ParticleSystem.MainModule settings = GetComponent<ParticleSystem>().main;
			settings.startColor = new ParticleSystem.MinMaxGradient(activeColor);

		}
		
	}


}
