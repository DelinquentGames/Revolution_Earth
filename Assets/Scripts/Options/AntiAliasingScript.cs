using UnityEngine;
using System.Collections;
using System.Globalization;

public class AntiAliasingScript : MonoBehaviour {

	public UILabel AAValueLabel;
	
	// Update is called once per frame
	void Update () {
		switch(AAValueLabel.text){
		case "Disabled":
			QualitySettings.antiAliasing = 0;
			break;
		case "2x Multi Sampling":
			QualitySettings.antiAliasing = 2;
			break;
		case "4x Multi Sampling":
			QualitySettings.antiAliasing = 4;
			break;
		case "8x Multi Sampling":
			QualitySettings.antiAliasing = 8;
			break;		
		}
	}
}
