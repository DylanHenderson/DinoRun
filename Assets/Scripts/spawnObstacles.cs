using UnityEngine;
using System.Collections;

public class spawnObstacles : MonoBehaviour {

	public GameObject rock_prefab;
	public GameObject tree_prefab;
	public GameObject pit;
	public Transform world;

	public float x_spawn_position = 3.205652f;
	public float y_spawn_position = 0.67f;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("chooseObstacles",6,6);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void chooseObstacles(){
		//types of obstacle sets
		//single rock
		//single tree
		//rock -> tree
		//tree -> rock
		//rock -> rock
		//tree -> tree
		//etc..
		float spawnChance; 
		float obstacleChoice;

		//choose an obstacle for position 1
		spawnChance = Random.Range (0, 2);

		if (spawnChance == 1) {
			obstacleChoice = Random.Range (0, 3);

			if (obstacleChoice == 0){
				Invoke("spawnRock",1);
			}else if(obstacleChoice == 1){
				Invoke("spawnRock",2);
			}else{
				Invoke("spawnPit",3);
			}

		}

		//choose an obstacle for position 2
		spawnChance = Random.Range (0, 3);
		
		if (spawnChance == 1) {
			obstacleChoice = Random.Range (0, 3);
			
			if (obstacleChoice == 1){
				Invoke("spawnRock",1);
			}else if  (obstacleChoice == 2){
				Invoke("spawnTree",2);
			}else{
				Invoke("spawnPit",3);
			}
			
		}

		//choose an obstacle for position 3
		spawnChance = Random.Range (0, 4);
		
		if (spawnChance == 1) {
			obstacleChoice = Random.Range (0, 3);
			
			if (obstacleChoice == 1){
				Invoke("spawnRock",1);
			}else if  (obstacleChoice == 2){
				Invoke("spawnTree",2);
			}else{
				Invoke("spawnPit",3);
			}
			
		}





	}

	void spawnRock(){
		GameObject rock = Instantiate (rock_prefab) as GameObject;
		rock.transform.parent = world;
		//the position relative to the block that it should spawn for correct angle
		rock.transform.position = new Vector2 (x_spawn_position,y_spawn_position);

	}

	void spawnTree(){
		GameObject tree = Instantiate (tree_prefab) as GameObject;
		tree.transform.parent = world;
		//the position relative to the block that it should spawn for correct angle
		tree.transform.position = new Vector2 (tree_prefab.transform.position.x,tree_prefab.transform.position.y);
		
	}

	void spawnPit(){
		GameObject pitObject = Instantiate (pit) as GameObject;
		pitObject.transform.parent = world;

		//the position relative to the block that it should spawn for correct angle
		pitObject.transform.position = new Vector2 (pit.transform.position.x,pit.transform.position.y);
		
	}
}
