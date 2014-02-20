using UnityEngine;
using System.Collections;

public class ObjectSpin : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, 0, Time.deltaTime * 100f);

//		for(int cnt = 0; cnt < 20; cnt++){
//			transform.Rotate(0, 0, Time.deltaTime * 100f);
//			transform.position = new Vector3(transform.position.x, transform.position.y - cnt, transform.position.z);
//		}
//
//		for(int cnt = 0; cnt < 20; cnt++){
//			transform.Rotate(0, 0, Time.deltaTime * 100f);
//			transform.position = new Vector3(transform.position.x, transform.position.y + cnt, transform.position.z);
//		}
	}
}
