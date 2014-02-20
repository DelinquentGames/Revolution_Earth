using UnityEngine;
using System.Collections;

public class ShadowDistanceScript : MonoBehaviour {

	public UISlider ShadowDistance;
	
	// Update is called once per frame
	void Update () {
		QualitySettings.shadowDistance = ShadowDistance.value * 100;
	}
}
