using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public enum DinoType {Raptor, Triceratops};

	public DinoType dinoType;
	public Transform planet;
	public Transform collidingObject;
	public Transform movingBackround1;
	public Transform movingBackround2;
	public Sprite jump;
	public Sprite defaultSprite;
	public GameObject bird;
	public GameObject power_bar;
	public Transform start_point;
	public float bird_speed;
	public float speed = 100;
	public float gravity = 1;
	public bool grounded = false;

	private Animator animator;
	private Rigidbody2D r2;
	private float worldSpeed = 0;
	private bool flying;
	private float original_gravity;
	private float original_height;
	private PowerUpBar pb;
	private Vector3 end_point;
	private float original_bird_x;
	private float current_dino_x;
	
	void Start () {
		animator = this.GetComponent<Animator>();
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
	void Update()
	{
		// Gravity
		r2.AddForce((planet.position - transform.position).normalized * gravity);

		// Jumping
		if (Input.GetKeyDown ("space") && grounded) {
			r2.AddForce((planet.position - transform.position).normalized * speed *-1);
		}

		//when player is flying and still has power left
		if (flying && pb.getcanFly ()) {
			
			gameObject.transform.position = new Vector2 (current_dino_x, original_height + 1);
			end_point = new Vector3(gameObject.transform.position.x,bird.transform.position.y,bird.transform.position.z);
			
			
			
			bird.transform.position = Vector3.Lerp(start_point.position,end_point,bird_speed);
			
		//can no longer fly push to ground, send bird back to position	
		} else {
			flying = false;
			gravity = original_gravity;
			//pb.cancelDecrease();
			bird.transform.position = new Vector2 (original_bird_x, bird.transform.position.y);
		}

		// Cancel animation while jumping
		if((transform.position.y < 1.5f && !grounded)) {

			grounded = true;
			animator.enabled = true;
			transform.GetComponent<SpriteRenderer>().sprite = defaultSprite;
		}else if(transform.position.y > 1.5f && grounded)
		{
			grounded = false;
		}

		// Running Animation
		if (grounded && collidingObject == null) {

			worldSpeed = planet.GetComponent<Rotate> ().rotate_speed; 
			worldSpeed = Normalize (worldSpeed);
			animator.SetFloat ("Speed", worldSpeed);
		} else if(!grounded && worldSpeed != 0f) {

			// Set to 0 to make sure we dont continually update
			worldSpeed = 0f;
			animator.SetFloat ("Speed", 0f);

			// If moving upwards
			if(r2.velocity.y >= -10E-09)
			{
				animator.enabled = false;
				transform.GetComponent<SpriteRenderer>().sprite = jump;
			}
		}
	}

	public bool isUsingPower()
	{
		return flying;
	}

	public void setPower()
	{
		if(pb.getcanFly ())
		{
			flying = true;
			gravity = 0;
			pb.initiateDecrease();
			current_dino_x = gameObject.transform.position.x;
		}
	}

	public void cancelPower()
	{
		flying = false;
		gravity = original_gravity;
		pb.cancelDecrease();
		bird.transform.position = new Vector2 (original_bird_x, bird.transform.position.y);
	}


	/*
	 * Normalize number between 0 and 1
	 */ 
	float Normalize(float speed)
	{
		return (speed - 5f)/(20f - 5f);
	}

	void OnCollisionEnter2D(Collision2D collisionInfo)
	{
		if(collisionInfo.collider.name != "moving-Sphere")
		{
			collidingObject = collisionInfo.collider.transform;

			// Jumping stuff
			if(transform.position.y > 1.5f)
			{
				grounded = true;
				animator.enabled = true;
				transform.GetComponent<SpriteRenderer>().sprite = defaultSprite;
			}else{

				// World speed stuff
				if(collisionInfo.collider.name == "test-obstacle-rock(Clone)" || collisionInfo.collider.name == "tree(Clone)" )
				{
					animator.SetFloat ("Speed", 0f);
					planet.GetComponent<Rotate>().rotating = false;
					movingBackround1.GetComponent<MoveBackground>().moving = false;
					movingBackround2.GetComponent<MoveBackground>().moving = false;
				}else if(collisionInfo.collider.name == "pit(Clone)")
				{
					// Diable collider
					transform.GetComponent<BoxCollider2D>().enabled = false;
				}
			
			}

			float decreaseAmount;
			
			if(dinoType == DinoType.Raptor)
			{
				
			}

		}
	}

	void OnCollisionStay2D(Collision2D collisionInfo)
	{
		if(collisionInfo.collider.name == "test-obstacle-rock(Clone)" || collisionInfo.collider.name == "tree(Clone)")
		{
			animator.SetFloat ("Speed", 0f);
		}
	}

	void OnCollisionExit2D(Collision2D collisionInfo)
	{
		if(collisionInfo.collider.name == "test-obstacle-rock(Clone)" || collisionInfo.collider.name == "tree(Clone)" )
		{
			planet.GetComponent<Rotate>().rotating = true;
			planet.GetComponent<Rotate>().rotate_speed = planet.GetComponent<Rotate>().originalSpeed;

			movingBackround1.GetComponent<MoveBackground>().moving = true;
			movingBackround1.GetComponent<MoveBackground>().move_speed = movingBackround1.GetComponent<MoveBackground>().originalSpeed;

			movingBackround2.GetComponent<MoveBackground>().moving = true;
			movingBackround2.GetComponent<MoveBackground>().move_speed = movingBackround2.GetComponent<MoveBackground>().originalSpeed;
		}

		collidingObject = null;
	}
}
