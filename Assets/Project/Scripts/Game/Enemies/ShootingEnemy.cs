﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{

	public GameObject Model;
	public float timeToRotate = 2f;
	public float rotationSpeed = 6f;
	public float timeToShoot = 1f;
	public GameObject bulletPrefab;

	private int targetAngle;
	private float rotationTimer;
	private float shootingTimer;

	// Use this for initialization
	void Start ()
	{
		rotationTimer = timeToRotate;
		shootingTimer = timeToShoot;
	}
	
	// Update is called once per frame
	void Update ()
	{
		rotationTimer -= Time.deltaTime;
		if (rotationTimer <= 0f) {
			rotationTimer = timeToRotate;
			targetAngle += 90;
		}

		transform.localRotation = Quaternion.Lerp (transform.localRotation, Quaternion.Euler (0, targetAngle, 0), Time.deltaTime * rotationSpeed);
		shootingTimer -= Time.deltaTime;
		if (shootingTimer <= 0f) {
			shootingTimer = timeToShoot;
			GameObject bulletObject = Instantiate (bulletPrefab);
			bulletObject.transform.position = transform.position + Model.transform.forward;
			bulletObject.transform.forward = Model.transform.forward;
		}
	}
}
