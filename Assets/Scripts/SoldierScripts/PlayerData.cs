/// <summary>
/// PlayerData.cs
/// Adam T. Davis
/// 10/12/2013
/// 
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData : BaseCharacter {
	
	private List<Item> inventory = new List<Item>();
	public List<Item> Inventory {
		get{ return inventory; }
		set{ inventory = value; }
	}
	private Item _equipedWeapon;
	
	private static PlayerData instance = null;	
	public static PlayerData Instance {
		get{
			if (instance == null){
				//Instantiating a new PlayerData
				GameObject go = Instantiate(Resources.Load(GameConstants.MALE_MODEL_PATH + "MainCharacter")) as GameObject;
				GameObject go2 = GameObject.Find("PlayerSpawnPoint");
				
				if(go2 == null)
					Debug.Log("Unable to find PlayerSpawnPoint!");
			
				
				PlayerData pd = go.GetComponent<PlayerData>();
				
				if(pd == null)
					Debug.LogError("Player Prefab does not contain a PlayerData script. Please add and configure.");
				
				instance = pd;	// locating PlayerData Script
				
				go.name = "PlayerData";	// renaming the character			
				go.tag = "Player";	// ensuring the new instantiated model is tagged Player
				
				go.transform.position = go2.transform.position;
				go.transform.Rotate(0, 180, 0);
				
				// Assigning the player to the GameManager
				GameManager.Instance.Player = go;
				
				//assigning cameras to PauseEffectCameras
				GameObject ca1 = GameObject.FindWithTag("MainCamera") as GameObject;
				
				if(ca1 == null)
					Debug.Log ("Unable to find Soldier Camera!");
				
				GameManager.Instance.PauseEffectCameras[0] = ca1.transform.camera;
				
				GameObject ca2 = GameObject.Find("Radar") as GameObject;								
				GameManager.Instance.PauseEffectCameras[1] = ca2.transform.Find("radar_camera").camera;
				
				// Assigning the SoldierSmoke particale effect to the GameManager
				GameObject ss = GameObject.Find("SoldierSmoke") as GameObject;	
				//GameManager.Instance.soldierSmoke = ss.transform.particleEmitter;
				
			}			
			
			return instance;
		}
	}
	
	#region Unity functions
	
	public new void Awake(){
		base.Awake();
		
		instance = this;
	}
	
	#endregion
	
	// preparing to equip the selected weapon to the proper mount
	public Item EquipedWeapon {
        get { return _equipedWeapon; }
        set {
			if (value == null)
                return;
			
            _equipedWeapon = value;           

			if(rightHandMount.transform.childCount > 0)
				Destroy(instance.rightHandMount.transform.GetChild(0).gameObject);
			
			GameObject mesh = Instantiate(Resources.Load(GameConstants.RANGED_WEAPON_MESH_PATH + _equipedWeapon.Name), rightHandMount.transform.position, rightHandMount.transform.rotation) as GameObject;
			mesh.transform.parent = rightHandMount.transform;
		}
	}
	
	// loading characters saved settings
	public void LoadCharacter() {
		LoadHair();
	}
	
	// loading the characters saved mesh and hair color
	public void LoadHair(){
		LoadHairMesh();
		LoadHairColor();
	}
	
	// loading the characters saved hair mesh
	public void LoadHairMesh(){
		
	}
	
	
	public void LoadHairColor(){
		
	}
	
	public void LoadHelmet(){
		
	}
	
	public void LoadShoulderArmor(){
		
	}
	
	public void LoadArmArmor(){
		
	}
	
	public void LoadTorsoArmor(){
		
	}
	
	public void LoadHandArmor(){
		
	}
	
	public void LoadLegsArmor(){
		
	}
	
	public void LoadFeetArmor(){
		
	}
	
	public void LoadEquipment(){
		
	}
}