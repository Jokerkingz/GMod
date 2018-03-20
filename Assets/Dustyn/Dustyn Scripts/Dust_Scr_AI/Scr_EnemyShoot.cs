﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_EnemyShoot : MonoBehaviour {
	

	[Header("References")]
	public GameObject barrel;
	public GameObject bullet;
	private bool canShoot;

	[Header("Bullet Properties")]
	public float coolDownTime;
	public float bulletforce;
	public bool isShooting;

	void Start () {
		canShoot =true;
		isShooting=false;
	}
	
	
	void Update()
	{
		if (isShooting && canShoot)
		{
			StartCoroutine(CoolDown());
			
			GameObject tempBulletHandler;
			tempBulletHandler = Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);

			tempBulletHandler.transform.Rotate (Vector3.left *90);

			Rigidbody tempRB;
			tempRB =tempBulletHandler.GetComponent<Rigidbody>();
			tempRB.AddForce(transform.forward * bulletforce);
			

			Destroy (tempBulletHandler, 10.0f);

		}
	}

		private IEnumerator CoolDown()
	{
		canShoot=false;
		yield return new WaitForSeconds (coolDownTime);
		canShoot=true;
	}

}
