using UnityEngine;
using System.Collections;

public enum Swipe { None, Up, Down, Left, Right };

public class TouchScript : MonoBehaviour {

	public Transform player;
	public Transform planet;
	public Transform pauseScreen;
	public float speed =100;
	public float minSwipeLength = 200f;
	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;
	public static Swipe swipeDirection;

	private RaycastHit hit;
	private Ray ray;
	public Camera titleCamera;
	
	// Use this for initialization
	void Start () {
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
				switch(hit.collider.name){
				case "Swipe Area":
					firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
					break;
				case "Tap Area":	
					player.GetComponent<Rigidbody2D>().AddForce((planet.position - player.position).normalized * speed *-1);
					break;
				case "Pause":
					Time.timeScale = 0;
					pauseScreen.gameObject.SetActive(true);
					break;
				case "Play":
					Time.timeScale = 1;
					pauseScreen.gameObject.SetActive(false);
					break;
				case "Restart":
					Time.timeScale = 1;
					Application.LoadLevel("DinoRun");
				break;
			}
			}
		}
			
		if (Input.GetMouseButtonUp (0)) {
				
			ray = titleCamera.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray.origin, ray.direction, out hit)) {
				switch (hit.collider.name) {
				case "Swipe Area":
					
					secondPressPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
					currentSwipe = new Vector3 (secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
					
					// Make sure it was a legit swipe, not a tap
					if (currentSwipe.magnitude < minSwipeLength) {
						swipeDirection = Swipe.None;
						return;
					}
					
					currentSwipe.Normalize ();
					
					// Swipe up
					if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
						//swipeDirection = Swipe.Up;
						Debug.Log ("Swipe Up");
						// Swipe down
					} else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
						//swipeDirection = Swipe.Down;
						Debug.Log ("Swipe Down");
						// Swipe left
					} else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
						//swipeDirection = Swipe.Left;
						Debug.Log ("Swipe Left");
						// Swipe right
					} else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
						//swipeDirection = Swipe.Right;
						
						Debug.Log ("Swipe Right");
					}
					
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
				
				switch(hit.collider.name){
				case "Swipe Area":
					firstPressPos = new Vector2(touch.position.x, touch.position.y);
					break;
				case "Tap Area":
					player.GetComponent<Rigidbody2D>().AddForce((planet.position - player.position).normalized * speed *-1);
					break;
				case "Pause Button":

					if(Time.timeScale == 0)
					{
						Time.timeScale = 1;
					}else
					{
						Time.timeScale = 0;
					}

					break;
				}
			}
			
			if (touch.phase == TouchPhase.Ended  && Physics.Raycast (ray.origin, ray.direction, out hit)) {
				
				
				switch(hit.collider.name){
				case "Swipe Area":
					
					secondPressPos = new Vector2(touch.position.x, touch.position.y);
					currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
					
					// Make sure it was a legit swipe, not a tap
					if (currentSwipe.magnitude < minSwipeLength) {
						swipeDirection = Swipe.None;
						return;
					}
					
					currentSwipe.Normalize();
					
					// Swipe up
					if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
						//swipeDirection = Swipe.Up;
						Debug.Log ("Swipe Up");
						// Swipe down
					} else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
						//swipeDirection = Swipe.Down;
						Debug.Log ("Swipe Down");
						// Swipe left
					} else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
						//swipeDirection = Swipe.Left;
						Debug.Log ("Swipe Left");
						// Swipe right
					} else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
						//swipeDirection = Swipe.Right;
						
						Debug.Log ("Swipe Right");
					}
					
					break;
				}
				
			}
		} else if(swipeDirection != Swipe.None) {
			swipeDirection = Swipe.None;   
		}
	}
		
}
