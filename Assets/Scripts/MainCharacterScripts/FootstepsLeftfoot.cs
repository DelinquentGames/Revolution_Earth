/// <summary>
/// FootstepsLeftfoot.cs
/// Adam T. Davis
/// 01/20/2014
/// 
/// Handles all of the audio for the footsteps of the left foot hitting the ground.
/// </summary>
using UnityEngine;
using System.Collections;

public class FootstepsLeftfoot : MonoBehaviour {

	public Animator anim;
	public AudioSource footAudioSource;
	
	public AudioClip[] footstepWood;
	public AudioClip[] footstepMetal;
	public AudioClip[] footstepConcrete;
	public AudioClip[] footstepSand;

	public float animSpeed = 1.5f;
		
	// Update is called once per frame
	void OnTriggerEnter (Collider col) {
		//NGUIDebug.Log("Left Foot hit ground!");
		if(anim == null){
			Debug.LogError("Please attach an Animator Object!");
			return;
		}

		if (Time.time < 0.5) return;
		
		if (animSpeed != null)
		{
			footAudioSource.volume = Mathf.Clamp01(0.1f + animSpeed);
		}
		else
		{
			footAudioSource.volume = 1;
		}

		if(col.gameObject.tag == "wood" && anim.GetFloat("Footsteps")>0.01)
		{
			audio.PlayOneShot(footstepWood[Random.Range(0,footstepWood.Length)], footAudioSource.volume);
		}
		else if(col.gameObject.tag == "metal" && anim.GetFloat("Footsteps")>0.01)
		{
			audio.PlayOneShot(footstepMetal[Random.Range(0,footstepMetal.Length)], footAudioSource.volume);
		}
		else if(col.gameObject.tag == "concrete" && anim.GetFloat("Footsteps")>0.01)
		{
			audio.PlayOneShot(footstepConcrete[Random.Range(0,footstepConcrete.Length)], footAudioSource.volume);
		}  
		else if(col.gameObject.tag == "dirt" && anim.GetFloat("Footsteps")>0.01)
		{
			audio.PlayOneShot(footstepSand[Random.Range(0,footstepSand.Length)], footAudioSource.volume);
		}
		else if(col.gameObject.tag == "sand" && anim.GetFloat("Footsteps")>0.01)
		{
			audio.PlayOneShot(footstepSand[Random.Range(0,footstepSand.Length)], footAudioSource.volume);
		}   
	}
}