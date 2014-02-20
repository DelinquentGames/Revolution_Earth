using UnityEngine;
using System.Collections.Generic;

public static class ItemGenerator {
    public const int BASE_MELEE_RANGE = 1;
    public const int BASE_RANGED_RANGE = 5;

    private const string MELEE_WEAPON_PATH = "Item/Icon/Weapon/Melee/";
    private const string RANGED_WEAPON_PATH = "Item/Icon/Weapon/Ranged/";
	
    private enum rangeType {
        MELEE,
        RANGED
    }

    public static Item CreateItem() {
        //decide what type of item to make
       
        //call the method to create that base item type
        Item item = CreateWeapon(rangeType.RANGED);

//        item.Name = "MW:" + Random.Range(0, 100);
        //private string _name;

        //assign the value of the item
        item.Value = Random.Range(1, 100);

        //assign the items rarity
	    item.Rarity = RarityTypes.Common;

        //assign the items maximum durability
        item.MaxDurability = Random.Range(50, 61);

        //new items have max durability
        item.CurDurability = item.MaxDurability;
            
        //return the new Item
        return item;
    }

    private static Weapon CreateWeapon(rangeType rT)
    {
        Weapon weapon;

        //decide if we make it melee-0 or ranged-1 weapon
        switch (rT) {
            case rangeType.MELEE:
                weapon = CreateMeleeWeapon();
                break;
            default:
                weapon = CreateRangedWeapon();
                break;
        }                  
        
        //return the weapon created
        return weapon;
    }

    private static Weapon CreateRangedWeapon()
    {		
        Weapon rangedWeapon = new Weapon();
		
		
		List<string> weaponNames = new List<string>();
		
		weaponNames.Add("M4A1 Carbine");		
        //weaponNames.Add("Pistol");

        //fill in all of the values for that ItemType
        rangedWeapon.Name = weaponNames[Random.Range(0, weaponNames.Count)];

        //assign the max damage of the weapon
        rangedWeapon.MaxDamage = Random.Range(5, 11);
        rangedWeapon.DamageVariance = Random.Range(0.2f, 0.76f);
        rangedWeapon.TypeOfDamage = DamageType.Pierce;
		rangedWeapon.Description = "Description: " + "\n\n" +
								   "A compact version of the M16A2 rifle," + "\n" + 
								   "with a collapsible stock, a flat-top" + "\n" +
								   "upper receiver accessory rail and a" + "\n" +
								   "detachable handle/rear aperture site" + "\n" +
								   "assembly. The M4 enables a soldier" + "\n" +
								   "operating in close quarters to engage" + "\n" +
								   "targets at extended range with accurate," + "\n" +
								   "lethal fire. It achieves more than 85" + "\n" +
								   "percent commonality with the M16A2" + "\n" +
								   "rifle and will replace all .45 caliber" + "\n" +
								   "M3 submachine guns, selected M9 pistols," + "\n" +
								   "and M16 series rifles."  + "\n\n" + 
								   "CALIBER:   5.56 mm"  + "\n" +
								   "WEIGHT:   7.5 lbs (loaded weight with" + "\n" +
								   "                       sling & one magazine)"  + "\n" +
								   "MAX. EFFECTIVE RANGE:   600 m (area target)"  + "\n" +
								   "                                 500 m (point target)";

        //assign the max range of this weapon
        rangedWeapon.MaxRange = BASE_RANGED_RANGE;

		rangedWeapon.InventoryItem = GameObject.Find(GameConstants.RANGED_WEAPON_ICON_PATH + rangedWeapon.Name) as GameObject;

        //assign the icon for the weapon
        rangedWeapon.Icon = Resources.Load(GameConstants.RANGED_WEAPON_ICON_PATH + rangedWeapon.Name) as Texture2D;

        //return the ranged weapon
        return rangedWeapon;
    }

    private static Weapon CreateMeleeWeapon() {
        Weapon meleeWeapon = new Weapon();

        List<string> weaponNames = new List<string>();

        weaponNames.Add("Long Sword");
        weaponNames.Add("Dagger");
        weaponNames.Add("Great Axe");
        weaponNames.Add("Hammer");
        weaponNames.Add("Morningstar");

        //fill in all of the values for that ItemType
        meleeWeapon.Name = weaponNames[Random.Range(0, weaponNames.Count)];

        //assign the max damage of the weapon
        meleeWeapon.MaxDamage = Random.Range(5, 11);
        meleeWeapon.DamageVariance = Random.Range(0.2f, 0.76f);
        meleeWeapon.TypeOfDamage = DamageType.Slash;

        //assign the max range of this weapon
        meleeWeapon.MaxRange = BASE_MELEE_RANGE;

        //assign the icon for the weapon
        meleeWeapon.Icon = Resources.Load(GameConstants.MELEE_WEAPON_ICON_PATH + meleeWeapon.Name) as Texture2D;

        //return the melee weapon
        return meleeWeapon;
    }

    private static Armor CreateArmor()
    {
        Armor armor = new Armor();

        //fill in all of the values for that ItemType
        armor.Name = "A:" +  Random.Range(0, 100);

        return armor;
    }
}

public enum ItemType {
    Armor,
    Weapon,
    Consumable,
    Ammo
}
