using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

	public float duration = 5f;
	public float explosionRadius = 3f;

	private float explosionTimer;

	// Use this for initialization
	void Start ()
	{
		explosionTimer = duration;
	}
	
	// Update is called once per frame
	void Update ()
	{
		explosionTimer -= Time.deltaTime;
		if (explosionTimer <= 0) {
			Destroy (gameObject);
		}
	}
}
