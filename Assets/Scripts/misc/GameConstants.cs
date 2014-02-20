/// <summary>
/// GameConstants.cs
/// Adam T. Davis
/// 11/17/2013
/// 
/// Contains all the Constants used throughout the game.
/// </summary>
using UnityEngine;
using System.Collections;
using System;

public static class GameConstants {

    public const string VERSION_KEY_NAME = "ver";
    public const float VERSION_NUMBER = 0.202f;
	
	public const string PLAYER_SPAWN_POINT = "Player Spawn Point";		//This is the name of the game object that player
                                                                        //will spawn at the start of the level 

    #region PlayerPrefs Constants
    private const string PLAYER_POSITION = "Player Position";
    private const string CHARACTER_MODEL_INDEX = "Model Index";
    private const string PLAYER_HEAD_INDEX = "Head Index";
    private const string SKIN_COLOR = "Skin Color";
    private const string HAIR_COLOR = "Hair Color";
    private const string HAIR_MESH = "Hair Mesh";
	private const string ALIAS = "Players Alias";
    private const string FIRST_NAME = "Players First Name";
	private const string LAST_NAME = "Players Last Name";
    private const string BASE_VALUE = " - BASE VALUE";
    private const string EXP_TO_LEVEL = " - EXP TO LEVEL";
    private const string CUR_VALUE = " - Cur Value";
    private const string CHARACTER_WIDTH = "Character Width";
    private const string CHARACTER_HEIGHT = "Character Height";
    #endregion

    #region Resource Paths
    public const string MALE_MODEL_PATH = "Character/Model/Prefab/Human/Male/";
    public const string FEMALE_MODEL_PATH = "Character/Model/Prefab/Human/Female/";

    public const string MALE_HEAD_TEXTURE_PATH = "Character/Faces/Human/Male/textures/";
	public const string FEMALE_HEAD_TEXTURE_PATH = "Character/Faces/Human/Female/textures/";

    public const string MELEE_WEAPON_ICON_PATH = "Item/Icon/Weapon/Melee/";
    public const string MELEE_WEAPON_MESH_PATH = "Item/Mesh/Weapon/Melee/";
	
	public const string RANGED_WEAPON_ICON_PATH = "Item/Icon/Weapon/Ranged/";
    public const string RANGED_WEAPON_MESH_PATH = "Item/Mesh/Weapon/Ranged/";

    public const string HUMAN_MALE_HAIR_MESH_PATH = "Character/Hair/Human/Male/Prefab";
    public const string HUMAN_MALE_HAIR_COLOR_PATH = "Character/Hair/Human/Male/Texture/";

    public const string HUMAN_FEMALE_HAIR_MESH_PATH = "Character/Hair/Human/Female/Prefab";
    public const string HUMAN_FEMALE_HAIR_COLOR_PATH = "Character/Hair/Human/Female/Texture/";
	
	public const string RIGHT_HAND_WEAPONMOUNT_PATH = "mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:RightShoulder/mixamorig:RightArm/mixamorig:RightForeArm/RightHand/_rWeaponMount/";
	public const string LEFT_HAND_WEAPONMOUNT_PATH = "mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:LeftShoulder/mixamorig:LeftArm/LeftArmForeArm/LeftHand/_lWeaponMount";

    #endregion
 
    public static string[] maleModels = { "male_model" };
	
	public static string[] femaleModels = { "female_model" };

    public static string[] levelNames = {
        "Intro",
		"CharacterGenerator",
		"TutorialIsland"
    }; //Character Generation, Character Customization
	
	static GameConstants(){
	}
	
	public static void SaveCharacterWidth( float width){
		PlayerPrefs.SetFloat(CHARACTER_WIDTH, width);
	}
	
	public static void SaveCharacterHeight( float height){
		PlayerPrefs.SetFloat(CHARACTER_HEIGHT, height);
	}
	
	public static void SaveCharacterScale( float width, float height){
		SaveCharacterWidth(width);
		SaveCharacterHeight(height);
	}
	
	public static float LoadCharacterWidth(){
		return PlayerPrefs.GetFloat(CHARACTER_WIDTH, 1);		
	}
	
	public static float LoadCharacterHeight(){
		return PlayerPrefs.GetFloat(CHARACTER_HEIGHT, 1);
	}
	
	public static float[] LoadCharacterScale(){
		float[] scale = new float[2];
		
		scale[0] = PlayerPrefs.GetFloat(CHARACTER_WIDTH, 1);
		scale[1] = PlayerPrefs.GetFloat(CHARACTER_HEIGHT, 1);
		
		return scale;
	}
	
	public static void SaveHairColor(int index){
		PlayerPrefs.SetInt(HAIR_COLOR, index);
	}
	
	public static int LoadHairColor(){
		return PlayerPrefs.GetInt(HAIR_COLOR, 0);
	}
	
	public static void SaveHairMesh(int index){
		PlayerPrefs.SetInt(HAIR_MESH, index);		
	}
	
	public static int LoadHairMesh(){
		return PlayerPrefs.GetInt(HAIR_MESH, 0);		
	}
	
	public static void SaveHair(int mesh, int color){
		SaveHairColor(color);
		SaveHairMesh(mesh);
	}
	
	public static string LoadAlias(){
		return PlayerPrefs.GetString(ALIAS, "Anon");
	}
	
	public static string LoadFirstName(){
		return PlayerPrefs.GetString(FIRST_NAME, "Anon");
	}
	
	public static string LoadLastName(){
		return PlayerPrefs.GetString(LAST_NAME, "Anon");
	}
	
	public static void SaveAlias(string alias){
		PlayerPrefs.SetString(ALIAS, alias);
	}
	
	public static void SaveFirstName(string first_name){
		PlayerPrefs.SetString(FIRST_NAME, first_name);
	}
	
	public static void SaveLastName(string last_name){
		PlayerPrefs.SetString(LAST_NAME, last_name);
	}
	
	public static void SaveAttribute(AttributeName name, Attribute attribute){
		PlayerPrefs.SetFloat(((AttributeName)name).ToString() + BASE_VALUE, attribute.BaseValue);
		PlayerPrefs.SetFloat(((AttributeName)name).ToString() + EXP_TO_LEVEL, attribute.ExpToLevel);		
	}
	
	public static Attribute LoadAttribute(AttributeName name){
		PlayerData.Instance.GetPrimaryAttribute((int)name).BaseValue = PlayerPrefs.GetInt(((AttributeName)name).ToString() + BASE_VALUE, 0);
		PlayerData.Instance.GetPrimaryAttribute((int)name).ExpToLevel = PlayerPrefs.GetInt(((AttributeName)name).ToString() + EXP_TO_LEVEL, 0);	
		
		return PlayerData.Instance.GetPrimaryAttribute((int)name);
	}
	
	public static void SaveAttributes(Attribute[] attribute){
		for(int cnt = 0; cnt < attribute.Length; cnt++)
			SaveAttribute((AttributeName)cnt, attribute[cnt]);				
	}
	
	public static Attribute[] LoadAttributes(){
		Attribute[] att = new Attribute[Enum.GetValues (typeof(AttributeName)).Length];
		
		for(int cnt = 0; cnt < att.Length; cnt++)
			att[cnt] = LoadAttribute((AttributeName)cnt);
		
		return att;
	}
	
	public static void SaveVital( VitalName name, Vital vital){
		PlayerPrefs.SetFloat(((VitalName)name).ToString() + BASE_VALUE, vital.BaseValue);
		PlayerPrefs.SetFloat(((VitalName)name).ToString() + EXP_TO_LEVEL, vital.ExpToLevel);
		PlayerPrefs.SetFloat(((VitalName)name).ToString() + CUR_VALUE, vital.CurValue);
	}
	
	public static Vital LoadVital(VitalName name){
		PlayerData.Instance.GetVital((int)name).BaseValue = PlayerPrefs.GetInt(((VitalName)name).ToString() + BASE_VALUE, 0);
		PlayerData.Instance.GetVital((int)name).ExpToLevel = PlayerPrefs.GetInt(((VitalName)name).ToString() + EXP_TO_LEVEL, 0);
		
		PlayerData.Instance.GetVital((int)name).Update();
		
		PlayerData.Instance.GetVital((int)name).CurValue = PlayerPrefs.GetInt(((VitalName)name).ToString() + CUR_VALUE, 1);
		
		return PlayerData.Instance.GetVital((int)name);
	}
	
	public static void SaveVitals(Vital[] vital){
		for(int cnt = 0; cnt < vital.Length; cnt++)
			SaveVital((VitalName)cnt, vital[cnt]);
	}
	
	public static Vital[] LoadVitals(){
		Vital[] vital = new Vital[Enum.GetValues (typeof(VitalName)).Length];
		
		for(int cnt = 0; cnt < vital.Length; cnt++)
			vital[cnt] = LoadVital((VitalName)cnt);
		
		return vital;
	}
	
	public static void SaveSkill(SkillName name, Skill skill){
		PlayerPrefs.SetFloat(((SkillName)name).ToString() + BASE_VALUE, skill.BaseValue);
		PlayerPrefs.SetFloat(((SkillName)name).ToString() + EXP_TO_LEVEL, skill.ExpToLevel);
	}
	
	public static Skill LoadSkill(SkillName name){
		Skill skill = new Skill();
		
		skill.BaseValue = PlayerPrefs.GetInt(((SkillName)name).ToString() + BASE_VALUE, 0);
		skill.ExpToLevel = PlayerPrefs.GetInt(((SkillName)name).ToString() + EXP_TO_LEVEL, 0);
		
		return skill;
	}
	
	public static void SaveSkills(Skill[] skill){
		for(int cnt = 0; cnt < skill.Length; cnt++)
			SaveSkill((SkillName)cnt, skill[cnt]);
	}
	
	public static Skill[] LoadSkills(){
		Skill[] skill = new Skill[Enum.GetValues (typeof(SkillName)).Length];
		
		for(int cnt = 0; cnt < skill.Length; cnt++)
			skill[cnt] = LoadSkill((SkillName)cnt);
		
		return skill;
	}
	
	
}