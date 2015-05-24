using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public enum DinoType {Raptor, Triceratops};

	public DinoType dinoType;
	public Transform planet;
	public Transform doom;
	public Transform collidingObject;
	public Transform movingBackround1;
	public Transform movingBackround2;
	public Sprite jump;
	public Sprite defaultSprite;
	public GameObject bird;
	public GameObject gameController;
	public GameObject power_bar;
	public Transform start_point;
	public float bird_speed;
	public float speed = 100;
	public float gravity = 1;
	public bool grounded = false;
	public GameObject bird_swoosh;
	public Transform bird_swoosh_start;
	public bool inGame = true;
	public GameObject touchcontrols;

	private Animator animator;
	private Rigidbody2D r2;
	private float worldSpeed = 0;
	private bool usingPower;
	private float original_gravity;
	private float original_height;
	private PowerUpBar pb;
	private Vector3 end_point;
	private float original_bird_x;
	private float current_dino_x;
	private SpriteRenderer spr;
	private bool red;
	private TouchScript ts;
	
	void Start () {
		red = false;
		spr = gameObject.GetComponent<SpriteRenderer>();
		ts = touchcontrols.GetComponent<TouchScript>();
		animator = this.GetComponent<Animator>();
		r2 = GetComponent<Rigidbody2D>();
		usingPower = false;
		original_gravity = gravity;
		original_height = gameObject.transform.position.y;
		//power_bar.GetComponent<PowerUpBar>().

		if (inGame) {
			pb = power_bar.GetComponent<PowerUpBar> ();
			original_bird_x = bird.transform.position.x;
			current_dino_x = gameObject.transform.position.x;
		} else {

			// Set animation for the main menu
			animator.SetFloat ("Speed", 0.5f);
		}
	}

	void FixedUpdate()
	{
		//print (ts.paused);
		if (ts.paused == true) {
			gamePaused ();
		}

		// Gravity
		r2.AddForce ((planet.position - transform.position).normalized * gravity);

		if (inGame) {

			// Jumping
			if (Input.GetKeyDown ("space") && grounded) {
				r2.AddForce ((planet.position - transform.position).normalized * speed * -1);
			}
			
			//when player is usingPower and still has power left
			if (usingPower && pb.canUsePower ()) {
				
				if(dinoType == DinoType.Raptor){
					gameObject.transform.position = new Vector2 (current_dino_x, original_height + 1);
					end_point = new Vector3 (gameObject.transform.position.x, bird.transform.position.y, bird.transform.position.z);
					
					
					
					bird.transform.position = Vector3.Lerp (start_point.position, end_point, bird_speed);
				}else{
					
					red = true;
				}
				
				//can no longer fly push to ground, send bird back to position	
			} else if (inGame) {
				usingPower = false;
				if(dinoType == DinoType.Raptor){
					usingPower = false;
					gravity = original_gravity;
					//pb.cancelDecrease();
					bird.transform.position = new Vector2 (original_bird_x, bird.transform.position.y);
				}else{
					spr.color = new Color(255f, 255f, 255f, 1f);
					red = false;
					
				}
			}
			
			// Cancel animation while jumping
			if ((transform.position.y < 1.5f && !grounded)) {

				ts.playLandSound();
				grounded = true;
				animator.enabled = true;
				transform.GetComponent<SpriteRenderer> ().sprite = defaultSprite;
			} else if (transform.position.y > 1.5f && grounded) {
				grounded = false;
			}
			
			// Running Animation
			if (grounded && collidingObject == null) {
				
				worldSpeed = planet.GetComponent<Rotate> ().rotate_speed; 
				worldSpeed = Normalize (worldSpeed);
				animator.SetFloat ("Speed", worldSpeed);
			} else if (!grounded && worldSpeed != 0f) {
				
				// Set to 0 to make sure we dont continually update
				worldSpeed = 0f;
				animator.SetFloat ("Speed", 0f);
				
				// If moving upwards
				if (r2.velocity.y >= -10E-09) {
					animator.enabled = false;
					transform.GetComponent<SpriteRenderer> ().sprite = jump;
				}
			}
		}
	}

	// Update is called once per frame
	void Update()
	{}

	public bool isUsingPower()
	{
		return usingPower;
	}

	public void setPower()
	{
		if(pb.canUsePower())
		{
			// Will use flying power else dash
			if(dinoType == DinoType.Raptor)
			{
				usingPower = true;
				gravity = 0;
				pb.initiateDecrease();
				current_dino_x = gameObject.transform.position.x;
				GameObject swoosh = Instantiate(bird_swoosh,bird_swoosh_start.position,Quaternion.identity) as GameObject;
				
				Destroy (swoosh, 5);
			}else{
				usingPower = true;
				red = true;
				//gravity = 0;
				pb.initiateDecrease();
				//current_dino_x = gameObject.transform.position.x;
				//GameObject swoosh = Instantiate(bird_swoosh,bird_swoosh_start.position,Quaternion.identity) as GameObject;
				spr.color = new Color(255f, 0f, 0f, 1f);
				//Destroy (swoosh, 5);
			}
		}
	}

	public void cancelPower()
	{
		usingPower = false;
		gravity = original_gravity;
		pb.cancelDecrease();

		// Will cancel flying power else dash
		if(dinoType == DinoType.Raptor)
		{
			bird.transform.position = new Vector2 (original_bird_x, bird.transform.position.y);
		}else{
			
		}
	}


	/*
	 * Normalize number between 0 and 1
	 */ 
	float Normalize(float speed)
	{
		return (speed - 5f)/(20f - 5f);
	}

	void OnTriggerEnter2D(Collider2D collisionInfo)
	{
		if(collisionInfo.name == "pit(Clone)")
		{
			Debug.Log("Here");

			// Activate the child collider
			foreach (Transform child in collisionInfo.transform) {
				child.gameObject.SetActive(true);
			}

			// Disable world collider
			planet.GetComponent<CircleCollider2D>().enabled = false;
		}
	}

	void OnCollisionEnter2D(Collision2D collisionInfo)
	{
		if(collisionInfo.collider.name != "moving-Sphere")
		{
			collidingObject = collisionInfo.collider.transform;

			// DOOM
			if(collisionInfo.collider.tag == "doom" )
			{
				// End the game if hit by doom
				gameController.GetComponent<GameControl>().isGameOver = true;
			}

			// Jumping stuff
			if(transform.position.y > 1.5f)
			{
				grounded = true;
				animator.enabled = true;
				transform.GetComponent<SpriteRenderer>().sprite = defaultSprite;
			}else{



				// World speed stuff
				if(collisionInfo.collider.tag == "hard" )
				{
					ts.playHitSound();

					if(red){
						Destroy (collisionInfo.collider.gameObject);
						
					}else{
						
						
						
						print ("hard collider");
						animator.SetFloat ("Speed", 0f);
						planet.GetComponent<Rotate>().rotating = false;
						//planet.GetComponent<Rotate>().originalSpeed = planet.GetComponent<Rotate>().rotate_speed;
						
						movingBackround1.GetComponent<MoveBackground>().moving = false;
						movingBackround1.GetComponent<MoveBackground>().originalSpeed = movingBackround1.GetComponent<MoveBackground>().move_speed;
						
						movingBackround2.GetComponent<MoveBackground>().moving = false;
						movingBackround2.GetComponent<MoveBackground>().originalSpeed = movingBackround2.GetComponent<MoveBackground>().move_speed;
						
					}
				}



				if(collisionInfo.collider.tag == "bush" ){

					ts.playHitSound();

					if(red){
						Destroy (collisionInfo.collider.gameObject);
						
					}else{


						print ("colliding");
						animator.SetFloat ("Speed", 0.1f);

						float decreasedSpeed = 0f;

						if(dinoType == DinoType.Raptor)
						{
							decreasedSpeed = 0.5f;
						}else
						{
							decreasedSpeed = 0.75f;
						}


						if(movingBackround1.GetComponent<MoveBackground>().moving ==true){
							doom.GetComponent<Rotate>().rotate_speed -= 0.0025f;
						}

						Physics2D.IgnoreCollision(collisionInfo.collider, GetComponent<BoxCollider2D>());
						//planet.GetComponent<Rotate>().originalSpeed = planet.GetComponent<Rotate>().rotate_speed;
						movingBackround1.GetComponent<MoveBackground>().originalSpeed = movingBackround1.GetComponent<MoveBackground>().move_speed;
						movingBackround2.GetComponent<MoveBackground>().originalSpeed = movingBackround2.GetComponent<MoveBackground>().move_speed;
						
						
						movingBackround1.GetComponent<MoveBackground>().moving = true;
						movingBackround1.GetComponent<MoveBackground>().move_speed = movingBackround1.GetComponent<MoveBackground>().originalSpeed * decreasedSpeed;
						
						movingBackround2.GetComponent<MoveBackground>().moving = true;
						movingBackround2.GetComponent<MoveBackground>().move_speed = movingBackround2.GetComponent<MoveBackground>().originalSpeed * decreasedSpeed;
						
						planet.GetComponent<Rotate>().rotating = true;
						planet.GetComponent<Rotate>().rotate_speed = planet.GetComponent<Rotate>().originalSpeed * decreasedSpeed;
						
						Invoke ("resetSpeed", decreasedSpeed);



					}
				}
			
			}

		}
	}



	void OnCollisionStay2D(Collision2D collisionInfo)
	{
		if(collisionInfo.collider.tag == "hard")
		{
			animator.SetFloat ("Speed", 0f);
			CancelInvoke ("resetSpeed");

				doom.GetComponent<Rotate>().rotate_speed -= 0.0005f;

		}

		if(collisionInfo.collider.tag == "bush" ){
			
			print ("colliding");
			animator.SetFloat ("Speed", 0.1f);
			Physics2D.IgnoreCollision(collisionInfo.collider, GetComponent<BoxCollider2D>());
			//Invoke ("resetSpeed",0.5f);

			
		}
	}

	void OnCollisionExit2D(Collision2D collisionInfo)
	{
		if(collisionInfo.collider.tag == "hard" )
		{
			planet.GetComponent<Rotate>().rotating = true;
			planet.GetComponent<Rotate>().rotate_speed = planet.GetComponent<Rotate>().originalSpeed;

			movingBackround1.GetComponent<MoveBackground>().moving = true;
			movingBackround1.GetComponent<MoveBackground>().move_speed = movingBackround1.GetComponent<MoveBackground>().originalSpeed;

			movingBackround2.GetComponent<MoveBackground>().moving = true;
			movingBackround2.GetComponent<MoveBackground>().move_speed = movingBackround2.GetComponent<MoveBackground>().originalSpeed;
			Invoke ("resetSpeed",0.5f);



		}

		doom.GetComponent<Rotate>().rotate_speed = 0.0f;



		collidingObject = null;
	}

	public void gamePaused(){
		doom.GetComponent<Rotate>().rotate_speed = 0.0f;

	}

	void resetSpeed(){
		planet.GetComponent<Rotate>().rotating = true;
		planet.GetComponent<Rotate>().rotate_speed = planet.GetComponent<Rotate>().originalSpeed;
		
		movingBackround1.GetComponent<MoveBackground>().moving = true;
		movingBackround1.GetComponent<MoveBackground>().move_speed = movingBackround1.GetComponent<MoveBackground>().originalSpeed;
		
		movingBackround2.GetComponent<MoveBackground>().moving = true;
		movingBackround2.GetComponent<MoveBackground>().move_speed = movingBackround2.GetComponent<MoveBackground>().originalSpeed;
	}
}
