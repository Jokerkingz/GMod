using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Button : MonoBehaviour {

private Scr_Door door;
	void Start () {
		door = GetComponentInParent<Scr_Door>();
	}
	
void OnTriggerEnter (Collider col)
{
	if (col.tag=="Player")
	{
		door.canInteract=true;
	}
}

void OnTriggerExit (Collider col)
{
		if (col.tag=="Player")
	{
		door.canInteract=false;
	}
}
}
