using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Animator animator;
	private Rigidbody2D r2;
	public Transform planet;

	public float speed = 100;
	public float gravity = 1;
	private float worldSpeed = 0;
	private bool grounded = false;

	// Use this for initialization
	void Start()
	{
		animator = this.GetComponent<Animator>();
		r2 = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update()
	{
		// Gravity
		r2.AddForce((planet.position - transform.position).normalized * gravity);

		// Jumping
		if (Input.GetKeyDown ("space")) {
			r2.AddForce((planet.position - transform.position).normalized * speed *-1);
		}

		// Cancel animation while jumping
		if(transform.position.y < 1.5f && !grounded) {
			grounded = true;
		}else if(transform.position.y > 1.5f && grounded)
		{
			grounded = false;
		}

		// Running Animation
		if (grounded) {

			worldSpeed = planet.GetComponent<Rotate> ().rotate_speed; 
			worldSpeed = Normalize (worldSpeed);
			animator.SetFloat ("Speed", worldSpeed);
		} else if(!grounded && worldSpeed != 0f) {

			// Set to 0 to make sure we dont continually update
			worldSpeed = 0f;
			animator.SetFloat ("Speed", 0f);
		}
	}

	/*
	 * Normalize number between 0 and 1
	 */ 
	float Normalize(float speed)
	{
		return (speed - 5f)/(20f - 5f);
	}
}
