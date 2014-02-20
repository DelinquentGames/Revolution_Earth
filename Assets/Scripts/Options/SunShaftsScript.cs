using UnityEngine;
using System.Collections;

public class SunShaftsScript : MonoBehaviour {

	public UIToggle Checkbox;
	
	void OnClick(){
		if(Checkbox.value == true)
			GameQualitySettings.sunShafts = true;
		else
			GameQualitySettings.sunShafts = false;
		
	}
}
