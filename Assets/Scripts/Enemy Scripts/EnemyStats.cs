using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {
	
	public int maxHealth = 100;
	public int curHealth = 100;

    public int maxVigor = 100;
    public int curVigor = 100;

    public int maxMana = 100;
    public int curMana = 100;
	
	public float healthBarLength;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		AdjustCurrentHealth(0);
        AdjustCurrentVigor(0);
        AdjustCurrentMana(0);
	}
	
	void OnGUI(){
	}
	
	public void AdjustCurrentHealth(int adj){
		curHealth += adj;
		
		if(curHealth < 0)
			curHealth = 0;
		
		if(curHealth > maxHealth)
			curHealth = maxHealth;
		
		if(maxHealth < 1)
			maxHealth = 1;
	}

    public void AdjustCurrentVigor(int adj)
    {
        curVigor += adj;

        if (curVigor < 0)
            curVigor = 0;

        if (curVigor > maxMana)
            curVigor = maxMana;

        if (maxMana < 1)
            maxMana = 1;
    }

    public void AdjustCurrentMana(int adj)
    {
        curMana += adj;

        if (curMana < 0)
            curMana = 0;

        if (curMana > maxMana)
            curMana = maxMana;

        if (maxMana < 1)
            maxMana = 1;
    }

}
