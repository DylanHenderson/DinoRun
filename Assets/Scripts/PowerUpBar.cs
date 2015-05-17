using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUpBar : MonoBehaviour {

	Image myImage;
	// Use this for initialization

	private bool can_fly = false;
	void Start () {
		InvokeRepeating ("increasePower",0.5f,0.5f);
		myImage = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void increasePower(){
		if (myImage.fillAmount >= 0.05) {
			can_fly = true;
		} else {
			can_fly = false;
		}
		
		if (myImage.fillAmount >= 1) {
			//ready for powerup
		} 
		else {
			myImage.fillAmount += 0.01f;
		}

	}

	public void initiateDecrease(){
		InvokeRepeating ("decreasePower",0.05f,0.05f);
		CancelInvoke("increasePower");

	}

	public void cancelDecrease(){
		print ("caneling");
		CancelInvoke("decreasePower");
		InvokeRepeating ("increasePower",0.5f,0.5f);

		
	}

	public bool getcanFly(){
		return can_fly;

	}

	public void decreasePower(){
		if (myImage.fillAmount <= 0.01) {
			can_fly = false;
			CancelInvoke("decreasePower");
			InvokeRepeating ("increasePower",0.5f,0.5f);
		} else {
			myImage.fillAmount -= 0.01f;
		}
		
	}
}
