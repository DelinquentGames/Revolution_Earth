using UnityEngine;
using System.Collections;

public class FullScreenScript : MonoBehaviour {

	public UIToggle Checkbox;
	
	void OnClick(){
		if(Checkbox.value == true)
			Screen.fullScreen = true;
		else
			Screen.fullScreen = false;
		
	}
}
