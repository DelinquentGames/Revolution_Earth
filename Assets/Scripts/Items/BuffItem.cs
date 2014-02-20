/// <summary>
/// BuffItem.cs
/// Adam T. Davis
/// 07/08/2013
/// 
/// 
/// </summary>
using UnityEngine;
using System;
using System.Collections;

public class BuffItem : Item {
	
	private Hashtable buffs;
	/// <summary>
	/// Might, 50
	/// Melee Offense, 100
	/// </summary>

	public BuffItem(){
		buffs = new Hashtable ();
	}
	
	public BuffItem(Hashtable ht) {
		
	}
	
	public void AddBuff(Attribute stat, int mod){
		try{
			buffs.Add(stat, mod);
		}
		catch(UnityException e) {
			Debug.LogWarning (e.ToString());
		}
	}

	public void RemoveBuff(Attribute stat){
		buffs.Remove (stat.Name);
	}

	public int BuffCount(){
		return buffs.Count;
	}

	public Hashtable GetBuffs(){
		return buffs;
	}
}
