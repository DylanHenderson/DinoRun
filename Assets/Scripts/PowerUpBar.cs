using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUpBar : MonoBehaviour {

	Image myImage;
	// Use this for initialization

	private bool usePower = false;
	void Start () {
		InvokeRepeating ("increasePower",0.5f,0.5f);
		myImage = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void increasePower(){
		if (myImage.fillAmount >= 0.05) {
			usePower = true;
		} else {
			usePower = false;
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
		CancelInvoke("decreasePower");
		InvokeRepeating ("increasePower",0.5f,0.5f);

		
	}

	public bool canUsePower(){
		return usePower;

	}

	public void decreasePower(){
		if (myImage.fillAmount <= 0.01) {
			usePower = false;
			CancelInvoke("decreasePower");
			InvokeRepeating ("increasePower",0.5f,0.5f);
		} else {
			myImage.fillAmount -= 0.005f;
		}
		
	}
}
