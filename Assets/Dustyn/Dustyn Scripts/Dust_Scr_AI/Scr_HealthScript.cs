using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_HealthScript : MonoBehaviour {

public float curHealth;
public float maxHealth;


void Start()
{
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
		//death
		if (tag=="Enemy")
		{
			Debug.Log("EnemyDead");
		}

		if (tag=="Player")
		{
			Debug.Log("GameOver");
		}
	}

	
}

public void Damage(float dmg)
{
	curHealth -= dmg;
}

public void Heal (float heal)
{
	curHealth += heal;
}

}
