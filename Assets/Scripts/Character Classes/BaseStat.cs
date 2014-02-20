/// <summary>
/// BaseStat.cs
/// Adam T. Davis
/// 06/25/2013
/// 
/// Base class for character stats used in game
/// </summary>

public class BaseStat {
	public const int STARTING_EXP_COST = 100;	//publicly accessable value for all base stats to start at
	
	private float _baseValue;						//the base value of this stat
	private float _buffValue;						//the amount of the buff to this stat
	private int _expToLevel;					//the total amount of exp needed to raise this stat
	private float _levelModifier;				//the modifier applied to the exp needed to raise this stat
	
	/// <summary>
	/// Initializes a new instance of the <see cref="BaseStat"/> class.
	/// </summary>
	public BaseStat(){
		//UnityEngine.Debug.Log("Base Stat Created");
		_baseValue = 0;	
		_buffValue = 0;	
		_expToLevel = STARTING_EXP_COST;	
		_levelModifier = 1.1f;
	}
	
#region Basic Setters and Getters	
	
	/// <summary>
	/// Gets or sets the _baseValue.
	/// </summary>
	/// <value>
	/// The _baseValue.
	/// </value>
	
	public float BaseValue{
		get{ return _baseValue; }
		set{ _baseValue = value; }
	}
	
	/// <summary>
	/// Gets or sets the _buffValue.
	/// </summary>
	/// <value>
	/// The _buffValue.
	/// </value>
	public float BuffValue{
		get{ return _buffValue; }
		set{ _buffValue = value; }
	}
	
	/// <summary>
	/// Gets or sets the _expToLevel.
	/// </summary>
	/// <value>
	/// The _expToLevel.
	/// </value>
	public int ExpToLevel{
		get{ return _expToLevel; }
		set{ _expToLevel = value; }
	}
	
	/// <summary>
	/// Gets or sets the _levelModifier.
	/// </summary>
	/// <value>
	/// The _levelModifier.
	/// </value>
	public float LevelModifier{
		get{ return _levelModifier; }
		set{ _levelModifier = value; }		
	}
	
#endregion
	
	/// <summary>
	/// Calculates the exp to level.
	/// </summary>
	/// <returns>
	/// The exp to level.
	/// </returns>
	private int CalculateExpToLevel(){
		return (int)(_expToLevel * _levelModifier);
	}
	
	/// <summary>
	/// Assign the new value to _expToLevel and then increase the
	/// _baseValue by one.
	/// </summary>
	public void LevelUp(){
		_expToLevel = CalculateExpToLevel();
		_baseValue ++;
	}
	
	/// <summary>
	/// Recalculate the adjusted base value and return it.
	/// </summary>
	/// <value>
	/// The adjusted base value.
	/// </value>
	public float AdjustedBaseValue{
		get{ return _baseValue + _buffValue; }
	}
}
