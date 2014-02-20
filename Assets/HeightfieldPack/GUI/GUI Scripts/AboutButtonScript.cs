using UnityEngine;
using System.Collections;

public class AboutButtonScript : MonoBehaviour {

	public UIPanel characterPanel;
	public UIPanel equipmentPanel;
	public UIPanel inventoryPanel;
	public UIPanel optionsPanel;
	public UIPanel aboutPanel;

	void OnClick() {

		// display about panel
		characterPanel.gameObject.SetActive(false);
		equipmentPanel.gameObject.SetActive(false);
		inventoryPanel.gameObject.SetActive(false);
		optionsPanel.gameObject.SetActive(false);
		aboutPanel.gameObject.SetActive(true);
	}
}
