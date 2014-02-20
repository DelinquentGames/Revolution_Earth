using UnityEngine;
using System;
using System.Collections;

public class StartButtonScript : MonoBehaviour {

	public GameObject ProgressBar;
	public UISlider percentLoaded;
	
	[HideInInspector]
	public string LevelToLoad = GameConstants.levelNames[2];

	void OnClick(){
		ProgressBar.SetActive(true);

		GameConstants.SaveAlias("WidowMaker");

		GameConstants.SaveAttributes(CharacterGenerator.Instance.primaryAttribute);


		GameConstants.SaveSkills(CharacterGenerator.Instance.skill);
		StartCoroutine("LoadLevelWithProgress", LevelToLoad);
	}

	// adjusts the Progressbar with the amount loaded
	IEnumerator LoadLevelWithProgress(string levelToLoad){
		if (levelToLoad == "")
			yield break;
		
		var async = Application.LoadLevelAsync(levelToLoad);
		while(!async.isDone){
			percentLoaded.value = async.progress;
			yield return null;
		}
	}
}
