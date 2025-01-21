using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioScript : Singleton<AudioScript>
{

    public enum Effects
    {
        Lose,
        Victory,
        Pickup
    }

    public AudioSource MusicSource; // Source Background Music
    public AudioSource EffectsSource; // Source Effects 
    public AudioClip[] SoundEffects;
    private float Volume = 1;
    public AudioClip BackgroundMusic; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerEffect(Effects Effect)
    {
        EffectsSource.volume = Volume;
        EffectsSource.PlayOneShot(SoundEffects[(int)Effect]);
    }

    public void StartMusic()
    {
        if (MusicSource != null && BackgroundMusic != null)
        {
            MusicSource.clip = BackgroundMusic;
            MusicSource.loop = true; // Loop
            MusicSource.volume = MenuScript.Instance.Volume;
            MusicSource.Play();
        }
    }
    public void ChangeVolume()
    {
        Volume = MenuScript.Instance.Volume;
        MusicSource.volume=Volume;
        EffectsSource.volume = MenuScript.Instance.Volume;
    }

    public void StopMusic()
    {
        MusicSource.Stop();
    }

}
