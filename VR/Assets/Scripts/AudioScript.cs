using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioScript : Singleton<AudioScript>
{

    public enum Effects // Enum for SoundEffect List
    {
        Defeat,
        Victory,
        Pickup
    }

    // Source Background Music
    [SerializeField]
    private AudioSource musicSource;

    // Source Sound Effects 
    [SerializeField]
    private AudioSource effectsSource;

    // Sound effects clip list
    [SerializeField]
    private AudioClip[] soundEffects;

    // Volume of all the source
    private float volume;

    // Background Music clip
    [SerializeField]
    private AudioClip backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        volume = 0.5f;
    }

    // Play one of the Sound Effect based on what is passed
    // I used enum to be able to know what effect am calling instead of a simple int
    public void PlayEffect(Effects Effect)
    {
        // Check if the Source and the effect are not null
        if (effectsSource != null && soundEffects[(int)Effect] != null)
        {
            //Play once the chosen sound effect
            effectsSource.PlayOneShot(soundEffects[(int)Effect]);
        }
    }

    // Start the BackGround Music
    public void StartMusic()
    {
        // Check if the Source and the effect are not null
        if (musicSource != null && backgroundMusic != null)
        {
            // Set BackGround Music
            musicSource.clip = backgroundMusic;

            // Song is looped
            musicSource.loop = true;

            //Play the BackGround Music
            musicSource.Play();
        }
    }

    // Called when the Player Change the volume
    public void ChangeVolume()
    {
        // Set the new volume value
        volume = MenuScript.Instance.Volume;

        // Set BackGround Music volume
        musicSource.volume = volume;

        // Set Sound Effect volume
        effectsSource.volume = volume;
    }

    // Stop the BackGroundMusic
    public void StopMusic()
    {
        musicSource.Stop();
    }

}
