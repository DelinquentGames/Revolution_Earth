using UnityEngine;
using System.Collections;

public class QualitySelection : MonoBehaviour {

	public UILabel QualityValueLabel;

	// Update is called once per frame
	void Update () {
		//NGUIDebug.Log(QualityValueLabel.text);
		switch(QualityValueLabel.text){
		case "QUALITY: FASTEST":
			GameQualitySettings.overallQuality = 0;
			break;
		case "QUALITY: FAST":
			GameQualitySettings.overallQuality = 1;
			break;
		case "QUALITY: SIMPLE":
			GameQualitySettings.overallQuality = 2;
			break;
		case "QUALITY: GOOD":
			GameQualitySettings.overallQuality = 3;
			break;
		case "QUALITY: BEAUTIFUL":
			GameQualitySettings.overallQuality = 4;
			break;
		case "QUALITY: FANTASTIC":
			GameQualitySettings.overallQuality = 5;
			break;
		}
	}
}
