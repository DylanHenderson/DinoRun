using UnityEngine;
using System.Collections;

public class FireballMovement : MonoBehaviour {

	public Transform target_crash;
	public GameObject explosion;
	public float speed = 10;
	public GameObject spawner;
	// Use this for initialization
	void Start () {

		Destroy (gameObject,1);
		Invoke ("explode",0.9f);
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.Lerp(transform.position,target_crash.position,speed);

		
		
	}

	void explode(){
		GameObject swoosh = Instantiate(explosion,explosion.transform.position,Quaternion.identity) as GameObject;
		Destroy (swoosh, 4);

		spawnObstacles sn = spawner.GetComponent<spawnObstacles> ();
			sn.spawnFire();
		
	}
	
}
