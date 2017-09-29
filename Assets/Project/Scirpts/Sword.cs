using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

	public float swingingSpeed = 2f;

	private Quaternion targetRotation;

	// Use this for initialization
	void Start ()
	{
		targetRotation = Quaternion.Euler (0, 0, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.localRotation = Quaternion.Lerp (transform.localRotation, targetRotation, Time.deltaTime * swingingSpeed);
	}


	public void Attack ()
	{
		targetRotation = Quaternion.Euler (-90, 0, 0);
	}
}
