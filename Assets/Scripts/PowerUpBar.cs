using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUpBar : MonoBehaviour {

	Image myImage;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("increasePower",0.5f,0.5f);
		myImage = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void increasePower(){


		if (myImage.fillAmount >= 360) {
			//ready for powerup
		} else {
			myImage.fillAmount += 0.01f;
		}

	}
}
