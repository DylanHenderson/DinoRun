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

	private RaycastHit hit;
	private Ray ray;
	public Camera titleCamera;
	
	// Use this for initialization
	void Start () {
		gameCountdown = gameController.GetComponent<Countdown>();
		gameCountdown.startCountdown();
		originalPositon = player.position;
	}
	
	// Update is called once per frame
	void Update () {	

		//Check if we are running either in the Unity editor or in a standalone build.
		#if UNITY_STANDALONE || UNITY_WEBPLAYER
		editorClick ();

		//Check if we are running on iOS, Android, Windows Phone 8 or Unity iPhone
		#elif UNITY_IOS || UNITY_ANDROID || UNITY_IPHONE
		mobileTouch ();

		#endif
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
				player.GetComponent<Rigidbody2D>().AddForce((planet.position - player.position).normalized * speed *-1);
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
		case "Main Menu":
			Time.timeScale = 1;
			Application.LoadLevel("Menu");
			break;
		case "Menu Play":
			Time.timeScale = 1;
			Application.LoadLevel("DinoRun");
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
			
			// Swipe right
		} else if (y > 0 && y > -0.5f && y < 0.5f) {
			
		}
	}
		
}
