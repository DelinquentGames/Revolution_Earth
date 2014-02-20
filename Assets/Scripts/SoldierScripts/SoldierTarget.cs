using UnityEngine;
using System.Collections;

public class SoldierTarget : MonoBehaviour {

	public Texture2D target;					// target reticule graphic
	public Texture2D targetOver;				// target reticule graphic is over an enemy target

	public static bool overEnemy;						// value showing if we are over and enemy or not
	private bool _overEnemy;					// private variable used for showing if over enemy or not
	
	private GUITexture gui;						// used to display the current Target reticule texture
	
	public bool aim;				
	private bool _aim;

	public static GameObject enemyTarget;
	private float currentDistance;
	
	public LayerMask enemyLayer;
	public LayerMask otherLayer;
	
	public float enemyDistance = 50.0f;
	
	public Camera mainCam;
	
	public Transform soldierTarget;				// used to store the soldierTarget object's transform
	public PlayerController playerController;	// this is the SoldierController script
	public MainCamera mainCamera;			// this is the SoldierCamera or main camera
	
	void Start(){
		
		// assigning main camera to soldier cam automatically
		GameObject go = GameObject.FindWithTag("MainCamera") as GameObject;
		
		// if it hasn't been done manually assign the soldier cam to soldierCam
		if (mainCam == null){
			mainCam = go.transform.camera;
			mainCamera = go.GetComponent<MainCamera>();
		}
		
		// getting the Player character
		GameObject pc = GameObject.FindWithTag("Player") as GameObject;
		
		// getting the SoldierController from the player character
		if (playerController == null)
			playerController = pc.GetComponent<PlayerController>();

		gui.color = new Color(0.5f, 0.5f, 0.5f, 0.75f);
		enemyLayer = 1<<8;
		
	}
	
	void OnEnable()
	{
		// unparenting the soldierTarget
		soldierTarget.parent = null;
		
		// assigning gui the guiTexture
		gui = guiTexture;
		
		// setting the gui's position using pixelInset
		gui.pixelInset = new Rect((-target.width * 0.5f), (-target.height * 0.5f), target.width, target.height);
		
		//RevolutionEarthDebug.Log(target.name.ToString());
		// assigning the gui's texture to be the targets texture
		gui.texture = target;
		
		// changing the color of the tecture
		gui.color = new Color(0.5f, 0.5f, 0.5f, 0.15f);
	}
	
	void Update()
	{	
		if(!mainCam.gameObject.activeSelf) 
		{
			// adjusting the Target reticules color to bright white.
			gui.color = new Color(0.5f, 0.5f, 0.5f, 0.0f);
			return;
		}
	
		aim = Input.GetButton("Fire2");

		Ray ray = mainCam.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f));
		
		RaycastHit hit1;
		RaycastHit hit2;
		
		overEnemy = Physics.Raycast(ray.origin, ray.direction,out hit1, enemyDistance, enemyLayer);
		
		if(overEnemy)
		{
			if(Physics.Raycast(ray.origin, ray.direction,out hit2, enemyDistance, otherLayer))
			{
				overEnemy = hit1.distance < hit2.distance;
				enemyTarget = hit1.collider.gameObject;
			}
		}
		else
			enemyTarget = null;
		
		if(overEnemy != _overEnemy)
		{
			_overEnemy = overEnemy;

//			if(overEnemy && !enemyTarget.GetComponent<AdvancedAiEnemy>().isDead)
//			{
//				Messenger.Broadcast<string>("set enemy name", enemyTarget.GetComponent<AdvancedAiEnemy>().generalParameters.botName);
//				Messenger.Broadcast<float>("enemy health update", enemyTarget.GetComponent<AdvancedAiEnemy>().generalParameters.botHealth);
//				Messenger.Broadcast<float>("enemy mana update", enemyTarget.GetComponent<AdvancedAiEnemy>().generalParameters.botMana);
//
//				gui.texture = targetOver;
//
//			}
//			else
//			{
//				enemyTarget = null;
//				Messenger.Broadcast<string>("set enemy name", "");
//				Messenger.Broadcast<float>("enemy health update", 0);
//				Messenger.Broadcast<float>("enemy mana update", 0);
//				gui.texture = target;
//			}
		}
	}
}