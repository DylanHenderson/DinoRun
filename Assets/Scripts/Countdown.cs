using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Countdown : MonoBehaviour 
{
	
	public int countMax;                        // Max countdown number
	private int countDown;                      // Current countdown number
	public GameObject world; 
	public GameObject player;
	public GameObject guiTextCountdown;            // GUIText reference
	
	void Start()
	{
	}
	
	public void startCountdown()
	{
		// Call the CountdownFunction
		objectActivation (false);
		guiTextCountdown.SetActive(true);
		StartCoroutine(CountdownFunction());
	}

	GameObject[] FindGameObjectsWithLayer (int layer) {

		GameObject[] goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
		List<GameObject> goList = new List<GameObject>();

		for (int i = 0; i < goArray.Length; i++) {
			if (((GameObject)goArray[i]).layer == layer) {
				goList.Add(goArray[i]);
			}
		}

		if (goList.Count == 0) {
			return null;
		}

		return goList.ToArray();
	}

	void objectActivation(bool activate)
	{
		if (activate) {
			player.GetComponent<PlayerController> ().enabled = true;
			player.GetComponent<Animator> ().enabled = true;
			world.GetComponent<spawnObstacles> ().enabled = true;
			world.GetComponent<Rotate> ().enabled = true;
			gameObject.GetComponent<GameControl>().enabled = true;
		} else {
			player.GetComponent<PlayerController>().enabled = false;
			player.GetComponent<Animator>().enabled = false;
			world.GetComponent<spawnObstacles>().enabled = false;
			world.GetComponent<Rotate>().enabled = false;
			gameObject.GetComponent<GameControl>().enabled = false;
		}
	}

	IEnumerator CountdownFunction()
	{
		// Start the countdown
		for (countDown = countMax; countDown > -1; countDown--)
		{
			if (countDown != 0)
			{
				// Display the number to the screen via the GUIText
				guiTextCountdown.GetComponent<TextMesh>().text = countDown.ToString();

				// Add a one second delay
				yield return new WaitForSeconds(1);
			}
			else
			{
				guiTextCountdown.GetComponent<TextMesh>().text = "GO!";
				yield return new WaitForSeconds(1);
				objectActivation (true);
			}
		}
		
		// Disable the GUIText once the countdown is done with
		guiTextCountdown.SetActive(false);
	}
}
