using UnityEngine;
using System.Collections;

public class SoldierDamageControl : MonoBehaviour {

	public float life;
	
	public Texture2D hitTexture;
	public Texture2D blackTexture;
	
	private float hitAlpha;
	private float blackAlpha;
	
	private float recoverTime;
	
	public AudioClip[] hitSounds;
	public AudioClip dyingSound;
	
	void Start()
	{
		SoldierController.dead = false;
		hitAlpha = 0.0f;
		blackAlpha = 0.0f;
		life = 1.0f;
	}
	
	void HitSoldier(string hit)
	{
		if(GameManager.receiveDamage)
		{
			life -= 0.05f;
			
			if(!audio.isPlaying)
			{
				if(life < 0.5 && (Random.Range(0, 100) < 30))
				{
					audio.clip = dyingSound;
				}
				else
				{
					audio.clip = hitSounds[Random.Range(0, hitSounds.Length)];
				}
				
				audio.Play();
			}
			
			recoverTime = (1.0f - life) * 10.0f;
			
			if(hit == "Dummy")
			{
				//TrainingStatistics.dummiesHit++;
			}
			else if(hit == "Turret")
			{
				//TrainingStatistics.turretsHit++;
			}
			
			if(life <= 0.0)
			{
				//PlayerController.dead = true;
			}
		}
	}
	
	void Update()
	{
		recoverTime -= Time.deltaTime;
		
		if(recoverTime <= 0.0)
		{
			life += life * Time.deltaTime;
			
			life = Mathf.Clamp(life, 0.0f, 1.0f);
			
			hitAlpha = 0.0f;
		}
		else
		{
			hitAlpha = recoverTime / ((1.0f - life) * 10.0f);
		}
	
        if(!SoldierController.dead) return;
		
		blackAlpha += Time.deltaTime;
		
		if(blackAlpha >= 1.0)
		{
			Application.LoadLevel(1);
		}
	}
	
	void OnGUI()
	{
		if(!GameManager.receiveDamage) return;
		
		Color oldColor;
		Color auxColor;
		oldColor = auxColor = GUI.color;
		
		auxColor.a = hitAlpha;
		GUI.color = auxColor;
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), hitTexture);
		
		auxColor.a = blackAlpha;
		GUI.color = auxColor;
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blackTexture);
		
		GUI.color = oldColor;
	}	
}