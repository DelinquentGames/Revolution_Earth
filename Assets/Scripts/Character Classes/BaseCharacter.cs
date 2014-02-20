using UnityEngine;
using System;				//added to access the enum class
using System.Collections;

public class BaseCharacter : MonoBehaviour {
    public GameObject leftHandMount;
    public GameObject rightHandMount;
    public GameObject helmetMount;
    public GameObject hairMount;
	//public GameObject cameraMount;
    //public GameObject characterMaterialMesh;

	private string _alias;
	private string _firstName;
	private string _lastName;
	private int _level;
	private uint _freeExp;
	
	public Attribute[] primaryAttribute;
	private Vital[] _vital;
	public Skill[] skill;
	
	public virtual void Awake(){
		_alias = string.Empty;
		_firstName = string.Empty;
		_lastName = string.Empty;
		_level = 0;
		_freeExp = 0;
		
		primaryAttribute = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];
		_vital = new Vital[Enum.GetValues(typeof(VitalName)).Length];
		skill = new Skill[Enum.GetValues(typeof(SkillName)).Length];
		
		SetupPrimaryAttributes();
		SetupVitals();
		SetupSkills();
	}
	
	public string Alias{
		get{ return _alias; }
		set{ _alias = value; }
	}
	
	public string FirstName{
		get{ return _firstName; }
		set{ _firstName = value; }
	}
	
	public string LastName{
		get{ return _lastName; }
		set{ _lastName = value; }
	}
	
	public int Level{
		get{ return _level; }
		set{ _level = value; }
	}
	
	public uint FreeExp{
		get{ return _freeExp; }
		set{ _freeExp = value; }
	}
	
	public void AddExp(uint exp){
		_freeExp += exp;
		
		CalculateLevel();
	}
	
	//take the average of all of the players skills and make that the players level
	public void CalculateLevel(){
		
	}
	
	private void SetupPrimaryAttributes(){
		for(int cnt = 0; cnt < primaryAttribute.Length; cnt++){
			primaryAttribute[cnt] = new Attribute();
			primaryAttribute[cnt].Name = ((AttributeName)cnt).ToString();
		}
	}
	
	private void SetupVitals(){
		for(int cnt = 0; cnt < _vital.Length; cnt++)
			_vital[cnt] = new Vital();
	}
	
	private void SetupSkills(){
		for(int cnt = 0; cnt < skill.Length; cnt++)
			skill[cnt] = new Skill();
		
		SetupSkillLinkedAttributes();
	}
	
	public Attribute GetPrimaryAttribute(int index){
		return primaryAttribute[index];
	}
	
	public Vital GetVital(int index){
		return _vital[index];
	}
	
	public Skill GetSkill(int index){
		return skill[index];
	}
	
	private void SetupVitalModifiers(){
		//Health
        //GetVital((int)VitalName.Essense).BaseValue = 6;
		//GetVital((int)VitalName.Health).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), 0.25f));
		//GetVital((int)VitalName.Health).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Body), 0.25f));
		//Reaction
		        
		//Mana
        //GetVital((int)VitalName.Magic).BaseValue = GetVital((int)VitalName.Essense).BaseValue;
		//GetVital((int)VitalName.Mana).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Mind), 0.25f));
		//GetVital((int)VitalName.Mana).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Intellect), 0.25f));
	}
	
	private void SetupSkillLinkedAttributes(){
		// Agility
		GetSkill((int)SkillName.Archery).LinkedAttribute = (int)AttributeName.Agility;		
		GetSkill((int)SkillName.Blades).LinkedAttribute = (int)AttributeName.Agility;
		GetSkill((int)SkillName.Dodge).LinkedAttribute = (int)AttributeName.Agility;
		GetSkill((int)SkillName.Unarmed_Combat).LinkedAttribute = (int)AttributeName.Agility;
		GetSkill((int)SkillName.Gunnery).LinkedAttribute = (int)AttributeName.Agility;
		GetSkill((int)SkillName.Locksmith).LinkedAttribute = (int)AttributeName.Agility;
		GetSkill((int)SkillName.Stealth).LinkedAttribute = (int)AttributeName.Agility;
		GetSkill((int)SkillName.Eluding).LinkedAttribute = (int)AttributeName.Agility;
		
		// Body
		GetSkill((int)SkillName.First_Aid).LinkedAttribute = (int)AttributeName.Body;
		GetSkill((int)SkillName.Medicine).LinkedAttribute = (int)AttributeName.Body;
		
		// Combat
		GetSkill((int)SkillName.Pistols).LinkedAttribute = (int)AttributeName.Combat;
		GetSkill((int)SkillName.Rifles).LinkedAttribute = (int)AttributeName.Combat;
		GetSkill((int)SkillName.Automatic_Weapons).LinkedAttribute = (int)AttributeName.Combat;
		GetSkill((int)SkillName.Heavy_Weapons).LinkedAttribute = (int)AttributeName.Combat;
		GetSkill((int)SkillName.Demolitions).LinkedAttribute = (int)AttributeName.Combat;
		
		// Charisma
		GetSkill((int)SkillName.Intimidation).LinkedAttribute = (int)AttributeName.Charisma;
		GetSkill((int)SkillName.Leadership).LinkedAttribute = (int)AttributeName.Charisma;
		GetSkill((int)SkillName.Negotiation).LinkedAttribute = (int)AttributeName.Charisma;
		
		// Intellect
		GetSkill((int)SkillName.Street_Knowledge).LinkedAttribute = (int)AttributeName.Intellect;
		GetSkill((int)SkillName.Tracking).LinkedAttribute = (int)AttributeName.Intellect;		
		GetSkill((int)SkillName.Cyber_Technology).LinkedAttribute = (int)AttributeName.Intellect;

	}
	
	public void StatUpdate(){
		for(int cnt = 0; cnt < _vital.Length; cnt++){
			_vital[cnt].Update();
		}
		
		for(int cnt = 0; cnt < skill.Length; cnt++){
			skill[cnt].Update();
		}
	}
}
