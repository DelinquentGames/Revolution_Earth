using UnityEngine;
using System.Collections;

public class OptionsButtonScript : MonoBehaviour {

	public UIPanel characterPanel;
	public UIPanel equipmentPanel;
	public UIPanel inventoryPanel;
	public UIPanel optionsPanel;
	public UIPanel aboutPanel;
	
	void OnClick() {

		// display options panel
		characterPanel.gameObject.SetActive(false);
		equipmentPanel.gameObject.SetActive(false);
		inventoryPanel.gameObject.SetActive(false);
		aboutPanel.gameObject.SetActive(false);
		optionsPanel.gameObject.SetActive(true);
	}
}
