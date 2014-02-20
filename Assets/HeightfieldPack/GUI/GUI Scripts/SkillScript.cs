using UnityEngine;
using System;
using System.Collections;

public class SkillScript : MonoBehaviour {

	public static SkillScript Instance;
	public UILabel ValueLabel;
	public int AttributeID;
	public int SkillID;
	public float MaxValue;
	public float MinValue;
	public float CurValue;
	public int LinkedAttributeID;
	public bool IsAttribute = false;
	public GameObject ToolTipControl;
	public bool[] IsValid;

	// Use this for initialization
	void Start () {
		Instance = this;
		CurValue = MinValue;
		IsValid = new bool[Enum.GetValues(typeof(SkillName)).Length];
	}

	void Update(){
		GameObject attribute = GameObject.Find(Enum.GetNames(typeof(AttributeName))[AttributeID]);
		GameObject skill = GameObject.Find (Enum.GetNames(typeof(SkillName))[SkillID]);
		GameObject reaction = GameObject.Find("Reaction");

		if (attribute == null)
			return;

		if (IsAttribute){
			CharacterGenerator.Instance.GetPrimaryAttribute(AttributeID).BaseValue = CurValue;
			attribute.transform.FindChild("Value").GetComponent<UILabel>().text = CharacterGenerator.Instance.GetPrimaryAttribute(AttributeID).BaseValue.ToString();
			attribute.transform.FindChild("Value").GetComponent<UILabel>().color = new Color32(67,134,179,255);
			CheckSkills();
		}

		if(!IsAttribute){
			MaxValue = CharacterGenerator.Instance.GetPrimaryAttribute(CharacterGenerator.Instance.GetSkill(SkillID).LinkedAttribute).BaseValue;
			CharacterGenerator.Instance.GetSkill(SkillID).BaseValue = CurValue;
			skill.transform.FindChild("Value").GetComponent<UILabel>().text = CharacterGenerator.Instance.GetSkill(SkillID).BaseValue.ToString();
		}

		CharacterGenerator.Instance.GetPrimaryAttribute((int)AttributeName.Reaction).BaseValue = (int)((CharacterGenerator.Instance.GetPrimaryAttribute((int)AttributeName.Agility).BaseValue / 2) + (CharacterGenerator.Instance.GetPrimaryAttribute((int)AttributeName.Intellect).BaseValue / 2));
		reaction.transform.FindChild("Value").GetComponent<UILabel>().text = (CharacterGenerator.Instance.GetPrimaryAttribute((int)AttributeName.Reaction).BaseValue).ToString();
		reaction.transform.FindChild("Value").GetComponent<UILabel>().color = new Color32(67,134,179,255);


	}

	public void CheckSkills(){

		GameObject go = GameObject.Find("UIGrid");
		GameObject reaction = GameObject.Find("Reaction");

		Transform go2;
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++){

			IsValid[cnt] = true;

			go2 = go.transform.FindChild(Enum.GetNames(typeof(SkillName))[cnt]);

			Transform go3 = go2.transform.FindChild("Value");

			if (CharacterGenerator.Instance.GetSkill(cnt).BaseValue > CharacterGenerator.Instance.GetPrimaryAttribute(CharacterGenerator.Instance.GetSkill(cnt).LinkedAttribute).BaseValue){
				go3.GetComponent<UILabel>().color = Color.red;
				go3.GetComponent<UILabel>().text = CharacterGenerator.Instance.GetSkill(cnt).BaseValue.ToString();
				IsValid[cnt] = false;
			}
			else{
				go3.GetComponent<UILabel>().color = new Color32(67,134,179,255);
				IsValid[cnt] = true;
			}

			if(IsAttribute){
				ValueLabel.color = new Color32(67,134,179,255);
				ValueLabel.text = CharacterGenerator.Instance.GetPrimaryAttribute(AttributeID).BaseValue.ToString();

			}
		}
	}

	void OnTooltip(bool isOver)
	{
		string _text = "";
		if (IsAttribute){
			switch(AttributeID){
			
			case 0:
				_text = "[0099ff]AGILITY[-] \n\n Rates the swiftness and accuracy exibited by the character.";
				break;
			case 1:
				_text = "[0099ff]COMBAT[-] \n\n Determines the Attack Strength and carrying capacity of the character.";
				break;
			case 2:
				_text = "[0099ff]BODY[-] \n\n Determines the Health and Physical Stamina of the character.";
				break;
			case 3:
				_text = "[0099ff]CHARISMA[-] \n\n Used to determine the success of persuasion. accuracy exibited by the character.";
				break;
			case 4:
				_text = "[0099ff]INTELLECT[-] \n\n Used to determine the characters academic knowledge or experience with TechnoMage technology.";
				break;
			case 5:
				_text = "[0099ff]WILLPOWER[-] \n\n Used to determine the characters mental strengths and resitances.";
				break;			
			}
		}else
		{
			switch(SkillID){
				
			case 0:
				_text = "[0099ff]ARCHERY[-] \n\nUsed to determine reload speed and accuracy with bows.";
				break;
			case 1:
				_text = "[0099ff]AUTOMATIC WEAPONS[-] \n\n Used to determine accuracy and damage with automatic weapons.";
				break;
			case 2:
				_text = "[0099ff]BLADES[-] \n\nUsed to determine attack speed and accuracy when using bladed weapons.";
				break;
			case 3:
				_text = "[0099ff]CYBERTECHNOLOGY[-] \n\nUsed to determine the success when using or defending against cyber-attacks.";
				break;
			case 4:
				_text = "[0099ff]DODGING[-] \n\nMakes it more difficult for enemies to hit you with their attacks.";
				break;
			case 5:
				_text = "[0099ff]DEMOLITIONS[-] \n\nGives a bonus to the damage and to range.";
				break;
			case 6:
				_text = "[0099ff]ELUDING[-] \n\nUsed to determine reload speed and accuracy with bows.";
				break;
			case 7:
				_text = "[0099ff]FIRST AID[-] \n\n Used to determine accuracy and damage with automatic weapons.";
				break;
			case 8:
				_text = "[0099ff]GUNNERY[-] \n\nUsed to determine attack speed and accuracy when using bladed weapons.";
				break;
			case 9:
				_text = "[0099ff]HACKING[-] \n\nUsed to determine the success when using or defending against cyber-attacks.";
				break;
			case 10:
				_text = "[0099ff]HARDWARE[-] \n\nMakes it more difficult for enemies to hit you with their attacks.";
				break;
			case 11:
				_text = "[0099ff]HEAVY WEAPONS[-] \n\nGives a bonus to the damage and to range.";
				break;	
			case 12:
				_text = "[0099ff]INTIMIDATION[-] \n\nUsed to determine reload speed and accuracy with bows.";
				break;
			case 13:
				_text = "[0099ff]LEADERSHIP[-] \n\n Used to determine accuracy and damage with automatic weapons.";
				break;
			case 14:
				_text = "[0099ff]LOCKSMITH[-] \n\nUsed to determine attack speed and accuracy when using bladed weapons.";
				break;
			case 15:
				_text = "[0099ff]MAGIC RESISTANCE[-] \n\nUsed to determine the success when using or defending against cyber-attacks.";
				break;
			case 16:
				_text = "[0099ff]MECHANICAL ADVANTAGE[-] \n\nMakes it more difficult for enemies to hit you with their attacks.";
				break;
			case 17:
				_text = "[0099ff]MEDICINE[-] \n\nGives a bonus to the damage and to range.";
				break;
			case 18:
				_text = "[0099ff]NEGOTIATION[-] \n\nUsed to determine reload speed and accuracy with bows.";
				break;
			case 19:
				_text = "[0099ff]PERCEPTION[-] \n\n Used to determine accuracy and damage with automatic weapons.";
				break;
			case 20:
				_text = "[0099ff]PISTOLS[-] \n\nUsed to determine attack speed and accuracy when using bladed weapons.";
				break;
			case 21:
				_text = "[0099ff]RIFLES[-] \n\nUsed to determine the success when using or defending against cyber-attacks.";
				break;
			case 22:
				_text = "[0099ff]STEALTH[-] \n\nMakes it more difficult for enemies to hit you with their attacks.";
				break;
			case 23:
				_text = "[0099ff]STREET KNOWLEDGE[-] \n\nGives a bonus to the damage and to range.";
				break;	
			case 24:
				_text = "[0099ff]TRACKING[-] \n\nMakes it more difficult for enemies to hit you with their attacks.";
				break;
			case 25:
				_text = "[0099ff]UNARMED COMBAT[-] \n\nGives a bonus to the damage and to range.";
				break;
			}
		}

		if(isOver){
			UITooltip.ShowText(_text);

			if(ToolTipControl == null)
				return;

			Transform tt = ToolTipControl.transform.FindChild("Text");
			//tt.position = new Vector3(0, 0, tt.position.z);
			tt.GetComponent<UILabel>().MakePixelPerfect();
		}
		else{
			UITooltip.ShowText("");
		}
	}
}
