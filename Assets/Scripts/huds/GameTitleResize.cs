/// <summary>
/// GameTitleResize.cs
/// 10/27/2013
/// Adam T. Davis
/// 
/// This script repositions the Title image to the center of 
/// the screen and gives it a 10 pixel padding
/// </summary>
using UnityEngine;
using System.Collections;

public class GameTitleResize : MonoBehaviour {
	
	public Texture Title1;
	
	void OnGUI(){
		// centering the title image and lowering it 10 from the top
		transform.GetComponent<GUITexture>().texture = Title1;
		transform.GetComponent<GUITexture>().pixelInset = new Rect( 0 - (Title1.width * 0.5f),								// x position
																	(((0 + Screen.height * 0.5f) - Title1.height)) - 10,	// y position
																	transform.GetComponent<GUITexture>().texture.width,		// width of the image
																	transform.GetComponent<GUITexture>().texture.height);	// height of the image
		transform.GetComponent<GUITexture>().enabled = true;																// enabling the image		
		
	}
}
