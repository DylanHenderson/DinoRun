using UnityEngine;
using System.Collections;

public class ComputerJumpScript : MonoBehaviour {



	public float speed =100;
	public float gravity = 1;
	public Transform planet;


	public GameObject power_bar;


	// Use this for initialization
	private Rigidbody2D r2;
	private bool flying;
	private float original_gravity;
	private float original_height;
	private PowerUpBar pb;


	void Start () {
		r2 = GetComponent<Rigidbody2D>();
		flying = false;
		original_gravity = gravity;
		original_height = gameObject.transform.position.y;
		//power_bar.GetComponent<PowerUpBar>().

		pb = power_bar.GetComponent<PowerUpBar> ();

	}
	
	// Update is called once per frame
	void Update () {

		r2.AddForce((planet.position - transform.position).normalized * gravity);
		if (Input.GetKeyDown ("space") && !flying) {

			//r2.AddForce(transform.up * speed);
			r2.AddForce((planet.position - transform.position).normalized * speed *-1);


		}

		if (flying && pb.getcanFly ()) {

			gameObject.transform.position = new Vector2 (gameObject.transform.position.x, original_height + 1);
		} else {
			flying = false;
			gravity = original_gravity;
			pb.cancelDecrease();
			
		}

		if (Input.GetKeyDown ("x") && pb.getcanFly ()){
			flying = true;
			gravity = 0;
			pb.initiateDecrease();



		}

		if (Input.GetKeyUp ("x")) {
			flying = false;
			gravity = original_gravity;
			pb.cancelDecrease();

			
		}
	}
}
