using UnityEngine;
using System;
using System.Collections;

public class SpinnerDownScript : MonoBehaviour {

	public SkillScript SkillScript;
	public float _pointsLeft;
		
	void OnClick(){
		_pointsLeft = CharacterGenerator.Instance.PointsLeft;

		if (SkillScript.CurValue > SkillScript.MinValue){
			SkillScript.CurValue--;
			CharacterGenerator.Instance.PointsLeft = AdjustPointsUp(SkillScript.CurValue);
			CharacterGenerator.Instance.DisplayPointsLeft();
		}
	}

	/// <summary>
	/// Adjusts the point left for character creation by returning the
	/// points spent if the player reduces the modified Attribute.
	/// </summary>
	/// <returns>
	/// this returns the actual points the player has left after the modfication.
	/// </returns>
	/// <param name='statValue'>
	/// This is the value of the Ability Score being adjusted.
	/// </param>
	public float AdjustPointsUp(float curValue){		
		float tempPoints;
		
		tempPoints = _pointsLeft + ((curValue - 1) + (curValue + 2));
		
		if (tempPoints >= _pointsLeft)
			return tempPoints;
		else
			return _pointsLeft;
	}
}
