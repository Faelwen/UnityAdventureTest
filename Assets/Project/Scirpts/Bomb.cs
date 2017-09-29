using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

	public float duration = 5f;
	public float explosionRadius = 3f;
	public float explosionDuration = 0.25f;
	public GameObject explosionModel;

	private float explosionTimer;
	private bool exploded;

	// Use this for initialization
	void Start ()
	{
		explosionTimer = duration;
		explosionModel.transform.localScale = Vector3.one * explosionRadius;
		explosionModel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		explosionTimer -= Time.deltaTime;
		if (explosionTimer <= 0 && exploded == false) {
			exploded = true;
			Collider[] hitObjects = Physics.OverlapSphere (transform.position, explosionRadius);
			foreach (Collider collider in hitObjects) {
				Debug.Log (collider.name);
			}
			StartCoroutine (Explode ());
		}
	}

	private IEnumerator Explode ()
	{
		explosionModel.SetActive (true);
		yield return new WaitForSeconds (explosionDuration);
		Destroy (gameObject);
	}
}
