using UnityEngine;
using System.Collections;

public class LootPanelScript : MonoBehaviour {

	public GameObject lootPanel;

	private string _toolTip = "";
	private PlayerData _playerData;

	// Use this for initialization
	void Start () {
		PlayerData.Instance.Awake();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnEnable(){		
		//Messenger.AddListener("CloseChest", ClearWindow);
		Messenger.AddListener("DisplayLoot", DisplayLoot);
	}

	private void DisplayLoot() {
		lootPanel.SetActive(true);
		//LootWindowOpen = true;
		MainCamera.DisableCameraRotate = true;
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
}
