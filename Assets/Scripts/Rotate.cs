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
	public GameObject iceDoomWave;
	public Vector3 originalCameraPosition;

	private float previous_speed = 5;

	// Use this for initialization
	void Start () {
		
		originalCameraPosition = Camera.main.transform.position;
		InvokeRepeating ("increaseSpeed",time_to_increase,time_to_increase);
		Invoke ("setOriginal", 5);
	}
	
	// Update is called once per frame
	void Update () {

		if (rotating && !doom) {
			transform.Rotate (Vector3.forward * Time.deltaTime * rotate_speed, Space.Self);
			previous_speed = rotate_speed;
		} else if(rotating && doom) {
			transform.Rotate (Vector3.forward * rotate_speed, Space.Self);
			previous_speed = rotate_speed;
		}

		// The faster the doom the faster the screenshake
		if(doom && rotate_speed < 0.05f)
		{
			float quakeAmt = Random.value * rotate_speed * 2 - rotate_speed;
			Vector3 pp =  Camera.main.transform.position;
			pp.x += quakeAmt/2; 
			pp.y += quakeAmt/2; 
			Camera.main.transform.position = pp;

		}else if(doom && rotate_speed > 0.05f)
		{
			Camera.main.transform.position  = originalCameraPosition;
		}
	}

	public void setIceSpawning()
	{
		iceDoomWave.GetComponent<Rotate> ().originalSpeed = originalSpeed;
		iceDoomWave.GetComponent<Rotate> ().rotate_speed = rotate_speed;

		iceDoomWave.SetActive (true);
		gameObject.SetActive (false);
	}

	void increaseSpeed(){

		if (!doom) {
			rotate_speed += speed_increase;
		}
	}

	void setOriginal(){
		if (!doom && rotate_speed > previous_speed) {
			originalSpeed = rotate_speed;
		} 
	}
}
