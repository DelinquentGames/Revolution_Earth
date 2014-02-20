using UnityEngine; 
using System.Collections;
 
public class RadarCamera : MonoBehaviour
 
{
 
    //reference to the main character
    public Transform player;
 
    //reference to an indicator graphic that shows the player's field of view
    public Transform RadarMap;
 
    private Quaternion forwardDirection;
 
    // Update will position camera along same 2d plane as main player
    //this will allow the camera to follow the player exactly
    void Update()
 
    {
 
        //get reference to current player.
        forwardDirection = player.transform.rotation;
 
        this.transform.position = new Vector3(player.position.x, this.transform.position.y, player.position.z);
 
        //only need to transform in one direction
        RadarMap.transform.rotation = new Quaternion(forwardDirection.x, forwardDirection.y, forwardDirection.z, forwardDirection.w);
 
    }
 
}