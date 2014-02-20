/* Description: This holds variables that are used by the AI to determine how they should behave
 * Author: Adam T. Davis
 * Modified by: Robbie Carrington Jr.
 */
using UnityEngine;
using System.Collections;

public class EnemySpawnPoint : MonoBehaviour{

	/**
	public GameObject[] patrolPoints;
	public float perceptionRadius = 5;
    public float baseActivationRange = 2;
	public bool isRanged = true; // indicates that this AI object is a ranged enemy - RobbieC
	public float rangedAttackRange = 100.0f; // determines how far to search for a collider using raycast - RobbieC
	public bool isStaticPatrol = false; // determines if the AI object should follow the patrol route from 1 to the end or go to random points - RobbieC
	**/
	public GameObject enemyPrefab; // Holds the enemy to spawn - RobbieC
	public Vector3 FacingDirection;
	
	public void OnDrawGizmos(){
		Gizmos.DrawIcon (transform.position, "enemy_spawn_point");
	}

}
