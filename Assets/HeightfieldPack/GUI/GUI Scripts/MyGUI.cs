/// <summary> 
/// MyGUI.cs 
/// June 24, 2013
/// Adam T. Davis
/// 
/// This class Displays the Character Window and tabs for Equipment,
/// Attributes, and Skills 
/// </summary>
///
using System; 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Revolution Earth/GUI/HUD")]
public class MyGUI : MonoBehaviour {
	public GUISkin mySkin;	
	public GUIStyle ButtonTextStyle;
    public string emptyInventorySlotStyle;              //the style name for an empty inventory slot
    public string closeButtonStyle;                     //the style for a close button
	public static float buttonWidth = 135;
	public static float buttonHeight = 99;
	public static bool LootWindowOpen {get; set;}
	private float native_width = 1600;
	private float native_height = 900;
        
	private static float _offset = 10;
	private string _toolTip = "";
    private PlayerData _playerData;

	/*************************/
	/* Loot Window Variables */
	/*************************/
	// size of the inventory slot graphic
    public static float lootWindowHeight = 135;	
    public float closeButtonWidth = 20;
    public float closeButtonHeight = 20;
	private bool _displayLootWindow = false;	
	private const int LOOT_WINDOW_ID = 0;
    private Rect _lootWindowRect = new Rect(Screen.width * 0.5f - ((135 * 3) + 100) * 0.5f, Screen.height * 0.5f - (lootWindowHeight + (_offset * 3) * 0.5f), (135 * 3) + 70, 99 + 5);    // 165 * 3 is the number of generated items times the inventory_slot graphic width / 
	private Vector2 _lootWindowSlider = Vector2.zero;
    public static ChestEffectsHandler Chest;
	
	/************************************/
	/* Character Sheet Window Variables */
	/************************************/
	[HideInInspector]
	static public bool DisplayCharacterSheetWindow = false;
	
	private bool _displayCharacterSheetWindow = false;	
	private static float characterWindowWidth = 633f;
	private static float characterWindowHeight = 812f;
	private const int CHARACTER_SHEET_WINDOW_ID = 2;
    private Rect _characterSheetWindowRect = new Rect(Screen.width * 0.5f - (characterWindowWidth * 0.5f), Screen.height * 0.5f - (characterWindowHeight * 0.5f), characterWindowWidth, characterWindowHeight);
    
	/******************************/
	/* Character Window Variables */
	/******************************/		
	private bool _displayCharacterWindow = false;	
	private const int CHARACTER_WINDOW_ID = 3;
    private Rect _characterWindowRect = new Rect(Screen.width * 0.5f - ((500 + _offset) * 0.5f), Screen.height * 0.5f - ((465 + _offset) * 0.5f), 500 + _offset, 465 + _offset);	
	private int _characterPanel = 0;
	private string[] _characterPanelNames = new string[] {"Attributes", "Skills", "Equipment"};
	
	void OnGUI(){
		//set up scaling
		float rx = Screen.width / native_width;
		float ry = Screen.height / native_height;
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3 (rx, ry, 1)); 
		GUI.skin = mySkin;

        if (_displayLootWindow) {            
            _lootWindowRect = GUI.Window(LOOT_WINDOW_ID, _lootWindowRect, LootWindow, "", "Inventory Window");
        }
		
		if(_displayCharacterSheetWindow){			
            _characterSheetWindowRect = GUI.Window(CHARACTER_SHEET_WINDOW_ID, _characterSheetWindowRect, CharacterSheetWindow, "", "Character Sheet");
		}
		
		if(_displayCharacterWindow)
			_characterWindowRect = GUI.Window(CHARACTER_WINDOW_ID,_characterWindowRect, CharacterWindow, "Character Stats");

        DisplayToolTip();
     
	}
	
	void Awake(){
       }
	
	// Use this for initialization
	void Start () {  
        PlayerData.Instance.Awake();
		LootWindowOpen = false;
	}
		
	private void OnEnable(){		
//        Messenger.AddListener("CloseChest", ClearWindow);
//        Messenger.AddListener("DisplayLoot", DisplayLoot);
		Messenger.AddListener("ToggleCharacterWindow", ToggleCharacterWindow);
		Messenger.AddListener("ToggleCharacterSheetWindow", ToggleCharacterSheetWindow);
	}

    private void OnDisable()
    {
        //Messenger.RemoveListener("CloseChest", ClearWindow);
        //Messenger.RemoveListener("DisplayLoot", DisplayLoot);
        //Messenger.RemoveListener("ToggleCharacterWindow", ToggleCharacterWindow);
        //Messenger.RemoveListener("ToggleCharacterSheetWindow", ToggleCharacterSheetWindow);
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void DisplayLoot() {
        _displayLootWindow = true;
		LootWindowOpen = true;
		MainCamera.DisableCameraRotate = true;
    }

    private void ClearWindow() {       
        _displayLootWindow = false;
		MainCamera.DisableCameraRotate = false;
		LootWindowOpen = false;
        Chest.ChestState();
        Chest = null;        
    }
	
	private void LootWindow(int id){

        if (GUI.Button(new Rect(_lootWindowRect.width - _offset * 3, 0, closeButtonWidth, closeButtonHeight), "X", "Close Button Style"))
            ClearWindow();

        if (Chest == null)
            return;
		
        if (Chest.Loot.Count == 0) {
            ClearWindow();
            return;
        }

        //_lootWindowSlider = GUI.BeginScrollView(new Rect(_offset, _lootWindowRect.height - 135, _lootWindowRect.width - (_offset * 2), 99), _lootWindowSlider, new Rect(0, 0, (Chest.Loot.Count * 135) + (_offset - 1), 99));

        for (int cnt = 0; cnt < Chest.Loot.Count; cnt++)
        {
            if (GUI.Button(new Rect(30 + (buttonWidth * cnt), 5, buttonWidth, buttonHeight), new GUIContent(Chest.Loot[cnt].Icon, Chest.Loot[cnt].ToolTip()), "Inventory Slot Common"))
            {
                PlayerData.Instance.Inventory.Add(Chest.Loot[cnt]);
                Chest.Loot.RemoveAt(cnt);
                _toolTip = "";
            }
        }

       //GUI.EndScrollView();
       SetToolTip();

       GUI.DragWindow();
	}
	
	private void CharacterSheetWindow(int id){
					
		if (GUI.Button(new Rect(_characterSheetWindowRect.width - 25, 35, closeButtonWidth, closeButtonHeight), "X", "Close Button Style"))
			Messenger.Broadcast("ToggleCharacterSheetWindow");		
		
		//display characters alias
		GUI.Label(new Rect( 75, 57, 180, 40 ), (char)(34) + GameConstants.LoadAlias() + (char)(34));		
		
		//display characters alias
		GUI.Label(new Rect( 115, 75, 180, 40 ), GameConstants.LoadFirstName());	
		
		//display characters alias
		GUI.Label(new Rect( 405, 75, 180, 40 ), GameConstants.LoadLastName());
		
		//display Agility
		GUI.Label(new Rect( 156, 144, 195, 40 ), GameConstants.LoadAttribute(AttributeName.Agility).BaseValue.ToString());
		
		//display Combat
		GUI.Label(new Rect(  156, 173, 195, 40 ), GameConstants.LoadAttribute(AttributeName.Combat).BaseValue.ToString());
		
		//display Body
		GUI.Label(new Rect(  156, 202, 195, 40 ), GameConstants.LoadAttribute(AttributeName.Body).BaseValue.ToString());
		
		//display Charisma
		GUI.Label(new Rect( 196, 157, 195, 40 ), GameConstants.LoadAttribute(AttributeName.Charisma).BaseValue.ToString());
		
		//display Intellect
		GUI.Label(new Rect(  196, 186, 195, 40 ), GameConstants.LoadAttribute(AttributeName.Intellect).BaseValue.ToString());
		
		//display Willpower
		GUI.Label(new Rect(  196, 215, 195, 40 ), GameConstants.LoadAttribute(AttributeName.WillPower).BaseValue.ToString());
		
		//display Reaction
		GUI.Label(new Rect(  385, 177, 195, 40 ), GameConstants.LoadAttribute(AttributeName.Reaction).BaseValue.ToString());
		
		//display Magic
		GUI.Label(new Rect(  441, 187, 195, 40 ), GameConstants.LoadAttribute(AttributeName.Magic).BaseValue.ToString());
		
		//display Essence
		GUI.Label(new Rect(  531, 165, 195, 40 ), GameConstants.LoadAttribute(AttributeName.Essence).BaseValue.ToString());
		
		
		GUI.DragWindow();		
	}
	
	private void CharacterWindow(int id){
		
		_characterPanel = GUI.Toolbar(new Rect(40, 40, _characterWindowRect.width - 80, 50),_characterPanel,_characterPanelNames, "Character Tabs");
        
		switch(_characterPanel){
		case 0:
			DisplayAttributes();
			break;
		case 1:
			DisplaySkills();
			break;
		case 2:
			DisplayEquipment();
			break;
		}
		
		GUI.DragWindow();
	}

    private void SetToolTip() {
        if (Event.current.type == EventType.Repaint && GUI.tooltip != _toolTip) {
            if (_toolTip != "")
                _toolTip = "";

            if (GUI.tooltip != "")
                _toolTip = GUI.tooltip;
        }
    }

    private void DisplayToolTip() {
        if(_toolTip != ""){			
            GUI.Box(new Rect(Event.current.mousePosition.x,Event.current.mousePosition.y, 200, 100), _toolTip, "ToolTip");
			GUI.depth = 0;
		}
    }

    private void DisplayEquipment()
    {
        if (PlayerData.Instance.EquipedWeapon == null) {
            GUI.Label(new Rect(140, 100, 40, 40), "RH", emptyInventorySlotStyle);
            GUI.Label(new Rect(40, 100, 40, 40), "LH", emptyInventorySlotStyle);
        }
        else
        {
            if (GUI.Button(new Rect(40, 100, 40, 40), new GUIContent(PlayerData.Instance.EquipedWeapon.Icon, PlayerData.Instance.EquipedWeapon.ToolTip()), "Inventory Slot Common")) {
                PlayerData.Instance.Inventory.Add(PlayerData.Instance.EquipedWeapon);
                PlayerData.Instance.EquipedWeapon = null;
            }
            GUI.Label(new Rect(140, 100, 40, 40), "RH", emptyInventorySlotStyle);
        }
        SetToolTip();
	}
	
	private void DisplayAttributes(){
        GUI.Label(new Rect(40, 100, 230, 40), " Physical");
        GUI.Label(new Rect(40, 110, 230, 40), "Attributes");
        for (int cnt = 0; cnt < 3; cnt++)
        {
            GUI.Label(new Rect(40,									//x
                               130 + (cnt * 10),                	//y
                               230,                             	//width
                               40   								//height
                ), ((AttributeName)cnt).ToString());

            GUI.Label(new Rect(140,                  				//x
                                130 + (cnt * 10),                 	//y
                                40,             					//width
                                40  								//height
                ), _playerData.GetPrimaryAttribute(cnt).AdjustedBaseValue.ToString());
        }        

        GUI.Label(new Rect(200, 100, 230, 40), "  Mental");
        GUI.Label(new Rect(200, 110, 230, 40), "Attributes");

        for (int cnt = 0; cnt < 3; cnt++)
        {
            GUI.Label(new Rect(200,									//x
                               130 + (cnt * 10),                	//y
                               230,                             	//width
                               40   								//height
                ), ((AttributeName)cnt + 3).ToString());

            GUI.Label(new Rect(300,                  				//x
                                130 + (cnt * 10),                 	//y
                                40,             					//width
                                40  								//height
                ), _playerData.GetPrimaryAttribute(cnt + 3).AdjustedBaseValue.ToString());
        }
        //GUI.Label(new Rect(200, 130, 230, 40), "Reasoning: ");
        //GUI.Label(new Rect(200, 140, 230, 40), "Mind:  ");
        //GUI.Label(new Rect(200, 150, 230, 40), "Intellect:  ");

        GUI.Label(new Rect(350, 100, 230, 40), " Mystical");
        GUI.Label(new Rect(350, 110, 230, 40), "Attributes");
        GUI.Label(new Rect(350, 130, 230, 40), "Presence: ");
        GUI.Label(new Rect(350, 140, 230, 40), "Magic:  ");
        GUI.Label(new Rect(350, 150, 230, 40), "Ego:  ");
	}
	
	private void DisplaySkills(){
		//Debug.Log ("Displaying Skills");
	}
	
	public void ToggleCharacterSheetWindow(){
		Screen.lockCursor = !Screen.lockCursor;
		DisplayCharacterSheetWindow = _displayCharacterSheetWindow;
		_displayCharacterSheetWindow = !_displayCharacterSheetWindow;
		DisplayCharacterSheetWindow = _displayCharacterSheetWindow;
	}

	public void ToggleCharacterWindow(){		
		_displayCharacterWindow = !_displayCharacterWindow;
	}   
}
