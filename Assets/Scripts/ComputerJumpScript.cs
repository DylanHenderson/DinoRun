using UnityEngine;
using System.Collections;

public class ComputerJumpScript : MonoBehaviour {



	public float speed =100;
	public float gravity = 1;
	public Transform planet;
	public GameObject bird;

	public GameObject power_bar;
	public Transform start_point;
	public float bird_speed;


	// Use this for initialization
	private Rigidbody2D r2;
	private bool flying;
	private float original_gravity;
	private float original_height;
	private PowerUpBar pb;
	private Vector3 end_point;
	private float original_bird_x;
	private float current_dino_x;


	void Start () {
		r2 = GetComponent<Rigidbody2D>();
		flying = false;
		original_gravity = gravity;
		original_height = gameObject.transform.position.y;
		//power_bar.GetComponent<PowerUpBar>().

		pb = power_bar.GetComponent<PowerUpBar> ();
		original_bird_x = bird.transform.position.x;
		current_dino_x = gameObject.transform.position.x;

	}
	
	// Update is called once per frame
	void Update () {

		r2.AddForce((planet.position - transform.position).normalized * gravity);
		if (Input.GetKeyDown ("space") && !flying) {

			//r2.AddForce(transform.up * speed);
			r2.AddForce((planet.position - transform.position).normalized * speed *-1);


		}

		if (flying && pb.getcanFly ()) {

			gameObject.transform.position = new Vector2 (current_dino_x, original_height + 1);
			end_point = new Vector3(gameObject.transform.position.x,bird.transform.position.y,bird.transform.position.z);



			bird.transform.position = Vector3.Lerp(start_point.position,end_point,bird_speed);


		} else {
			flying = false;
			gravity = original_gravity;
			bird.transform.position = new Vector2 (original_bird_x, bird.transform.position.y);
		}

		if (Input.GetKeyDown ("x") && pb.getcanFly ()){
			flying = true;
			gravity = 0;
			pb.initiateDecrease();
			current_dino_x = gameObject.transform.position.x;


		}

		if (Input.GetKeyUp ("x")) {
			print ("key is up");
			flying = false;
			gravity = original_gravity;
			pb.cancelDecrease();
			bird.transform.position = new Vector2 (original_bird_x, bird.transform.position.y);

			
		}
	}
}
