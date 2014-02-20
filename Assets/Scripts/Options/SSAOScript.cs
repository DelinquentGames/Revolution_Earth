using UnityEngine;
using System.Collections;

public class SSAOScript : MonoBehaviour {

	public UIToggle Checkbox;
	
	void OnClick(){
		if(Checkbox.value == true)
			GameQualitySettings.ssao = true;
		else
			GameQualitySettings.ssao = false;
		
	}
}
