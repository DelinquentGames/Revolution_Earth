using UnityEngine;
using System;
using System.Collections;

public class ClassSelectionScript : MonoBehaviour {
	public UIToggle[] classes;
	public UILabel Essence;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//checking radio group to see which button selected
		for (int cnt = 0; cnt < classes.Length; cnt++){
			classes[cnt].GetComponent<ToggleScript>().ClassID = cnt;
			if(classes[cnt].value == true){
				//NGUIDebug.Log(classes[cnt].name);
				switch(cnt){
				case 0:										//TechnoMage Section
					CharacterGenerator.Instance.GetPrimaryAttribute(8).BaseValue = 2;
					CharacterGenerator.Instance.StatUpdate();
					Essence.text = CharacterGenerator.Instance.GetPrimaryAttribute(8).BaseValue.ToString();
					break;
				case 1:										//Streamer Section
					CharacterGenerator.Instance.GetPrimaryAttribute(8).BaseValue = 5.5f;
					CharacterGenerator.Instance.StatUpdate();
					Essence.text = CharacterGenerator.Instance.GetPrimaryAttribute(8).BaseValue.ToString("f1", System.Globalization.CultureInfo.InvariantCulture);
					break;
				case 2 :									//Mercenary Section
					CharacterGenerator.Instance.GetPrimaryAttribute(8).BaseValue = 6f;
					CharacterGenerator.Instance.StatUpdate();
					Essence.text = CharacterGenerator.Instance.GetPrimaryAttribute(8).BaseValue.ToString();
					break;
				}
			}
		}
	}
}
