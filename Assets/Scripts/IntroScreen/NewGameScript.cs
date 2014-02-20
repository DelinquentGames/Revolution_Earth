using UnityEngine;
using System.Collections;

public class NewGameScript : MonoBehaviour {

	public GUIMainMenu mainMenuScript;

	void OnClick(){
		mainMenuScript.StartCoroutine("LoadLevelWithProgress", mainMenuScript.LevelToLoad);
	}
}
