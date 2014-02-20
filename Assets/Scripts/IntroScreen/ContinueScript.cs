using UnityEngine;
using System.Collections;

public class ContinueScript : MonoBehaviour {

	public GUIMainMenu mainMenuScript;
	
	void OnClick(){
		mainMenuScript.StartCoroutine("LoadLevelWithProgress", GameConstants.levelNames[2]);
	}
}
