using UnityEngine;
using System.Collections;

public class GUIDriver : MonoBehaviour {

	public VitalBarBasic PlayerHealthVitalsBar;
	public VitalBarBasic PlayerManaVitalsBar;
	public VitalBarBasic EnemyHealthVitalsBar;
	public VitalBarBasic EnemyManaVitalsBar;
	public UILabel PlayerName;
	public UILabel EnemyName;
	public bool displayText = false;

	private float maxValue = 100;

	//This method is called when the game object is enabled
	public void OnEnable()
	{
		Messenger.AddListener<float>("player health update", OnChangePlayerHealth);
		Messenger.AddListener<float>("player mana update", OnChangePlayerMana);
		Messenger.AddListener<string>("set player name", SetPlayerName);
		Messenger.AddListener<float>("enemy health update", OnChangeEnemyHealthBarSize);
		Messenger.AddListener<float>("enemy mana update", OnChangeEnemyManaBarSize);
		Messenger.AddListener<string>("set enemy name", SetEnemyName);
	}

	void OnChangePlayerHealth(float curHealth){
		if(curHealth > 0){

			if (!displayText)
				PlayerHealthVitalsBar.UpdateDisplay(curHealth / maxValue);
			else
				PlayerHealthVitalsBar.UpdateDisplay(curHealth / maxValue, curHealth + "/" + maxValue);

			PlayerHealthVitalsBar.gameObject.SetActive(true);
		}
		else
			PlayerHealthVitalsBar.gameObject.SetActive(false);
	}
	
	void OnChangePlayerMana(float curMana){
		if(curMana > 0){

			if (!displayText)
				PlayerManaVitalsBar.UpdateDisplay(curMana / maxValue);
			else
				PlayerManaVitalsBar.UpdateDisplay(curMana / maxValue, curMana + "/" + maxValue);

			PlayerManaVitalsBar.gameObject.SetActive(true);
		}
		else
			PlayerManaVitalsBar.gameObject.SetActive(false);
	}
	
	void SetPlayerName(string name){
			PlayerName.text = name;
	}
	

	void OnChangeEnemyHealthBarSize(float curHealth){

		if(curHealth > 0){
			// Changed the getComponent function from returning the enemy script to returning the AdvancedAiEnemy script - RobbieC
			if (!displayText){
				EnemyHealthVitalsBar.UpdateDisplay(curHealth / maxValue);
				//SoldierTarget.enemyTarget.GetComponent<EnemyAI>().generalParameters.botHealth = curHealth;
			}
			else
			{
				// Changed the getComponent function from returning the enemy script to returning the AdvancedAiEnemy script - RobbieC
				EnemyHealthVitalsBar.UpdateDisplay(curHealth / maxValue, curHealth + "/" + maxValue);
				//SoldierTarget.enemyTarget.GetComponent<EnemyAI>().generalParameters.botHealth = curHealth;
			}

			EnemyHealthVitalsBar.gameObject.SetActive(true);
		}
		else
			EnemyHealthVitalsBar.gameObject.SetActive(false);
	}
	
	void OnChangeEnemyManaBarSize(float curMana){
		if(curMana > 0){
			if (!displayText)
				EnemyManaVitalsBar.UpdateDisplay(curMana/ maxValue);
			else
				EnemyManaVitalsBar.UpdateDisplay(curMana / maxValue, curMana + "/" + maxValue);

			EnemyManaVitalsBar.gameObject.SetActive(true);
		}
		else
			EnemyManaVitalsBar.gameObject.SetActive(false);
	
	}
	
	void SetEnemyName(string name){
		if (name != ""){
			EnemyName.text = name;
			EnemyName.enabled = true;
		}
		else
			EnemyName.enabled = false;
	}

}
