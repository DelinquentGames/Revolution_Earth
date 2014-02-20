using UnityEngine;
using System.Collections;

public class AnisotropicFilteringScript : MonoBehaviour {

	public UIToggle Checkbox;

	void OnClick(){
		if(Checkbox.value == true)
			GameQualitySettings.anisotropicFiltering = true;
		else
			GameQualitySettings.anisotropicFiltering = false;

	}
}
