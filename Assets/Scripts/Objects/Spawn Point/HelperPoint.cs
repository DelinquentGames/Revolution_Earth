/* Description: This script displays an icon for the helper points
 * Author: Robbie Carrington Jr.
 */

using UnityEngine;
using System.Collections;

public class HelperPoint : MonoBehaviour {

    public void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "enemy_helper_point");
    }
}
