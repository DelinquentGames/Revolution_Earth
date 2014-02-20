using UnityEngine;
using System.Collections;

public class CharacterButtonScript : MonoBehaviour {

	public UIPanel characterPanel;
	public UIPanel equipmentPanel;
	public UIPanel inventoryPanel;
	public UIPanel optionsPanel;
	public UIPanel aboutPanel;
	
	void OnClick() {

		// display character panel
		equipmentPanel.gameObject.SetActive(false);
		inventoryPanel.gameObject.SetActive(false);
		optionsPanel.gameObject.SetActive(false);
		aboutPanel.gameObject.SetActive(false);
		characterPanel.gameObject.SetActive(true);		
	}
}
