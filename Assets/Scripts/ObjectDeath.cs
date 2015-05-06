using UnityEngine;
using System.Collections;

public class ObjectDeath : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < -5) {
			Destroy (gameObject);
		}
	}
}
