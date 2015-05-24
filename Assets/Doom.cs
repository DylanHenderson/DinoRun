using UnityEngine;
using System.Collections;

public class Doom : MonoBehaviour {

	public GameObject player;
	public GameObject world;

	bool freeze = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	

		foreach(Transform child in world.transform)
		{	
			if(child.name == "Ice Water(Clone)")
			{

				float childRotation = child.rotation.z;
				float doomRotation = transform.rotation.z;
				float result = 0f;

				if(childRotation < 0)
				{
					childRotation*=-1;
				}
				
				if(doomRotation < 0)
				{
					doomRotation*=-1;
				}

				result = doomRotation - childRotation;

				if(result < 0)
				{
					result *=-1;
				}

				if(result < 0.0045f)
				{
					freeze = true;
				}else{
					freeze = false;
				}
			}
		}

		if(player.GetComponent<PlayerController>().onIce && freeze  && transform.rotation.x >= -0.00410922f)
		{
			player.GetComponent<PlayerController>().animator.SetFloat ("Speed", 0f);
			player.GetComponent<PlayerController>().animator.enabled = false;
			
			// Activate the ice block
			foreach(Transform child in player.transform)
			{
				child.gameObject.SetActive(true);
			}
			
			player.GetComponent<PlayerController>().planet.GetComponent<Rotate>().rotating = false;
			//planet.GetComponent<Rotate>().originalSpeed = planet.GetComponent<Rotate>().rotate_speed;
			
			player.GetComponent<PlayerController>().movingBackround1.GetComponent<MoveBackground>().moving = false;
			player.GetComponent<PlayerController>().movingBackround1.GetComponent<MoveBackground>().originalSpeed = player.GetComponent<PlayerController>().movingBackround1.GetComponent<MoveBackground>().move_speed;
			
			player.GetComponent<PlayerController>().movingBackround2.GetComponent<MoveBackground>().moving = false;
			player.GetComponent<PlayerController>().movingBackround2.GetComponent<MoveBackground>().originalSpeed = player.GetComponent<PlayerController>().movingBackround2.GetComponent<MoveBackground>().move_speed;

			player.GetComponent<PlayerController> ().animator.SetFloat ("Speed", 0f);
			player.GetComponent<PlayerController> ().CancelInvoke ("resetSpeed");
			
			player.GetComponent<PlayerController> ().doom.GetComponent<Rotate> ().rotate_speed -= 0.0005f;
		}
	}
}
