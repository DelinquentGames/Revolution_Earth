using UnityEngine;
using System.Collections;

public class TerrainGrassDistanceInitialization : MonoBehaviour {

	public UISlider Slider;
	private SceneSettings sceneConf;
	
	// Update is called once per frame
	void Update () {
		//NGUIDebug.Log(GameQualitySettings.sceneConf.detailObjectDensity.ToString());
		Slider.value =  sceneConf.detailObjectDistance; 
	}
}
