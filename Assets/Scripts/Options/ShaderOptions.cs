using UnityEngine;
using System.Collections;

public class ShaderOptions : MonoBehaviour {

	public UIToggle AnsiotropicFiltering;
	public UIToggle AmbientParticles;
	public UIToggle ColorCorrection;
	public UIToggle BloomAndFlares;
	public UIToggle SunShafts;
	public UIToggle DepthOfField;
	public UIToggle SSAO;

	private GameObject camera;
		
	// Update is called once per frame
	void Update () {
		AnsiotropicFiltering.value = GameQualitySettings.anisotropicFiltering;
		AmbientParticles.value = GameQualitySettings.ambientParticles;
		ColorCorrection.value = GameQualitySettings.colorCorrection;
		BloomAndFlares.value = GameQualitySettings.bloomAndFlares;
		SunShafts.value = GameQualitySettings.sunShafts;
		DepthOfField.value = GameQualitySettings.depthOfField;
		SSAO.value = GameQualitySettings.ssao;
	}
}
