using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Footsteps : MonoBehaviour
{

    public AudioSource footAudioSource;

    public AudioClip[] woodSteps;
    public AudioClip[] metalSteps;
    public AudioClip[] concreteSteps;
    public AudioClip[] sandSteps;

    private PlayerController cc;
    private Transform t;

    public LayerMask hitLayer;
    private string cTag;

    void Start()
    {
        cc = GetComponent<PlayerController>();
        t = transform;
    }

    public void OnFootStrike()
    {
        if (Time.time < 0.5) return;

        if (cc != null)
        {
            footAudioSource.volume = Mathf.Clamp01(0.1f + cc.animSpeed);
        }
        else
        {
            footAudioSource.volume = 1;
        }

        footAudioSource.PlayOneShot(GetAudio(), footAudioSource.volume);
    }

    AudioClip GetAudio()
    {
        RaycastHit hit;

        //Debug.DrawRay(t.position + new Vector3(0, 0.5, 0), -Vector3.up * 5.0);

        if (Physics.Raycast(t.position + new Vector3(0, 0.5f, 0), -Vector3.up, out hit, Mathf.Infinity, hitLayer))
        {
            cTag = hit.collider.tag.ToLower();
        }

        if (cTag == "wood")
        {
            return woodSteps[Random.Range(0, woodSteps.Length)];
        }
        else if (cTag == "metal")
        {
            return metalSteps[Random.Range(0, metalSteps.Length)];
        }
        else if (cTag == "concrete")
        {
            footAudioSource.volume = 0.8f;
            return concreteSteps[Random.Range(0, concreteSteps.Length)];
        }
        else if (cTag == "dirt")
        {
            footAudioSource.volume = 1.0f;
            return sandSteps[Random.Range(0, sandSteps.Length)];
        }
        else if (cTag == "sand")
        {
            footAudioSource.volume = 1.0f;
            return sandSteps[Random.Range(0, sandSteps.Length)];
        }
        else
        {
            footAudioSource.volume = 1.0f;
            return sandSteps[Random.Range(0, sandSteps.Length)];
        }
    }
}
