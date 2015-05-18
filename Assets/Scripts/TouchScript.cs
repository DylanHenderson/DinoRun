using UnityEngine;
using System.Collections;

public enum Swipe { None, Up, Down, Left, Right };

public class TouchScript : MonoBehaviour {

	public Transform player;
	public Transform planet;
	public Transform pauseScreen;
	public float speed = 100f;
	public float minSwipeLength = 200f;
	public GameObject gameController;
	Countdown gameCountdown;
	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;
	public static Swipe swipeDirection;
	private Vector3 originalPositon;
	public bool inGame = true;
	public bool characterSelection = false;

	private RaycastHit hit;
	private Ray ray;
	public Camera titleCamera;

	// Use this for initialization
	void Start () {

		if (inGame) {
			gameCountdown = gameController.GetComponent<Countdown> ();
		} else {
			PlayerPrefs.SetString("Dino", "Raptor");
		}
		originalPositon = player.position;
	}
	
	// Update is called once per frame
	void Update () {	

		//Check if we are running either in the Unity editor or in a standalone build.
		//#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
		editorClick ();
		//#endif

		//Check if we are running on iOS, Android, Windows Phone 8 or Unity iPhone
		//#if UNITY_IOS || UNITY_ANDROID || UNITY_IPHONE
		//mobileTouch ();
		//#endif
	}

	void editorClick()
	{
		if (Input.GetMouseButtonDown(0) ) {

			ray = titleCamera.ScreenPointToRay (Input.mousePosition);

			if(Physics.Raycast (ray.origin, ray.direction, out hit))
			{
				buttonTouch(hit.collider.name);
			}
		}
			
		if (Input.GetMouseButtonUp (0)) {
				
			ray = titleCamera.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray.origin, ray.direction, out hit)) {
				switch (hit.collider.name) {
				case "Tap Area":
					
					secondPressPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
					currentSwipe = new Vector3 (secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
					
					// Make sure it was a legit swipe, not a tap
					if (currentSwipe.magnitude < minSwipeLength) {
						swipeDirection = Swipe.None;
						return;
					}

					currentSwipe.Normalize ();

					swipeAction(currentSwipe.x, currentSwipe.y);
					
					break;
				}
				
			}
		}
	}

	void mobileTouch()
	{
		if (Input.touches.Length > 0) {
			Touch touch = Input.touches [0];
			ray = titleCamera.ScreenPointToRay (Input.touches [0].position);
			
			if (touch.phase == TouchPhase.Began && Physics.Raycast (ray.origin, ray.direction, out hit)) {
				buttonTouch(hit.collider.name);
			}
			
			if (touch.phase == TouchPhase.Ended  && Physics.Raycast (ray.origin, ray.direction, out hit)) {
				
				
				switch(hit.collider.name){
				case "Tap Area":
					
					secondPressPos = new Vector2(touch.position.x, touch.position.y);
					currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
					
					// Make sure it was a legit swipe, not a tap
					if (currentSwipe.magnitude < minSwipeLength) {
						swipeDirection = Swipe.None;
						return;
					}
					
					currentSwipe.Normalize();
					
					swipeAction(currentSwipe.x, currentSwipe.y);
					
					break;
				}
				
			}
		} else if(swipeDirection != Swipe.None) {
			swipeDirection = Swipe.None;   
		}
	}

	void previousDino()
	{
		planet.transform.Rotate (Vector3.forward   * 180f, Space.Self);
		
		
		if(PlayerPrefs.GetString("Dino").Equals("Raptor"))
		{
			PlayerPrefs.SetFloat("RotationValue", 180f);
			PlayerPrefs.SetString("Dino", "Triceratops");
		}else{
			PlayerPrefs.SetFloat("RotationValue", 0f);
			PlayerPrefs.SetString("Dino", "Raptor");
		}

	}
	
	void nextDino()
	{
		planet.transform.Rotate (Vector3.back  * 180f, Space.Self);
		
		
		if(PlayerPrefs.GetString("Dino").Equals("Raptor"))
		{
			PlayerPrefs.SetFloat("RotationValue", 180f);
			PlayerPrefs.SetString("Dino", "Triceratops");
		}else{
			PlayerPrefs.SetFloat("RotationValue", 0f);
			PlayerPrefs.SetString("Dino", "Raptor");
		}

	}

	
	void buttonTouch(string name)
	{
		switch(name){
		case "Tap Area":	

			if(player.GetComponent<PlayerController>().isUsingPower())
			{
				player.GetComponent<PlayerController>().cancelPower();
			}

			if(player.GetComponent<PlayerController>().grounded || (player.position.y > originalPositon.y && player.GetComponent<PlayerController>().collidingObject != null))
			{
				//player.GetComponent<Rigidbody2D>().AddForce((planet.position - player.position).normalized * speed *-1);
				player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 6);
			}

			firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

			break;
		case "Pause":
			Time.timeScale = 0;
			pauseScreen.gameObject.SetActive(true);
			break;
		case "Play":
			Time.timeScale = 1;
			gameCountdown.startCountdown();
			pauseScreen.gameObject.SetActive(false);
			break;
		case "Previous":
			previousDino();
			break;
		case "Next":
			nextDino ();
			break;
		case "Main Menu":
			Time.timeScale = 1;
			Application.LoadLevel("Menu");
			break;
		case "Selection play":
			Time.timeScale = 1;
			Application.LoadLevel("DinoRun");
			break;
		case "Tap to play":
			Application.LoadLevel("CharacterSelection");
			break;
		case "Restart":
			Time.timeScale = 1;
			Application.LoadLevel("DinoRun");
			break;
		}
	}
	
	void swipeAction(float x, float y)
	{
		// Swipe up
		if (y > 0 && x > -0.5f && x < 0.5f) {
			player.GetComponent<PlayerController>().setPower();
			
			// Swipe down
		} else if (y < 0 && x > -0.5f && x < 0.5f) {
			
			//player.GetComponent<PlayerController>().cancelPower();
			
			// Swipe left
		} else if (y < 0 && y > -0.5f && y < 0.5f) {

			if(characterSelection)
			{
				previousDino();
			}

			// Swipe right
		} else if (y > 0 && y > -0.5f && y < 0.5f) {
			
			if(characterSelection)
			{
				nextDino();
			}
		}
	}
		
}
