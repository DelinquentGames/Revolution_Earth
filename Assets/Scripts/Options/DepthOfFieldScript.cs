using UnityEngine;
using System.Collections;

public class DepthOfFieldScript : MonoBehaviour {

	public UIToggle Checkbox;
	
	void OnClick(){
		if(Checkbox.value == true)
			GameQualitySettings.depthOfField = true;
		else
			GameQualitySettings.depthOfField = false;
		
	}
}
