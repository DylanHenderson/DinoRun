using UnityEngine;
using System.Collections;

public class FireballSpawner : MonoBehaviour {
	public GameObject fireball;
	public GameObject snowball;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("spawnFireball", 1,21);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	
	public void setIceSpawning()
	{
		fireball = snowball;	
	}

	void spawnFireball(){
		GameObject swoosh = Instantiate(fireball,fireball.transform.position,Quaternion.identity) as GameObject;
		//Destroy (swoosh, 1);
	}
}
