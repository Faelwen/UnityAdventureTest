using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float movingVelocity;
	public float jumpingVelocity;

	private Rigidbody playerRigidBody;
	private bool canJump;

	// Use this for initialization
	void Start ()
	{
		playerRigidBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		ProcessInput ();
	}

	void ProcessInput ()
	{
		playerRigidBody.velocity = new Vector3 (0, playerRigidBody.velocity.y, 0);

		if (Input.GetKey (KeyCode.RightArrow)) {
			playerRigidBody.velocity = new Vector3 (movingVelocity, playerRigidBody.velocity.y, playerRigidBody.velocity.z);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			playerRigidBody.velocity = new Vector3 (-movingVelocity, playerRigidBody.velocity.y, playerRigidBody.velocity.z);
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			playerRigidBody.velocity = new Vector3 (playerRigidBody.velocity.x, playerRigidBody.velocity.y, movingVelocity);
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			playerRigidBody.velocity = new Vector3 (playerRigidBody.velocity.x, playerRigidBody.velocity.y, -movingVelocity);
		}

		// Prevents jump in midair
		RaycastHit hit;
		if (Physics.Raycast (transform.position, Vector3.down, out hit, 1.01f)) {
			canJump = true;
		}

		if (Input.GetKeyDown (KeyCode.Space) && canJump) {
			canJump = false;
			playerRigidBody.velocity = new Vector3 (playerRigidBody.velocity.x, jumpingVelocity, playerRigidBody.velocity.z);
		} 
	}
}
