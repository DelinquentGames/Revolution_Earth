/// <summary>
/// Armor.cs
/// Adam T. Davis
/// 07/07/2013
/// 
/// this class controls Armor stats
/// </summary>
using UnityEngine;

public class Armor : Clothing {
    private int _armorLevel;        // the armor level of this piece of armor

    public Armor() {
        _armorLevel = 0;
    }

    public Armor(int al) {
        _armorLevel = al;
    }

    public int ArmorLevel
    {
        get { return _armorLevel; }
        set { _armorLevel = value; }
    }

    public override string ToolTip()
    {
        return Name + "\n" +
                "Value " + Value + "\n" +
                "Durability " + CurDurability + "/" + MaxDurability + "\n" +
                "Armor Level " + ArmorLevel;
    }

}

