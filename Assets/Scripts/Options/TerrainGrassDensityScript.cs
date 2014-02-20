using UnityEngine;
using System.Collections;

public class TerrainGrassDensityScript : MonoBehaviour {

	public UISlider TerrainGrassDensity;
	private SceneSettings sceneConf;

	// Update is called once per frame
	void Update () {
		//NGUIDebug.Log(GameQualitySettings.sceneConf.detailObjectDensity.ToString());
		//sceneConf.detailObjectDensity = TerrainGrassDensity.value;
	}
}