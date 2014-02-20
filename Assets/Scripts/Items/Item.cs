using UnityEngine;

public class Item : MonoBehaviour{
    public Texture2D icon;
	public GameObject inventoryItem;
	private string _name;
	private int _value;
	private RarityTypes _rarity;
	private int _curDur;
	private int _maxDur;
	private string _description;

    void Awake() {
        Init();
    }

	public void Init(){
		_name = "Need Name";
		_value = 0;
		_rarity = RarityTypes.Common;
		_maxDur = 50;
		_curDur = _maxDur;
		_description = "Need Description";
	}
	
	public void Init(string name, int value, RarityTypes rare, int maxDur, int curDur, string description) {
		_name = name;
		_value = value;
		_rarity = rare;
		_maxDur = maxDur;
		_curDur = curDur;
		_description = description;
	}
	
	public string Name {
		get{ return _name; }
		set{ _name = value; }
	}
	
	public int Value {
		get{ return _value; }
		set{ _value = value;}
	}
	
	public RarityTypes Rarity {
		get{ return _rarity; }
		set{ _rarity = value; }
	}
	
	public int MaxDurability {
		get{ return _maxDur; }
		set{ _maxDur = value; }
	}
	
	public int CurDurability {
		get{ return _curDur; }
		set{ _curDur = value; }
	}
	
	public string Description{
		get{ return _description; }
		set{ _description = value; }
	}

	public GameObject InventoryItem{
		get{ return inventoryItem; }
		set{ inventoryItem = value; }
	}

    public Texture2D Icon
    {
        get { return icon; }
        set { icon = value; }
    }

    public virtual string ToolTip() {
        return Name + "\n" +
                "Value " + Value + "\n" +
                "Durability " + CurDurability + "/" + MaxDurability + "\n";

    }
}