using UnityEngine;
using System.Collections;

public class VitalBarBasic : MonoBehaviour {
	public UILabel label;

	private UISlider _slider;
	private float _maxWidth;
	private bool _displayText = false;

	void Awake(){
		_slider = GetComponent<UISlider>();

		if(_slider == null)
		{
			Debug.LogError("Could not find the UISlider Element!");
			return;
		}

		_maxWidth = _slider.value;

		DisplayText = _displayText;
	}

	public void UpdateDisplay(float x){

		if(x < 0)
			x = 0;
		else if(x > 1)
			x = 1;

		_slider.value = _maxWidth * x;


		DisplayText = false;
	}

	public void UpdateDisplay(float x, string str){

		UpdateDisplay(x);

		if(str != "")
			label.text = str;
	}

	public bool DisplayText	{
		get{ return _displayText;}
		set{
			_displayText = value;

			if(!_displayText)
				label.text = "";
		}
	}
}
