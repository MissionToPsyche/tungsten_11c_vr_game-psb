// Created 3/16/24 - Peyton O'Boyle
// Organizes narration usage for minigame three
// Modification History:
/* 
    3/16/24 - Peyton O'Boyle

    4/2/24 - Noah Pfeffer
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GravityScienceTextManager : MonoBehaviour
{
    //The narration text box this will be projected to
    public GameObject narrationBox;

    //The lines for each scenario
    public string[] startLines;
    public string[] buttonPressLines;
    public string[] gravityIncreaseLines;
    public string[] gravityDecreaseLines;
    public string[] endLines;

    //The ausio clips for each scenario
    public AudioClip[] startAudioLines;
    public AudioClip[] buttonPressAudioLines;
    public AudioClip[] gravityIncreaseAudioLines;
    public AudioClip[] gravityDecreaseAudioLines;
    public AudioClip[] endAudioLines;

    public GameObject console;

    private bool increasePlayed;
    private bool decreasePlayed;

    private bool finished;

    //Message displayed to user when game is completed
    public GameObject winMenu;

    public GameObject XRRig;

    // Start is called before the first frame update
    void Start()
    {
        //Select the start narration
        SelectNarration("Start");

        increasePlayed = false;
        decreasePlayed = false;
        finished = false;
    }

    public void SelectNarration(string selection)
    {
        //Play Start Narration
        if (selection == "Start")
        {
            PlayNarration(startLines, startAudioLines);
            StartCoroutine(UncoverButton());
        }

        //Play Button Press Narration
        if (selection == "ButtonPress")
        {
            PlayNarration(buttonPressLines, buttonPressAudioLines);
            StartCoroutine(UncoverSlider());
        }

        //Play Gravity Increase Narration
        if (selection == "GravityIncrease")
        {
            PlayNarration(gravityIncreaseLines, gravityIncreaseAudioLines);

            increasePlayed = true;

            if (decreasePlayed == true)
            {
                //If this was chosen last, wait for the end of these lines and then start the End text box
                StartCoroutine(Wait(gravityIncreaseAudioLines));
            }
        }

        //Play Gravity Decrease Narration
        if (selection == "GravityDecrease")
        {
            PlayNarration(gravityDecreaseLines, gravityDecreaseAudioLines);

            decreasePlayed = true;

            if (increasePlayed == true)
            {
                //If this was chosen last, wait for the end of these lines and then start the End text box
                StartCoroutine(Wait(gravityDecreaseAudioLines));
            }
        }

        if (selection == "End")
        {
            PlayNarration(endLines, endAudioLines);
        }
    }

    void PlayNarration(string[] textLines, AudioClip[] audioLines)
    {
        //Set the text lines
        narrationBox.GetComponent<GravityScienceNarrationTextBox>().lines = textLines;

        //Set the audio lines
        narrationBox.GetComponent<GravityScienceNarrationTextBox>().audioLines = audioLines;

        //Play them
        narrationBox.GetComponent<GravityScienceNarrationTextBox>().ActivateTextBox();
    }

    IEnumerator UncoverButton()
    {
        float audioLength = 0f;

        //Find length of every clip until the final one in start lines
        for (int i = 0; i < startAudioLines.Length; i++)
        {
            audioLength += startAudioLines[i].length;
        }

        //Wait it out
        yield return new WaitForSeconds(audioLength);

        //Uncover the button
        console.GetComponent<ConsoleManager>().UncoverButton();
    }

    IEnumerator UncoverSlider()
    {
        float audioLength = 0f;

        //Find length of every clip until the final one in start lines
        for (int i = 0; i < buttonPressAudioLines.Length; i++)
        {
            audioLength += buttonPressAudioLines[i].length;
        }

        //Wait it out
        yield return new WaitForSeconds(audioLength);

        //Uncover the Slider
        console.GetComponent<ConsoleManager>().UncoverSlider();
    }

    IEnumerator Wait(AudioClip[] audioLines)
    {
        for (int i = 0; i < audioLines.Length; i++)
        {
            //Wait for audio to finish
            yield return new WaitForSeconds(audioLines[i].length);
        }

        SelectNarration("End");

        finished = true;

        DisplayWinMenu();
    }

    // Display the menu based if finished
    public void DisplayWinMenu()
    {
            winMenu.SetActive(true);
    }
    
    // Transition when player presses Y at end of level
    public void YButtonPressed(InputAction.CallbackContext context)
    {
        if(finished == true)
        {
            if(context.performed)
            {
                StartCoroutine(GoToNextScene());
            }
        }
    }

    public IEnumerator GoToNextScene()
    {
        XRRig.GetComponent<MovementStill>().OnExit();

        yield return new WaitForSeconds(1);

        if(Global.full == false)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }

        yield return null;
    }
}
