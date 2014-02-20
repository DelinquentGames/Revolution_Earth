using UnityEngine;
using System;
using System.Collections;

public class LoadSkills : BaseCharacter {

	public GameObject grid;
	public GameObject _skill;
	public UILabel _skillName;

	// Use this for initialization
	void Start () {
		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++){
			GameObject _go = NGUITools.AddChild(grid, _skill);
			_go.name = Enum.GetNames(typeof(SkillName))[cnt];
			_go.GetComponent<SkillScript>().SkillID = cnt;
			_go.GetComponentInChildren<UILabel>().text = StringEnum.GetStringValue(((SkillName)cnt)).ToUpper();
			_go.GetComponent<BoxCollider>().center = new Vector3(168.41f, -22.6f, 0f);
			_go.GetComponent<BoxCollider>().size = new Vector3(278.2f, -40.3f, 0.001f);
			Transform sn = _go.transform.FindChild("SkillName");
			sn.GetComponent<UILabel>().MakePixelPerfect();
			sn.position = new Vector3(sn.position.x + 0.08f, sn.position.y - 0.04f, sn.position.z);
			Transform sbd = _go.transform.FindChild("SpinnerButtonDown");
			sbd.GetComponent<UISprite>().MakePixelPerfect();
			sbd.position = new Vector3(sbd.position.x + 0.01f, sbd.position.y - 0.06f, sbd.position.z);
			Transform sbu = _go.transform.FindChild("SpinnerButtonUp");
			sbu.GetComponent<UISprite>().MakePixelPerfect();
			sbu.position = new Vector3(sbu.position.x + 0.01f, sbu.position.y, sbu.position.z);
			Transform v = _go.transform.FindChild("Value");
			v.GetComponent<UILabel>().MakePixelPerfect();
			v.position = new Vector3(v.position.x + 0.8f, v.position.y, v.position.z);
		}
		grid.GetComponent<UIGrid>().Reposition();	
	}
}
