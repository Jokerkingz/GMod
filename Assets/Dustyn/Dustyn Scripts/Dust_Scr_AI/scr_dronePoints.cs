using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_dronePoints : MonoBehaviour {

	public int pointID;
	public bool a;

	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Wall"))
		{
			Blocked();
		}
	}
	void OnTriggerExit (Collider col)
	{
		if (col.CompareTag("Wall"))
		{
			Unblocked();
		}
	}

	void Blocked()
	{
		
	}

	void Unblocked()
	{
	
	}
}
