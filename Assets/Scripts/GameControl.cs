using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	public GameObject gameOverScreen;
	public Transform player;
	public GUISkin skin;
	public float deathPosition = -2.754f;
	float totalTimeElapsed = 0;
	float score = 0f;                // Total score
	int middle = 0;

	public bool isGameOver = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
			
		// Calculate the score based on total time elapsed
		totalTimeElapsed += Time.deltaTime;
		score = totalTimeElapsed * 10; 

		if(!isGameOver && player.position.x <= deathPosition)
		{
			isGameOver = true;
		}
	}

	void OnGUI()
	{
		GUI.skin = skin;
		skin.label.fontSize = 25;

		// Check if game is not over, if so, display the score and the time left
		if (!isGameOver)
		{
			if((int)score < 10)
			{
				middle = 745;
			}else if((int)score < 100)
			{
				middle = 735;
			}else if((int)score < 1000)
			{
				middle = 730;
			}else if ((int)score > 1000)
			{
				skin.label.fontSize = 15;
				middle = 725;
			}

			GUI.Label(new Rect(middle, 40, Screen.width / 6, Screen.height / 6), ((int)score).ToString());
		}
		
		// If game over, display game over menu with score
		else
		{
			skin.label.fontSize = 55;
			Time.timeScale = 0;
			gameOverScreen.gameObject.SetActive(true);
			GUI.Label(new Rect(300, 120, 500, 500), "Score: " + ((int)score).ToString());
		}
	}
}
