using UnityEngine;
using System;
using System.Collections;

public class SpinnerUpScript : MonoBehaviour {

	public SkillScript SkillScript;
	public float _pointsLeft;

	void OnClick(){
		_pointsLeft = CharacterGenerator.Instance.PointsLeft;

		if (_pointsLeft < SkillScript.CurValue + (SkillScript.CurValue + 1f))
			return;

		if ((SkillScript.CurValue < SkillScript.MaxValue)){
			SkillScript.CurValue++;
			CharacterGenerator.Instance.PointsLeft = AdjustPointsDown(SkillScript.CurValue);
			CharacterGenerator.Instance.DisplayPointsLeft();
		}
	}

	/// <summary>
	/// Adjusts the point left for character creation by decreasing the
	/// points spent if the player increases the modified Attribute.
	/// </summary>
	/// <returns>
	/// this returns the actual points the player has left after the modfication.
	/// </returns>
	/// <param name='statValue'>
	/// This is the value of the Ability Score being adjusted.
	/// </param>
	private float AdjustPointsDown(float curValue){		
		float tempPoints;		
		
		tempPoints = _pointsLeft - ((curValue - 1) + curValue);
		//NGUIDebug.Log(tempPoints.ToString());
		
		if (tempPoints >= 0)
			return tempPoints;
		else
			return _pointsLeft;
	}
}
