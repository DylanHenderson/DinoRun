using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public float originalSpeed = 5;
	public float rotate_speed = 5;
	public float time_to_increase = 10;
	public float speed_increase = 1;
	public bool rotating = true;

	private float previous_speed =5;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("increaseSpeed",time_to_increase,time_to_increase);
		Invoke ("setOriginal", 5);
	}
	
	// Update is called once per frame
	void Update () {

		if (rotating) {
			transform.Rotate (Vector3.forward * Time.deltaTime * rotate_speed, Space.Self);
			previous_speed = rotate_speed;
		}

	}

	void increaseSpeed(){
		rotate_speed += speed_increase;
	}

	void setOriginal(){
		if (rotate_speed > previous_speed) {
			originalSpeed = rotate_speed;
		}

	}
}
