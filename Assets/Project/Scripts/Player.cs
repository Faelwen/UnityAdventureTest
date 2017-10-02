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
	public float knockbackForce;

	[Header ("Equipment")]
	public int health = 10;
	public Sword sword;
	public Bow bow;
	public GameObject bombPrefab;
	public float throwingSpeed;
	public int bombAmount = 5;
	public int arrowAmount = 20;


	private Rigidbody playerRigidBody;
	private Quaternion targetModelRotation;
	private bool canJump;
	private float knowckbackTimer;

	// Use this for initialization
	void Start ()
	{
		bow.gameObject.SetActive (false);
		playerRigidBody = GetComponent<Rigidbody> ();
		targetModelRotation = Quaternion.Euler (0, 0, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		model.transform.rotation = Quaternion.Lerp (model.transform.rotation, targetModelRotation, Time.deltaTime * rotatingSpeed);

		if (knowckbackTimer > 0) {
			knowckbackTimer -= Time.deltaTime;
		} else {
			ProcessInput ();
		}
	}

	void ProcessInput ()
	{
		playerRigidBody.velocity = new Vector3 (0, playerRigidBody.velocity.y, 0);


		if (Input.GetKey (KeyCode.RightArrow)) {
			playerRigidBody.velocity = new Vector3 (movingVelocity, playerRigidBody.velocity.y, playerRigidBody.velocity.z);
			targetModelRotation = Quaternion.Euler (0, 90, 0);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			playerRigidBody.velocity = new Vector3 (-movingVelocity, playerRigidBody.velocity.y, playerRigidBody.velocity.z);
			targetModelRotation = Quaternion.Euler (0, 270, 0);
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			playerRigidBody.velocity = new Vector3 (playerRigidBody.velocity.x, playerRigidBody.velocity.y, movingVelocity);
			targetModelRotation = Quaternion.Euler (0, 0, 0);
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			playerRigidBody.velocity = new Vector3 (playerRigidBody.velocity.x, playerRigidBody.velocity.y, -movingVelocity);
			targetModelRotation = Quaternion.Euler (0, 180, 0);
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
			sword.gameObject.SetActive (true);
			bow.gameObject.SetActive (false);
			sword.Attack ();
		}

		if (Input.GetKeyDown (KeyCode.X)) {
			if (arrowAmount > 0) {
				bow.gameObject.SetActive (true);
				sword.gameObject.SetActive (false);
				bow.Attack ();
				arrowAmount--;
			}
		}

		if (Input.GetKeyDown (KeyCode.C)) {
			ThrowBomb ();
		}
	}

	private void ThrowBomb ()
	{
		if (bombAmount <= 0) {
			return;
		}

		GameObject bombObject = Instantiate (bombPrefab);
		bombObject.transform.position = transform.position + model.transform.forward;
		Vector3 throwingDirection = (model.transform.forward + Vector3.up).normalized;
		bombObject.GetComponent<Rigidbody> ().AddForce (throwingDirection * throwingSpeed);
		bombAmount--;
	}

	void OnTriggerEnter (Collider otherCollider)
	{
		if (otherCollider.GetComponent<EnemyBullet> () != null) {
			Hit ((transform.position - otherCollider.transform.position).normalized);
		}
	}

	void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.GetComponent <Enemy> () != null) {
			Hit ((transform.position - collision.transform.position).normalized);
		}
	}

	private void Hit (Vector3 direction)
	{
		Vector3 knockbackDirection = (direction + Vector3.up).normalized;
		playerRigidBody.AddForce (knockbackDirection * knockbackForce);
		knowckbackTimer = 1f;

		health--;
		if (health <= 0) {
			Destroy (gameObject);
		}
	}
}
