using UnityEngine;
using System.Collections;

public class EssenceScript : MonoBehaviour {

	public GameObject ToolTipControl;

	void OnTooltip(bool isOver)
	{
		if(isOver){
			UITooltip.ShowText("[0099ff]ESSENCE[-] \n\n The amount of your remaining humanity.\n\n If this ever drops below a 1 in value you are 100% machine.");
			Transform tt = ToolTipControl.transform.FindChild("Text");
			//tt.position = new Vector3(0, 0, tt.position.z);
			tt.GetComponent<UILabel>().MakePixelPerfect();
		}
		else{
			UITooltip.ShowText("");
		}
	}
}
