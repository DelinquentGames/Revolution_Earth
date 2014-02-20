using UnityEngine;
using System.Collections;

public class AmbientParticlesScript : MonoBehaviour {

	public UIToggle Checkbox;

	void OnClick(){
		if(Checkbox.value == true)
			GameQualitySettings.ambientParticles = true;
		else
			GameQualitySettings.ambientParticles = false;
		
	}
}
