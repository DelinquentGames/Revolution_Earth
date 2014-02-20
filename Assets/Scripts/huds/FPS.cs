using System;
using UnityEngine;

public class FPS : MonoBehaviour
{
	private float accum;
	private int frames;
	private float timeleft;
	public float updateInterval = 0.5F;
	
	
	private void Start()
	{
		if (!guiText)
		{
			enabled = false;
			return;
		}
		timeleft = updateInterval;
	}
	
	private void Update()
	{
		timeleft -= Time.deltaTime;
		accum += Time.timeScale/Time.deltaTime;
		++frames;
		
		if (timeleft <= 0.0)
		{
			float fps = accum/frames;
			string format = String.Format("{0:F2} FPS", fps);
			guiText.text = format;
			
			if (fps < 30)
				guiText.material.color = Color.yellow;
			else if (fps < 10)
				guiText.material.color = Color.red;
			else
				guiText.material.color = Color.green;
			timeleft = updateInterval;
			accum = 0.0F;
			frames = 0;
		}
	}
}