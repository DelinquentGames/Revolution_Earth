using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {
	
	public Transform target;

    void Awake() {
        
    }
	
	// Update is called once per frame
	void Update () {

        if (target == null)
            target = Camera.main.gameObject.transform;

		transform.LookAt(target);
		transform.Rotate(0, 180,0);
	}
}
