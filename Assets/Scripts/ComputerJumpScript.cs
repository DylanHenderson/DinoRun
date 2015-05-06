using UnityEngine;
using System.Collections;

public class ComputerJumpScript : MonoBehaviour {

	public float speed =100;
	public float gravity = 1;
	public Transform planet;
	// Use this for initialization
	private Rigidbody2D r2;
	void Start () {
		r2 = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

		r2.AddForce((planet.position - transform.position).normalized * gravity);
		if (Input.GetKeyDown ("space")) {

			//r2.AddForce(transform.up * speed);
			r2.AddForce((planet.position - transform.position).normalized * speed *-1);


		}
	}
}
