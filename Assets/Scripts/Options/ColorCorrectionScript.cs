using UnityEngine;
using System.Collections;

public class ColorCorrectionScript : MonoBehaviour {

	public UIToggle Checkbox;
	
	void OnClick(){
		if(Checkbox.value == true)
			GameQualitySettings.colorCorrection = true;
		else
			GameQualitySettings.colorCorrection = false;
		
	}
}
