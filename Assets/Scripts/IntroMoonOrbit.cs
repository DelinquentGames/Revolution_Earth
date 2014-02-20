using UnityEngine;
using System.Collections;

public class IntroMoonOrbit : MonoBehaviour {
	private Transform target;
	
	// Use this for initialization
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag("Earth");
		target = go.transform;	
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Rotate(Vector3.down * Time.deltaTime * 0.05f);
		transform.RotateAround (target.transform.localPosition, Vector3.down, 0.2f * Time.deltaTime);
	}
}
