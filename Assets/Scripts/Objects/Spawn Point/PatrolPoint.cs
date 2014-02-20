/* Description: This script displays an icon for the patrol points
 * Author: Robbie Carrington Jr.
 */

using UnityEngine;
using System.Collections;

public class PatrolPoint : MonoBehaviour {

    public void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "enemy_patrol_point");
    }
}
