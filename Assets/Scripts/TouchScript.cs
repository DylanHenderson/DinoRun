using UnityEngine;
using System.Collections;

public enum Swipe { None, Up, Down, Left, Right };

public class TouchScript : MonoBehaviour {

	public Transform player;
	public Transform planet;
	public float speed =100;
	public bool tapAction = false;
	public float minSwipeLength = 200f;
	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;
	public static Swipe swipeDirection;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		// If right charge, can be used for all special effects
		if(swipeDirection == Swipe.Right)
		{
			Debug.Log ("Swipe Right");
			swipeDirection = Swipe.None;
		}
	}

	void OnMouseDown() 
	{
		if(tapAction)
		{
			player.GetComponent<Rigidbody2D>().AddForce((planet.position - player.position).normalized * speed *-1);
		}else{

			firstPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
		}
	}

	void OnMouseUp()
	{
		
		if (!tapAction) {
			secondPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
			currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

			// Make sure it was a legit swipe, not a tap
			if (currentSwipe.magnitude < minSwipeLength) {
				swipeDirection = Swipe.None;
				return;
			}

			currentSwipe.Normalize();
			
			// Swipe up
			if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
				swipeDirection = Swipe.Up;
				// Swipe down
			} else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
				swipeDirection = Swipe.Down;
				// Swipe left
			} else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
				swipeDirection = Swipe.Left;
				// Swipe right
			} else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
				swipeDirection = Swipe.Right;
			}
			
		}
	}
}
