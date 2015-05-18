using UnityEngine;
using System.Collections;

public class FireballSpawner : MonoBehaviour {
	public GameObject fireball;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("spawnFireball", 1,10);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void spawnFireball(){
		GameObject swoosh = Instantiate(fireball,fireball.transform.position,Quaternion.identity) as GameObject;
		//Destroy (swoosh, 1);
	}
}
