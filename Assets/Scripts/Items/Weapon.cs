/// <summary>
/// Weapon.cs
/// Adam T. Davis
/// 11/17/2013
/// 
/// this class controls weapon damage types
/// </summary>
using UnityEngine;

public class Weapon : BuffItem {
	private int _maxDamage;
	private float _dmgVar;
	private float _maxRange;
	public DamageType dmgType;

	public Weapon(){
		_maxDamage = 0;
		_dmgVar = 0;
		_maxRange = 0;
		dmgType = DamageType.Bludgeon;
	}

	public Weapon(int mDmg, float dmgV, float mRange, DamageType dt){
		_maxDamage = mDmg;
		_dmgVar = dmgV;
		_maxRange = mRange;
		dmgType = dt;
	}

	public int MaxDamage{
		get{ return _maxDamage; }
		set{ _maxDamage = value; }
	}

	public float DamageVariance{
		get{ return _dmgVar; }
		set{ _dmgVar = value; }
	}

	public float MaxRange{
		get{ return _maxRange; }
		set{ _maxRange = value; }
	}

	public DamageType TypeOfDamage{
		get{ return dmgType; }
		set{ dmgType = value; }
	}

    public override string ToolTip() {
//        return Name + "\n" +
//                "Value " + Value + "\n" +
//                "Durability " + CurDurability + "/" + MaxDurability + "\n" +
//                (int)(MaxDamage * DamageVariance) + " - " + MaxDamage;
		return Description;
    }
}
