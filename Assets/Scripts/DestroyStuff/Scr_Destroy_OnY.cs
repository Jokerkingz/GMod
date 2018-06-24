using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Destroy_OnY : MonoBehaviour {
	void Update () {
        if (transform.position.y < 0f)
            Destroy(this.gameObject);
	}
}
