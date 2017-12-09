using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimCircle : MonoBehaviour {

    public Image aimCircle;
    public float size = 1f;
    public bool canDecrease = true;
    public bool canIncrease = false;

	void Update() 
    {
        aimCircle.transform.localScale = new Vector3 (size, size, 1);
        //left mouse click
        if (Input.GetKey(KeyCode.E) && canDecrease)
        {
            size = size - 0.01f;
            canDecrease = true;
            canIncrease = false;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            canDecrease = false;
            canIncrease = true;
        }
        if (canIncrease)
        {
            size += Time.deltaTime;
            canDecrease = false;
            canIncrease = true;
        }

        if (size >= 1f)
        {
            canIncrease = false;
        }

        if (size < 1.1f && size > 0.3f)
        {
            canDecrease = true;
        }
        if (size <= 0.3f)
        {
            canDecrease = false;
        }
	}
}
