/// <summary>
/// Jewelry.cs
/// Adam T. Davis
/// 07/07/2013
/// 
/// this class controls Jewelry types
/// </summary>

using UnityEngine;

public class Jewelry : BuffItem {
    private JewelrySlot _slot;           //store the slot the jewelry is in

    private Jewelry() {
        _slot = JewelrySlot.Earrings;
    }

    public Jewelry(JewelrySlot slot) {
        _slot = slot;
    }

    public JewelrySlot Slot {
        get { return _slot; }
        set { _slot = value; }
    }
}