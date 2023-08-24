using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCoreComponent : CoreComponent
{
    AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();

        audioSource = GetComponent<AudioSource>();
    }
    
    public void InitAudio(AudioClip audio)
    {
        if (audioSource == null) return;

        audioSource.clip = audio;
        audioSource.Play();
    }
}
