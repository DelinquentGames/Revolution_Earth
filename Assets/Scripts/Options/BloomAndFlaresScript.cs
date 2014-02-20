using UnityEngine;
using System.Collections;

public class BloomAndFlaresScript : MonoBehaviour {

	public UIToggle Checkbox;
	
	void OnClick(){
		if(Checkbox.value == true)
			GameQualitySettings.bloomAndFlares = true;
		else
			GameQualitySettings.bloomAndFlares = false;
		
	}
}
