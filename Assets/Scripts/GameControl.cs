using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

	public GameObject touchcontrols;
	public GameObject gameOverScreen;
	public GameObject powerUpBar;
	public GameObject world;
	public Transform player;
	public GameObject doom;
	public GUISkin skin;
	public float deathPositionX = -2.754f;
	public float deathPositionY = 0.047f;
	float totalTimeElapsed = 0;
	float score = 0f;                // Total score
	int middle = 0;
	public Sprite tricera;
	public Sprite triceraJump;
	public RuntimeAnimatorController triceraController;
	Countdown gameCountdown;
	public GameObject embers;
	public GameObject snow;


	public bool isGameOver = false;
	public Text guiText;
	public Text gameOverText;

	// Use this for initialization
	void Start () {

		gameCountdown = transform.GetComponent<Countdown> ();

		if(PlayerPrefs.GetString("Dino").Equals("Triceratops"))
		{
			player.GetComponent<SpriteRenderer>().sprite = tricera;
			player.GetComponent<Animator>().runtimeAnimatorController = triceraController;
			player.GetComponent<PlayerController>().dinoType = PlayerController.DinoType.Triceratops;
			player.GetComponent<PlayerController>().defaultSprite = tricera;
			player.GetComponent<PlayerController>().jump = triceraJump;

			// Set powerbar values
			powerUpBar.GetComponent<PowerUpBar>().fillAmount = 0.003f;			// 30% of raptor fillAmount
			powerUpBar.GetComponent<PowerUpBar>().decreaseAmount = 0.002f;		// 40% of raptor decreaseAmount

			// Set worldspeed values
			world.GetComponent<Rotate>().speed_increase = 0.5f;					// Half the raptors speed increase
				
			Vector2 offset = new Vector2(0.07017317f, -0.151162f);
			Vector2 size = new Vector2(0.4421697f, 0.3538122f);
			player.GetComponent<BoxCollider2D>().offset = offset;
			player.GetComponent<BoxCollider2D>().size = size;
		}

		gameCountdown.startCountdown ();
	}
	
	// Update is called once per frame
	void Update () {
			
		// Calculate the score based on total time elapsed
		totalTimeElapsed += Time.deltaTime;

		score = totalTimeElapsed * 10; 

		// Change over to ice world
		if(score >= 5)
		{
			embers.SetActive(false);
			snow.SetActive(true);
			world.GetComponent<FireballSpawner>().setIceSpawning();
			world.GetComponent<spawnObstacles>().setIceSpawning();
			doom.GetComponent<Rotate>().setIceSpawning();
		}

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
			guiText.text = "Score:" + ((int)score).ToString();
			gameOverText.text =  "Game Over\n  Score:" + ((int)score).ToString();
			gameOverScreen.gameObject.SetActive(true);
		}
	}
}
