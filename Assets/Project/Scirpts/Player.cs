using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header ("Visuals")]
	public GameObject model;

	[Header ("Movements")]
	public float movingVelocity;
	public float jumpingVelocity;
	public float rotatingSpeed;

	[Header ("Equipment")]
	public Sword sword;

	private Rigidbody playerRigidBody;
	private Quaternion targetModelRotation;
	private bool canJump;

	// Use this for initialization
	void Start ()
	{
		playerRigidBody = GetComponent<Rigidbody> ();
		targetModelRotation = Quaternion.Euler (0, 0, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		model.transform.rotation = Quaternion.Lerp (model.transform.rotation, targetModelRotation, Time.deltaTime * rotatingSpeed);
		ProcessInput ();
	}

	void ProcessInput ()
	{
		playerRigidBody.velocity = new Vector3 (0, playerRigidBody.velocity.y, 0);


		if (Input.GetKey (KeyCode.RightArrow)) {
			playerRigidBody.velocity = new Vector3 (movingVelocity, playerRigidBody.velocity.y, playerRigidBody.velocity.z);
			targetModelRotation = Quaternion.Euler (0, 270, 0);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			playerRigidBody.velocity = new Vector3 (-movingVelocity, playerRigidBody.velocity.y, playerRigidBody.velocity.z);
			targetModelRotation = Quaternion.Euler (0, 90, 0);
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			playerRigidBody.velocity = new Vector3 (playerRigidBody.velocity.x, playerRigidBody.velocity.y, movingVelocity);
			targetModelRotation = Quaternion.Euler (0, 180, 0);
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			playerRigidBody.velocity = new Vector3 (playerRigidBody.velocity.x, playerRigidBody.velocity.y, -movingVelocity);
			targetModelRotation = Quaternion.Euler (0, 0, 0);
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

		if (Input.GetKeyDown (KeyCode.Z)) {
			sword.Attack ();
		}
	}


}
