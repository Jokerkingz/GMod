using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyMagazine : MonoBehaviour {

    public Image energyBar;
    public bool full = false;
    public bool empty = false;
    public bool canMove = true;
    public float energyAmount = 0.99f;

	void Update() 
    {
        energyBar.transform.localScale = new Vector3(1, energyAmount, 1);
        //decrease energy
        if (Input.GetKey(KeyCode.E) && canMove)
        {
            energyAmount = energyAmount - 0.01f;
        }
        //increase energy
        if (Input.GetKey(KeyCode.R) && canMove)
        {
            energyAmount = energyAmount + 0.01f;
        }

        if (energyAmount <= 0.01f)
        {
            canMove = false;
            empty = true;
            if (Input.GetKey(KeyCode.R) && empty)
            {
                canMove = true;
                empty = false;
            }
        }
        if (energyAmount >= 0.99f)
        {
            canMove = false;
            full = true;
            if (Input.GetKey(KeyCode.E) && full)
            {
                canMove = true;
                full = false;
            }
        }
	}
}
