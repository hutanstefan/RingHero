using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAfterRemove : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
   
    // Play the sound specified by the 'clip' variable
    public void PlaySound()
    {
        if (clip != null && audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is not assigned!");
        }
    }

    // Detach the AudioSource from its parent GameObject
    public void DetachAudioSource()
    {
        if (audioSource != null)
        {
            // Detach from parent
            audioSource.transform.parent = null;

            // Optionally, stop the audio if it's playing
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
        else
        {
            Debug.LogWarning("AudioSource is not assigned!");
        }
    }

    // Stop playing the audio
    public void StopSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
