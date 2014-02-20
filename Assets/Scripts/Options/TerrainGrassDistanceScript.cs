using UnityEngine;
using System.Collections;

public class TerrainGrassDistanceScript : MonoBehaviour {

	public UISlider TerrainGrassDistance;
	private SceneSettings sceneConf;
	
	// Update is called once per frame
	void Update () {
		//NGUIDebug.Log(GameQualitySettings.sceneConf.detailObjectDensity.ToString());
		//sceneConf.detailObjectDistance = TerrainGrassDistance.value;
	}
}
