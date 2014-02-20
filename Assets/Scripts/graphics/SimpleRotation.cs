using UnityEngine;
using System.Collections;

public class SimpleRotation : MonoBehaviour {
    
    public DesiredAxis desiredAxis;
	public float visualSlowSpeed = 0.5f;
	public int framePerVisualRotation = 4;
	
	private Transform t;
	private Vector3 axis;
	
	void Start()
	{
		t = transform;
		
		axis = new Vector3(1, 0, 0);
		
		switch(desiredAxis)
		{
			case DesiredAxis.Y:
				axis = new Vector3(0, 1, 0);
				break;
			case DesiredAxis.Z:
				axis = new Vector3(0, 0, 1);
				break;
		}
	}
	
	void Update()
	{
//        if(GameManager.pause) return;
		
		t.Rotate(axis * (visualSlowSpeed * 360f * Time.deltaTime + 360f / framePerVisualRotation));
	}
}

public enum DesiredAxis
{
	X,
	Y,
	Z
}