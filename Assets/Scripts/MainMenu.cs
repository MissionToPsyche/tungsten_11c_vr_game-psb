// Created 11/1/23 - Noah Pfeffer
// This script controls main menu functionality
// Modification History:
/* 
    11/1/23 - Noah Pfeffer

    11/24/23 - Noah Pfeffer

    11/29/23 - Jack Brand

    2/2/24 - Noah Pfeffer

    4/15/24 - Noah Pfeffer
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField input;

    public GameObject vignetteText;

    public GameObject movementText;

    public GameObject turnText;

    public GameObject MainMenuNormal;

    public GameObject MainMenuEvent;

    public Slider volume;

    public GameObject rightRay;

    public SceneTransition sceneTransition;

    void Start(){
        TextMeshProUGUI vText = vignetteText.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI mText = movementText.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI tText = turnText.GetComponent<TextMeshProUGUI>();
        
        if (Global.vignette == "Off"){
            vText.text = "Peripheral Vignette: Off";
        }
        else{
            vText.text = "Peripheral Vignette: On";
        }

        if (Global.movement == "Continuous"){
            mText.text = "Movement Type: Continuous";
        }
        else{
            mText.text = "Movement Type: Teleport";
        }

        if (Global.turn == "Continuous"){
            tText.text = "Turn Type: Continuous";
        }
        else{
            tText.text = "Turn Type: Snap";
        }

        volume.value = Global.volume;
        AudioListener.volume = Global.volume;

        if (Global.full == true){
            Time.timeScale = 1;
            MainMenuNormal.SetActive(true);
        }
        else{
            Time.timeScale = 1;
            MainMenuEvent.SetActive(true);
        }
    }

    //Accessibility options
    public void ChangeVolume()
    {
        Global.volume = volume.value;
        AudioListener.volume = volume.value;
    }

    public void VignetteButton()
    {
        TextMeshProUGUI vText = vignetteText.GetComponent<TextMeshProUGUI>();
        if (Global.vignette == "Off"){
            Global.vignette = "On";
            vText.text = "Peripheral Vignette: On";
        }
        else{
            Global.vignette = "Off";
            vText.text = "Peripheral Vignette: Off";
        }
    }

    public void MovementButton()
    {
        TextMeshProUGUI mText = movementText.GetComponent<TextMeshProUGUI>();
        if (Global.movement == "Continuous"){
            Global.movement = "Teleport";
            mText.text = "Movement Type: Teleport";
        }
        else{
            Global.movement = "Continuous";
            mText.text = "Movement Type: Continuous";
        }
    }

    public void TurnButton()
    {
        TextMeshProUGUI tText = turnText.GetComponent<TextMeshProUGUI>();
        if (Global.turn == "Continuous"){
            Global.turn = "Snap";
            tText.text = "Turn Type: Snap";
        }
        else{
            Global.turn = "Continuous";
            tText.text = "Turn Type: Continuous";
        }
    }
    //Function to start from the first in-game scene and do a full playthrough
    public void StartButtonFull()
    {
        rightRay.SetActive(false);
        Global.full = true;
        SetTimer();
        sceneTransition.GoToScene(1);
    }


    //Function to start from the first in-game scene (Main)
    public void StartButtonMain()
    {
        rightRay.SetActive(false);
        Global.full = false;
        SetTimer();
        sceneTransition.GoToScene(5);
    }
    //Function to start from the second in-game scene (Second)
    public void StartButtonSecond()
    {
        rightRay.SetActive(false);
        Global.full = false;
        SetTimer();
        sceneTransition.GoToScene(2);
    }
    //Function to start from the third in-game scene (Third)
    public void StartButtonThird()
    {
        rightRay.SetActive(false);
        Global.full = false;
        SetTimer();
        sceneTransition.GoToScene(3);
    }
    //Function for exit game button (does not work in the testing environment, since that would require closing Unity)
    public void ExitGame()
    {
        rightRay.SetActive(false);
        Application.Quit();
    }

    public void SetTimer()
    {
        if (float.TryParse(input.text, out float newTime))
        {
            if (newTime > 99.99)
            {
                Global.timer = (float) 99.99 * 60f;
                Global.timerInit = (float)99.99 * 60f;
            }
            else
            {
                Global.timer = newTime * 60f;
                Global.timerInit = newTime * 60f;
            }
            
        }
        else
        {
            Debug.LogWarning("Invalid input");
        }
    }
}
