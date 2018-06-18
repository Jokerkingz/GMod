using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_droneHealth : MonoBehaviour {

private scr_droneStats droneStats;
public float curHealth;
public float maxHealth;


void Start()
{
	droneStats = GetComponentInParent<scr_droneStats>();
	maxHealth = droneStats.vHealth;
	curHealth = maxHealth;
}

void Update()
{
	if (curHealth >= maxHealth)
	{
		curHealth=maxHealth;
	}
	if (curHealth <=0)
	{
		curHealth=0;
	}
}

public void Damage(float dmg)
{
	curHealth -= dmg;
}

}