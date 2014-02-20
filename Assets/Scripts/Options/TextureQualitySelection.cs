using UnityEngine;
using System.Collections;

public class TextureQualitySelection : MonoBehaviour {

	public UILabel TextureQualityValueLabel;
	
	// Update is called once per frame
	void Update () {
		switch(TextureQualityValueLabel.text){
		case "FULL RESOLUTION":
			GameQualitySettings.masterTextureLimit = 0;
			break;
		case "HALF RESOLUTION":
			GameQualitySettings.masterTextureLimit = 1;
			break;
		case "QUARTER RESOLUTION":
			GameQualitySettings.masterTextureLimit = 2;
			break;
		case "1/8 RESOLUTION":
			GameQualitySettings.masterTextureLimit = 3;
			break;		
		}
	}
}
