// Created 11/1/23 - Peyton O'Boyle
// Handles narration and audio sources for minigame three
// Modification History:
/* 
    11/1/23 - Peyton O'Boyle
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GravityScienceNarrationTextBox : MonoBehaviour
{
    public GameObject textBox;

    //The text to be displayed by the text box
    public string[] lines;

    //The audio clips of the narration for each line
    public AudioClip[] audioLines;

    //If this will be activated on start of scene or not
    public bool activateOnStart = false;

    //If this text box activates on start, start the text box
    void Start()
    {
        StartCoroutine(StartTextBox());
    }

    //Publically accessible way to start the text box
    public void ActivateTextBox()
    {
        StartCoroutine(StartTextBox());
    }

    //Displays lines of text one by one and plays the associated narration.
    IEnumerator StartTextBox()
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();

        for (int i = 0; i < lines.Length; i++)
        {
            //Display the line in the text
            textBox.GetComponent<TextMeshPro>().text = lines[i];

            //Play the audio associated with the line
            audioSource.clip = audioLines[i];
            audioSource.Play();

            //Wait for audio to finish
            yield return new WaitForSeconds(audioLines[i].length);
        }
    }
}
