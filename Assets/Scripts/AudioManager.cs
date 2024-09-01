using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AudioManager class that inherits from MonoBehaviour, used for managing audio in the game
public class AudioManager : MonoBehaviour
{
    // Header attribute for organizing the inspector in Unity
    [Header("---------- Audio Source ----------")]
    // SerializeField attribute allows private fields to be seen and edited in the inspector
    [SerializeField] AudioSource musicSource; // Audio source for background music
    [SerializeField] AudioSource SFXSource; // Audio source for sound effects (SFX)

    // Header attribute for organizing the inspector in Unity
    [Header("---------- Audio Clip ----------")]
    // Public field for background music clip, can be set in the inspector
    public AudioClip backgroundMusic;
    // Public field for the clip played when a heart is collected, can be set in the inspector
    public AudioClip heartCollect;
    // Public field for the clip played when an obstacle is hit, can be set in the inspector
    public AudioClip obstacleBop;
    // Public field for the clip played when the game is over
    public AudioClip gameOver;

    // Start method is called when the script begins to execute
    private void Start()
    {
        // Sets the background music to the audio source
        musicSource.clip = backgroundMusic;
        // Plays the background music
        musicSource.Play();
    }

    // Public method to play a sound effect (SFX)
    public void PlaySFX(AudioClip clip)
    {
        // Plays the given audio clip once through the SFX source
        SFXSource.PlayOneShot(clip);
    }
}
