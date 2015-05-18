using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

	public GameObject gameOverScreen;
	public Transform player;
	public GUISkin skin;
	public float deathPositionX = -2.754f;
	public float deathPositionY = 0.047f;
	float totalTimeElapsed = 0;
	float score = 0f;                // Total score
	int middle = 0;

	public bool isGameOver = false;
	public Text guiText;

	// Use this for initialization
	void Start () {
		//guiText = GameObject.Find("Score").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
			
		// Calculate the score based on total time elapsed
		totalTimeElapsed += Time.deltaTime;
		score = totalTimeElapsed * 10; 

		if((!isGameOver && player.position.x <= deathPositionX) || !isGameOver && player.position.y <= deathPositionY)
		{
			isGameOver = true;
		}
	}

	void OnGUI()
	{
		GUI.skin = skin;
		skin.label.fontSize = 65;

		// Check if game is not over, if so, display the score and the time left
		if (!isGameOver)
		{
			guiText.text = "Score:" + ((int)score).ToString();
			//GUI.Label(new Rect(Screen.width / 2, 70, Screen.width / 6, Screen.height / 6), ((int)score).ToString());
		}
		
		// If game over, display game over menu with score
		else
		{
			skin.label.fontSize = 55;
			Time.timeScale = 0;
			gameOverScreen.gameObject.SetActive(true);
			GUI.Label(new Rect(270, 85, 500, 500), "Game Over");
			GUI.Label(new Rect(300, 145, 500, 500), "Score: " + ((int)score).ToString());
		}
	}
}
