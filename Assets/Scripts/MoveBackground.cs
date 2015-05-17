using UnityEngine;
using System.Collections;

public class MoveBackground : MonoBehaviour {

	public float originalSpeed = 5;
	public float move_speed = 5;
	public float time_to_increase = 10;
	public float speed_increase = 1;
	public bool moving = true;

	//how much we must offset the respawn by, could probably use skybox length for this.
	public float offset;

	public Transform target;
	public Transform second_sky;

	private Vector3 spawn_position;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("increaseSpeed",time_to_increase,time_to_increase);

	}
	
	// Update is called once per frame
	void Update () {

		if (moving) {
			if (transform.position.x <= target.position.x) {
				spawn_position = new Vector3 (second_sky.position.x + offset, transform.position.y, transform.position.z);
				transform.position = spawn_position;
			}

			float step = move_speed / 10.0f * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, target.position, step);
		}
	}


	void increaseSpeed(){
		move_speed += speed_increase;
	}
}
