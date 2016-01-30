using UnityEngine;
using System.Collections;

public class Decelerater : MonoBehaviour {

	public float fDecelerateSpeed = 0.8f;

	private Rigidbody2D rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate ()
	{
		rb.velocity = rb.velocity * fDecelerateSpeed;
	}
}
