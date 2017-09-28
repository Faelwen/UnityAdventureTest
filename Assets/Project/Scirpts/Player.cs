using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float movingVelocity;
	public float jumpingVelocity;

	private bool canJump;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		ProcessInput ();
	}

	void ProcessInput ()
	{
		GetComponent<Rigidbody> ().velocity = new Vector3 (0, GetComponent<Rigidbody> ().velocity.y, 0);

		if (Input.GetKey (KeyCode.RightArrow)) {
			GetComponent<Rigidbody> ().velocity = new Vector3 (movingVelocity, GetComponent<Rigidbody> ().velocity.y, GetComponent<Rigidbody> ().velocity.z);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			GetComponent<Rigidbody> ().velocity = new Vector3 (-movingVelocity, GetComponent<Rigidbody> ().velocity.y, GetComponent<Rigidbody> ().velocity.z);
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			GetComponent<Rigidbody> ().velocity = new Vector3 (GetComponent<Rigidbody> ().velocity.x, GetComponent<Rigidbody> ().velocity.y, movingVelocity);
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			GetComponent<Rigidbody> ().velocity = new Vector3 (GetComponent<Rigidbody> ().velocity.x, GetComponent<Rigidbody> ().velocity.y, -movingVelocity);
		}

		// Prevents jump in midair
		RaycastHit hit;
		if (Physics.Raycast (transform.position, Vector3.down, out hit, 1.01f)) {
			canJump = true;
		}

		if (Input.GetKeyDown (KeyCode.Space) && canJump) {
			canJump = false;
			GetComponent<Rigidbody> ().velocity += Vector3.up * jumpingVelocity;
		} 
	}
}
