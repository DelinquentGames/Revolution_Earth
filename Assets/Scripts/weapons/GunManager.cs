using UnityEngine;
using System.Collections;

public class GunManager : MonoBehaviour {

	public GunKeyBinder[] guns;
	
	[HideInInspector]
	public Gun currentGun;
	
	[HideInInspector]
	public int currentWeapon;

	[HideInInspector]
	public float currentWeaponDamage;
	
	public SoldierController soldier;
	
	public HudWeapons hud;
	
	void Start()
	{
		//GameObject go = GameObject.Find("HudWeapons") as GameObject;
		
		if (hud == null)
			hud = GameObject.Find("HUDWeapons").GetComponent<HudWeapons>();
		
		if (hud == null)
			Debug.Log ("Unable to find HudWeapons Script!");
		
		for(int i = 0; i < guns.Length; i++)
		{
			guns[i].gun.enabled = false;
		}
		currentWeapon = 0;
		currentWeaponDamage = guns[0].gun.WeaponDamage;
		guns[0].gun.enabled = true;
		currentGun = guns[0].gun;
	}
	
	void Update()
	{
		for(int i = 0; i < guns.Length; i++)
		{
			if(Input.GetKeyDown(guns[i].keyToActivate))
			{
				ChangeToGun(i);
			}
		}
		
		hud.selectedWeapon = currentWeapon;
		hud.ammoRemaining[currentWeapon] = guns[currentWeapon].gun.currentRounds;
	}
	
	void ChangeToGun(int gunIndex)
	{
		Gun cGun = guns[gunIndex].gun;
		
		if(cGun.enabled)
		{
			if(guns[gunIndex].switchModesOnKey)
			{
				switch(cGun.fireMode)
				{
					case FireMode.SEMI_AUTO:
						cGun.fireMode = FireMode.FULL_AUTO;
						break;
					case FireMode.FULL_AUTO:
						cGun.fireMode = FireMode.BURST;
						break;
					case FireMode.BURST:
						cGun.fireMode = FireMode.SEMI_AUTO;
						break;
				}
			}
		}
		else
		{
			for(int j = 0; j < guns.Length; j++)
			{
				guns[j].gun.enabled = false;
			}
					
			cGun.enabled = true;
			currentGun = cGun;
			currentWeapon = gunIndex;
			currentWeaponDamage = currentGun.WeaponDamage;
		}
	}
}