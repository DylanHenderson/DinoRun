using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public float originalSpeed = 5;
	public float rotate_speed = 5;
	public float time_to_increase = 10;
	public float speed_increase = 1;
	public bool rotating = true;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("increaseSpeed",time_to_increase,time_to_increase);
	}
	
	// Update is called once per frame
	void Update () {

		if (rotating) {
			transform.Rotate (Vector3.forward * Time.deltaTime * rotate_speed, Space.Self);
		}

	}

	void increaseSpeed(){
		rotate_speed += speed_increase;
	}
}
