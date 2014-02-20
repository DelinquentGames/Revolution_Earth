/// <summary>
/// Attribute.cs
/// Adam T. Davis
/// 06/25/2013
/// 
/// Class containing all of the character attributes
/// in the game
/// </summary>
public class Attribute : BaseStat {
	new public const int STARTING_EXP_COST = 50;	//this is the starting cost of all our attributes
	
	private string _name;							//this is the name of the attribute
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Attribute"/> class.
	/// </summary>
	public Attribute(){
		//UnityEngine.Debug.Log("Attribute Created");
		_name = "";
		ExpToLevel = STARTING_EXP_COST;
		LevelModifier = 1.05f;
	}
	
	/// <summary>
	/// Gets or sets the _name.
	/// </summary>
	/// <value>
	/// The _name.
	/// </value>
	public string Name {
		get{ return _name; }
		set{ _name = value; }
	}
}


