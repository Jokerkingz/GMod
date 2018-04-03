using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_FixedAngle : MonoBehaviour {
	public GameObject vAngleSource;
	void Update () {
		if (vAngleSource != null)
		transform.eulerAngles = new Vector3(-90,180+vAngleSource.transform.localEulerAngles.y,0);
	}
}
