using UnityEngine;


public class CameraFacingBillboard : MonoBehaviour {

    private Transform target;
	
    void Start(){
        //GameObject go = GameObject.FindGameObjectWithTag("Sun");
        //target = Camera.main.transform;	
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Vector3.down * Time.deltaTime * 0.15f);
        //transform.RotateAround(target.position, Vector3.down, .05f * Time.deltaTime);
    }
}