using UnityEngine;
using System.Collections;

public class Cloak : MonoBehaviour {

	public Material[] materials;
	private int materials_counter = 0;

	public AudioClip[] sounds;

	private int sounds_counter = 0;

	void Update(){
		if(Input.GetKeyDown("f")){
			renderer.material = materials[GetNextMaterialIndex()];

			if(sounds_counter != 0){
				audio.clip = sounds[GetNextSoundIndex()];
				audio.Play();
			}
		}
	}

	int GetNextMaterialIndex(){
		materials_counter++;
		if(materials_counter == materials.Length){
			materials_counter = 0;
		}
		return materials_counter;
	}

	int GetNextSoundIndex(){
		sounds_counter++;
		if(sounds_counter == sounds.Length){
			sounds_counter = 0;
		}
		return sounds_counter;
	}
}
