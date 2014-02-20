using UnityEngine;
using System.Collections;

public class InventoryButtonScript : MonoBehaviour {

	public UIPanel characterPanel;
	public UIPanel equipmentPanel;
	public UIPanel inventoryPanel;
	public UIPanel optionsPanel;
	public UIPanel aboutPanel;

	
	void OnClick() {
		ActivatePanel();
	}

	public void ActivatePanel(){

		// display inventory panel
		characterPanel.gameObject.SetActive(false);
		equipmentPanel.gameObject.SetActive(false);
		optionsPanel.gameObject.SetActive(false);
		aboutPanel.gameObject.SetActive(false);
		inventoryPanel.gameObject.SetActive(true);
	}
}
