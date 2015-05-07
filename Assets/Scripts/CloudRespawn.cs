using UnityEngine;
using System.Collections;

public class CloudRespawn : MonoBehaviour {
	public float death_point;
	public float respawn_point;

	private float original_height;
	// Use this for initialization
	void Start () {
		original_height = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < death_point) {
			transform.position = new Vector3(respawn_point,original_height,transform.position.z);
			transform.rotation = Quaternion.identity;
		}
	}
}
