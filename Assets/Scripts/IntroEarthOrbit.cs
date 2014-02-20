using UnityEngine;
using System.Collections;

public class IntroEarthOrbit : MonoBehaviour {

    public AudioClip IntroMusic;
    private Transform target;

    void Awake()
    {
        audio.clip = IntroMusic;
        if (!audio.isPlaying)
            audio.Play();      
    }
	
	// Use this for initialization
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag("Sun");
		target = go.transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.down * Time.deltaTime * 0.15f);
		transform.RotateAround (target.position, Vector3.down, .05f * Time.deltaTime);	
	}
}
