using UnityEngine;
using System.Collections;

public class IntroRotate : MonoBehaviour {    

	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.down * Time.deltaTime * .5f);	
	}
}
