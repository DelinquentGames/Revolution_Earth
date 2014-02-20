using UnityEngine;
using System.Collections;

public class FadeLight : MonoBehaviour {

	public float delay;
	public float fadeTime;
	
	private float fadeSpeed;
	private float intensity;
	private Color color;
	
	void Start()
	{
		if(light == null)
		{
			Destroy(this);
			return;
		}
		
		intensity = light.intensity;
		
		
		fadeTime = Mathf.Abs(fadeTime);
		
		if(fadeTime > 0.0)
		{
			fadeSpeed = intensity / fadeTime;
		}
		else
		{
			fadeSpeed = intensity;
		}
		//alpha = 1.0;
	}
	
	void Update()
	{
		if(delay > 0.0)
		{
			delay -= Time.deltaTime;
		}
		else if(intensity > 0.0)
		{
			intensity -= fadeSpeed * Time.deltaTime;
			light.intensity = intensity;
		}
	}
}