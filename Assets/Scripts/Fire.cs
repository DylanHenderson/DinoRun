using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
	private GameObject gameController;
	// Use this for initialization
	void Start () {
		gameController = GameObject.Find("GameController");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter2D(Collision2D collisionInfo){
		if (collisionInfo.collider.name == "Player") {

			gameController.GetComponent<GameControl>().isGameOver = true; 
		}

	}

}
