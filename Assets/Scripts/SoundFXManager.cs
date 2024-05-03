// Created 4/2/24 - Cameron Schmidt
// Manages sound effects
// Modification History:
/* 
    4/2/24 - Cameron Schmidt
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip soundFX, Transform transformSoundFX, float volume, float length)
    {
        // Instantiate audio source
        AudioSource audioSource = Instantiate(soundFXObject, transformSoundFX.position, Quaternion.identity);

        // Assign sound FX data from parameters to audio source
        audioSource.clip = soundFX;
        audioSource.volume = volume;

        // Play audio source and then 
        audioSource.Play();

        // Destroy audio source after the clip is done
        Destroy(audioSource.gameObject, length);
    }
}
