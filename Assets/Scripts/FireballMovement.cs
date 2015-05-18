using UnityEngine;
using System.Collections;

public class FireballMovement : MonoBehaviour {

	public Transform target_crash;
	public float speed = 10;
	// Use this for initialization
	void Start () {
		Destroy (gameObject,5);
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.Lerp(transform.position,target_crash.position,speed);
	}

}
