using UnityEngine;
using System.Collections;

public class spawnObstacles : MonoBehaviour {

	public GameObject rock_prefab;
	public Transform world;

	public float x_spawn_position = 3.205652f;
	public float y_spawn_position = 0.67f;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("spawnRock",5,10);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void spawnRock(){
		GameObject rock = Instantiate (rock_prefab) as GameObject;
		rock.transform.parent = world;
		//the position relative to the block that it should spawn for correct angle
		rock.transform.position = new Vector2 (x_spawn_position,y_spawn_position);

	}
}
