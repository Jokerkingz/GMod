using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YRotate : MonoBehaviour {

    public float RotationSpeed = 2f;

	void Update () {
        transform.Rotate(Vector3.up * Time.deltaTime * RotationSpeed, Space.Self);
	}
}
