/// <summary>
/// EnemyGenerator.cs
/// July 24, 2013
/// Adam T. Davis
/// 
/// This class is responsible for making sure that there is an enemy for each spawn point.
/// Create an empty GameObject called Game Master and attach this script.
/// 
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Revolution Earth/Managers/Enemy Generator")]
public class EnemyGenerator : MonoBehaviour {
	public enum State {
		Idle,
		Initialize,
		Setup,
		SpawnEnemy
	}
    //public GameObject[] enemyPrefabs;           //an array to hold all of the prefabs of enemies we want to spawn. - uncommented by RobbieC
    public GameObject[] spawnPoints;            //this array will hold a reference to all the spawn points in the scene.

	private State _state;                       //this is the local variable that holds the current state

    //called before the scripts run. Use this to make sure all references and variables are set.
	void Awake(){
		_state = State.Initialize;
	}

	// Use this for initialization
	IEnumerator Start () {
		while (true) {
			switch (_state) {
			case State.Initialize:
				Initialize ();
				break;
			case State.Setup:
				Setup ();
				break;
			case State.SpawnEnemy:
				SpawnEnemy ();
				break;
			}
			yield return 0;
		}
	}

	//make sure that everything is initialized before we go on to the next step
	private void Initialize() {
		/** Commented out by RobbieC
		if (!CheckForEnemyPrefabs()) {
			return;
		}
		**/

		if (!CheckForSpawnPoints()) {
			return;
		}

		_state = State.Setup;
	}

	//make sure that everything is set up before we continue
	private void Setup() {
		_state = State.SpawnEnemy;
	}

	//spawn an enemy if we have an open spawn point
	private void SpawnEnemy() {
		GameObject[] gos = AvailableSpawnPoints();

		/** Added the ability to use the enemy prefab off of the spawn point instead of using it from this script
		 * Uses an if statement to ensure the enemy prefab is set
		 * Modified by RobbieC
		**/
		for (int cnt = 0; cnt < gos.Length; cnt++) 
		{
			if (gos[cnt].GetComponent<EnemySpawnPoint>().enemyPrefab == null)
			{
				Debug.LogError("You are missing the enemy prefab on the spawn point at element " + cnt.ToString() + " in your game manager object!");
			}
			else
			{
				GameObject go = Instantiate(gos[cnt].GetComponent<EnemySpawnPoint>().enemyPrefab,
				                            gos[cnt].transform.position,
				                            Quaternion.identity
				                            ) as GameObject;

				go.transform.parent = gos[cnt].transform;
				/** Set all children above spawn point so they are above ground? - RobbieC
				if (go.transform.GetChild(0).name == "_ProjectileOrigin")
				{
					go.transform.GetChild(1) = gos[cnt].transform;
				}
				else
				{
					go.transform.GetChild(0) = gos[cnt].transform;
				}
				**/

				//go.transform.position = new Vector3(gos[cnt].transform.position.x, gos[cnt].transform.position.y, gos[cnt].transform.position.z); // added to move the AI object to the spawn point - RobbieC
				go.transform.Rotate(go.transform.parent.GetComponent<EnemySpawnPoint>().FacingDirection.x, go.transform.parent.GetComponent<EnemySpawnPoint>().FacingDirection.y, go.transform.parent.GetComponent<EnemySpawnPoint>().FacingDirection.z);
				//go.transform.Rotate(0, 180, 0);
			}
		}
		_state = State.Idle;
	}

	/** Commented out by RobbieC
	 * check to see that we have at least one enemy prefab to spawn
	private bool CheckForEnemyPrefabs(){
		if (enemyPrefabs.Length > 0)
			return true;
		else
			return false;
	}
	**/

	//check to see if we have at least one spawn point to spawn the enemies
	private bool CheckForSpawnPoints(){
		if (spawnPoints.Length > 0)
			return true;
		else
			return false;
	}

	//generate a list of available spawn points that do not have any enemies childed to them
	private GameObject[] AvailableSpawnPoints(){
		List<GameObject> gos = new List<GameObject> ();

		for (int cnt = 0; cnt < spawnPoints.Length; cnt++) {
			if (spawnPoints [cnt].transform.childCount == 0) {
				gos.Add (spawnPoints [cnt]);
			}
		}
		return gos.ToArray ();
	}

}
