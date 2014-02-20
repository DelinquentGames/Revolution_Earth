using UnityEngine;
using System;
using System.Collections;

public class CharacterGenerator : BaseCharacter {

	public static CharacterGenerator Instance;

	public UILabel GenerationPointsLabel;
	public UILabel EssenceLabel;
	public UIButton StartButton;
	public GameObject[] Attribute;
	public float PointsLeft;
    
	private const int STARTING_ESSENCE = 6;
	private const int STARTING_ATTRIBUTE_VALUE = 1;
	private const int MIN_STARTING_ATTRIBUTE_VALUE = 1;
	private int STARTING_POINTS = 37;
	private SkillScript attribute;
	private bool canStart = false;
    	
	// Use this for initialization
	void Start () {
		StartButton.isEnabled = false;
		Instance = this;
		PointsLeft = STARTING_POINTS;

		//setting our default base Attribute scores
		for(int cnt = 0; cnt < 6; cnt++){

			attribute = Attribute[cnt].GetComponent<SkillScript>();
			GetPrimaryAttribute(cnt).BaseValue = STARTING_ATTRIBUTE_VALUE;
			attribute.CurValue = STARTING_ATTRIBUTE_VALUE;
			Attribute[cnt].transform.FindChild("Value").GetComponent<UILabel>().text = attribute.CurValue.ToString();
			PointsLeft -= (MIN_STARTING_ATTRIBUTE_VALUE);
		}
		GetPrimaryAttribute((int)AttributeName.Reaction).BaseValue = (GetPrimaryAttribute((int)AttributeName.Agility).BaseValue * 0.5f) + (GetPrimaryAttribute((int)AttributeName.Intellect).BaseValue * 0.5f);
		Attribute[(int)AttributeName.Reaction].transform.FindChild("Value").GetComponent<UILabel>().text = attribute.CurValue.ToString();
		DisplayPointsLeft();
		
		//assigning Essence value
		GetPrimaryAttribute(8).BaseValue = 6;
		EssenceLabel.text = STARTING_ESSENCE.ToString();
		StatUpdate();
	}

	public void DisplayPointsLeft(){
		GenerationPointsLabel.text = PointsLeft.ToString();
	}

	void LateUpdate(){

		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++){
			SkillScript.Instance.CheckSkills();
			//NGUIDebug.Log(SkillScript.Instance.IsValid[cnt].ToString());
			
			if (!SkillScript.Instance.IsValid[cnt]){
				canStart = false;
				break;
			}
			else
				canStart = true;
		}

		//NGUIDebug.Log(PointsLeft.ToString() + " , " + canStart.ToString());

		if (PointsLeft < 1 && canStart)
			StartButton.isEnabled = true;
		else 
			StartButton.isEnabled = false;
	}
}
