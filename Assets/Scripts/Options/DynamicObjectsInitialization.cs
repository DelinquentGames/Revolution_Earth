using UnityEngine;
using System.Collections;

public class DynamicObjectsInitialization : MonoBehaviour {

	public UISlider Slider;

	// Update is called once per frame
	void Start () {
		Slider.value = GameQualitySettings.dynamicObjectsFarClip; 
	}

	void OnValueChange(){
		Slider.value = GameQualitySettings.dynamicObjectsFarClip; 
	}
}
