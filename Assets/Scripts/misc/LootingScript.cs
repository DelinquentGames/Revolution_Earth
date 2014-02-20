using UnityEngine;
using System.Collections;

public class LootingScript : MonoBehaviour {

	public static ChestEffectsHandler Chest;
	private PlayerData _playerData;

	// Use this for initialization
	void Start () {

		if (Chest == null)
			return;

		for (int cnt = 0; cnt < Chest.Loot.Count; cnt++)
		{
//			if (GUI.Button(new Rect(30 + (buttonWidth * cnt), 5, buttonWidth, buttonHeight), new GUIContent(Chest.Loot[cnt].Icon, Chest.Loot[cnt].ToolTip()), "Inventory Slot Common"))
//			{
//				PlayerData.Instance.Inventory.Add(Chest.Loot[cnt]);
//				Chest.Loot.RemoveAt(cnt);
//				_toolTip = "";
//			}
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
