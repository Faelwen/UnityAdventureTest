using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float jumpingForce;

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
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.position += Vector3.right * 5f * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.position += Vector3.left * 5f * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			transform.position += Vector3.forward * 5f * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			transform.position += Vector3.back * 5f * Time.deltaTime;
		}

		// Prevents jump in midair
		RaycastHit hit;
		if (Physics.Raycast (transform.position, Vector3.down, out hit, 1.01f)) {
			canJump = true;
		}

		if (Input.GetKeyDown (KeyCode.Space) && canJump) {
			canJump = false;
			GetComponent<Rigidbody> ().AddForce (Vector3.up * jumpingForce);
		} 
	}
}
