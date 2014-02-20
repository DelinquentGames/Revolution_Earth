using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject Player;
	public GameObject NGUIPanel;
	public static GameManager Instance;
	public VitalBarBasic EnemyHealthBar;
	public VitalBarBasic EnemyManaBar;
	public InventoryButtonScript inventory;

	static public bool receiveDamage;
	static public bool menuVisible;
	static public bool scores;
	static public float time;
	static public bool running;
	static public bool targetReticuleVisible;	

	public GameObject targetReticule;

    public Camera[] PauseEffectCameras = new Camera[2];
	public bool pause;
	private bool _paused;
	private bool wasLocked;
	
	void Awake(){
		Instance = this;
		running = false;
		EnemyHealthBar.gameObject.SetActive(false);
		EnemyManaBar.gameObject.SetActive(false);
	}

	void Start()
	{	
		Player = GameObject.FindWithTag("Player");
		Screen.lockCursor = true;
		Messenger.Broadcast<string>("set player name", GameConstants.LoadAlias());
		menuVisible = false;
		targetReticuleVisible = false;
		scores = false;
		pause = false;
		_paused = false;
		time = 0.00001f;
		StartGame();
	}
	
	void Update()
	{
		Messenger.Broadcast<float>("player health update", 100);
		Messenger.Broadcast<float>("player mana update", 100);
        
		if(!pause && running) time += Time.deltaTime;

		if(Input.GetKeyDown(KeyCode.Escape) && !pause){
			Screen.lockCursor = true;
			Messenger.Broadcast<string>("set enemy name", "");
		}
		
		if(Input.GetKeyDown(KeyCode.Escape) && !pause || Input.GetKeyDown(KeyCode.I)){

			pause = !pause;
			menuVisible = pause;
			Screen.lockCursor = !pause;
			Screen.showCursor = !pause;
			Player.GetComponent<CharacterCamera>().MouseEnabled = !pause;
			//Player.GetComponent<CharacterCtrl>().IsEnabled = !pause;

			NGUITools.SetActive(NGUIPanel, pause); 

			if(Input.GetKeyDown(KeyCode.I) && pause){
				inventory.ActivatePanel();

			}

			for(int i = 0; i < PauseEffectCameras.Length; i++)
			{
				Camera cam = PauseEffectCameras[i];
				if (cam == null) continue;
				if (cam.name != "radar_camera") continue;
				
				cam.enabled = pause;
			}  

			if(_paused != pause)
			{
				_paused = pause;				
				
				for(int i = 0; i < PauseEffectCameras.Length; i++)
				{
					Camera cam = PauseEffectCameras[i];
					if (cam == null) continue;
					if (cam.name != "radar_camera") continue;
					
					cam.enabled = !pause;
				}  
			}

            //disabling the targeting reticule for looting mode
			GameManager.Instance.targetReticule.GetComponent<GUITexture>().enabled = !pause;
			MainCamera.DisableCameraRotate = pause;
		}        
	}

	void StartGame()
	{
		running = true;

        if (Player != null)
        {
            if (!Player.activeSelf)
            {
                Player.SetActive(true);
            }
        }
	}
}
