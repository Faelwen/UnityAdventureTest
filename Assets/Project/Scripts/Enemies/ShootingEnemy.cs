using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{

	public GameObject Model;
	public float timeToRotate = 2f;
	public float rotationSpeed = 6f;

	private int targetAngle;
	private float rotationTimer;

	// Use this for initialization
	void Start ()
	{
		rotationTimer = timeToRotate;
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
	}
}
