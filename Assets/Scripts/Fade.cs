using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {

	public Texture2D fadeTexture;
	public float fadeSpeed = 0.2f;
	public int drawDepth = -1000;
	
	public float alpha = 1.0f; 
	private float fadeDir = -1.0f;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
	}
	
	void OnGUI(){ 

		if (alpha > 0f) {

			 	alpha += fadeDir * fadeSpeed * Time.deltaTime ;
				alpha =  Mathf.Clamp01 (alpha);

				// GUI.color = new Color() { a = 0.5f }; 
				GUI.color = new Color (){a = alpha};
				GUI.depth = drawDepth; 
				GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeTexture); 
			} 
	}
}
