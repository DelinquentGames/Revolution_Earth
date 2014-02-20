using UnityEngine;
using System.Collections;

public class ResumeButtonScript : MonoBehaviour {
	
	void OnClick() {

		GameObject mainMenu = GameObject.Find("MainMenu");
		GameObject Player = GameObject.FindWithTag("Player");
		NGUITools.SetActive(mainMenu, false); 

		if(!MyGUI.LootWindowOpen)
			Screen.lockCursor = true;

		//re-enabling the targeting reticule and camera controls for combat mode
		GameManager.Instance.targetReticule.GetComponent<GUITexture>().enabled = true;
		Player.GetComponent<CharacterCamera>().MouseEnabled = true;
		//Player.GetComponent<CharacterCtrl>().IsEnabled = true;
	}
}
