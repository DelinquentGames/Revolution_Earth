using UnityEngine;
using System.Collections;

public class ShadowDistanceInitialization : MonoBehaviour {

	public UISlider Slider;
	
	// Update is called once per frame
	void Start () {
		Slider.value =  QualitySettings.shadowDistance / 100; 
	}
}
