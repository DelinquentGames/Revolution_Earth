using UnityEngine;
using System.Collections;

public class DynamicObjectsFarDistanceScript : MonoBehaviour {

	public UISlider DynamicObjectsFarDistance;
		
	// Update is called once per frame
	void Update () {
		GameQualitySettings.dynamicObjectsFarClip = DynamicObjectsFarDistance.value;
	}

	void OnValueChange(){
		Debug.Log("This Works!");
	}
}
