/// <summary>
/// Clothing.cs
/// Adam T. Davis
/// 11/17/2013
/// 
/// this class controls clothing stats
/// </summary>

using UnityEngine;

public class Clothing : BuffItem {
    private EquipmentSlot _slot;            //store the slot the armor will be in

    public Clothing() {
        _slot = EquipmentSlot.Head;
    }

    public Clothing(EquipmentSlot slot) {
        _slot = slot;
    }

    public EquipmentSlot Slot {
        get { return _slot; }
        set { _slot = value; }
    }

    public override string ToolTip()
    {
        return Name + "\n" +
                "Value: " + Value + "\n" +
                "Durability: " + CurDurability + "/" + MaxDurability + "\n" +
                "Armor Type: " + Slot;
    }
}
