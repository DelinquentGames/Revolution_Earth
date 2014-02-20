/* Description: This script displays an icon for the patrol points
 * Author: Robbie Carrington Jr. & Adam Davis
 * TODO: Change icon drawn so that it doesn't look like a spawn point
 * Done
 */

using UnityEngine;
using System.Collections;

public class EnemyPatrolPoint : MonoBehaviour {
	
    public void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "enemy_patrol_point");
    }
}
