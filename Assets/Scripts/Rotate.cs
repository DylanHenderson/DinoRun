using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public float originalSpeed = 5;
	public float rotate_speed = 5;
	public float time_to_increase = 10;
	public float speed_increase = 1;
	public bool rotating = true;
	public bool doom = false;
	public GameObject planet;

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

		if (!doom) {
			rotate_speed += speed_increase;
		} else {
			rotate_speed = (planet.GetComponent<Rotate>().originalSpeed - planet.GetComponent<Rotate>().rotate_speed) * 0.7f;

			if(rotate_speed > 0)
			{
				rotate_speed *= -1;
			}
		}
	}

	void setOriginal(){
		if (!doom && rotate_speed > previous_speed) {
			originalSpeed = rotate_speed;
		} else if (doom && rotate_speed < previous_speed) {
			originalSpeed = rotate_speed;
		}

	}
}
