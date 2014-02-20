using UnityEngine;
using System.Collections;

public class ToggleScript : MonoBehaviour {

	public bool IsChecked;
	public int ClassID;
	public GameObject ToolTipControl;

	void Awake(){

	}

	void OnValueChange(){

	}

	void OnTooltip(bool isOver)
	{
		string _text = "";

		switch(ClassID){
			
		case 0:
			_text = "[0099ff]TECHNOMAGE[-] \n\nImplanted with nanotechnology developed and kept secret by all technomages. These individuals posses spell like powers able to manipulate the very " +
				"molecules around them. Nanobots permiate the host limiting the amount of additional cyber technology the individual can use.";
			break;
		case 1:
			_text = "[0099ff]STREAMER[-] \n\nStealthy and resourceful streamers carry cybernetic implants that allow them to interface directly with tech. Streamers are known for their ability to " +
				"hack into secure locations take over automated systems and unlock even the most complex security seals on a door.";
			break;
		case 2:
			_text = "[0099ff]MERCENARY[-] \n\nDedicated specialists in combat these ex-military types use their skills to handle any situation. Their years as a soldier have made them hard hitting " +
				"and tough to kill. Mercenaries tend to wear a lot of armor naking them perfect for assignments requiring a need for overwhelming force.";
			break;		
		}
		
		if(isOver){
			UITooltip.ShowText(_text);

			if (ToolTipControl == null)
				return;

			Transform tt = ToolTipControl.transform.FindChild("Text");
			tt.GetComponent<UILabel>().MakePixelPerfect();
		}
		else{
			UITooltip.ShowText("");
		}
	}
}
