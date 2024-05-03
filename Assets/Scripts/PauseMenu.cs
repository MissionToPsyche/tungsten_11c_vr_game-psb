// Created 10/31/23 - Noah Pfeffer
// This script controls pause menu functionality
// Modification History:
/* 
    10/31/23 - Noah Pfeffer

    11/9/23 - Noah Pfeffer

    11/29/23 - Jack Brand

    1/8/2024 - Noah Pfeffer

    1/30/2024 - Noah Pfeffer

    4/17/24 - Jack Brand

    4/23/24 - Noah Pfeffer

    4/25/24 - Noah Pfeffer
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.Windows;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public SceneTransition sceneTransition;

    public GameObject wristUI;
    public GameObject rightRay;

    public GameObject teleportRay;
    public GameObject locomotion;

    public GameObject gameTimer;

    public bool activeWristUI = true;

    public bool activeRightRay = true;

    public bool tutorialActive;

    public GameObject vignetteText;

    public GameObject movementText;

    public GameObject turnText;

    public Slider volume;

    public GameObject XRRig;

    // Start is called before the first frame update
    void Start()
    {
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

        if (tutorialActive == true){
            rightRay.SetActive(true);
            DisplayWristUI();
        }
        else{
            rightRay.SetActive(false);
            DisplayWristUI();
        }
    }

    void Update(){
        if (tutorialActive == true){
            rightRay.SetActive(true);
        }
    }

    // Display the menu when the pause button is pressed on the left controller
    public void PauseButtonpressed(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(gameTimer.GetComponent<GameTimer>().hasEnded == false){
                if (tutorialActive == true){
                    rightRay.SetActive(true);
                }
                else{
                    rightRay.SetActive(false);
                    DisplayWristUI();
                }
            }
        }
    }

    public void tutorialOff(){
        tutorialActive = false;
    }

    //When pause button is pressed, show menu on left hand and raycaster on right hand
    public void DisplayWristUI()
    {
        for(int i = 0; i < 500; i++){
            Debug.Log(i);
        }
        if(activeWristUI)
        {
            Global.paused = false;
            //Close menu
            wristUI.SetActive(false);
            activeWristUI = false;
            //Disable right hand ray
            rightRay.SetActive(false);
            activeRightRay = false;
            //Enable teleport ray if on
            if(Global.movement == "Teleport" && teleportRay != null && locomotion != null){
                TeleportationProvider teleport = locomotion.GetComponent<TeleportationProvider>();
                teleport.enabled = true;
                teleportRay.SetActive(true);
            }

            //Unpause game
            Time.timeScale = 1;

            //StartCoroutine(Wait());
            if(XRRig.GetComponent<MovementStill>() != null){
                XRRig.GetComponent<MovementStill>().OnUnpause();
            }
            else{
                XRRig.GetComponent<Movement>().OnUnpause();
            }
        }
        else if (!activeWristUI)
        {
            Global.paused = true;
            //Open menu
            wristUI.SetActive(true);
            activeWristUI = true;
            //Enable right hand ray
            rightRay.SetActive(true);
            activeRightRay = true;
            //Disable teleport ray if on
            if(Global.movement == "Teleport" && teleportRay != null && locomotion != null){
                TeleportationProvider teleport = locomotion.GetComponent<TeleportationProvider>();
                teleport.enabled = false;
                teleportRay.SetActive(false);
            }
            //Pause game
            Time.timeScale = 0;
        }
    }
    //Function to go to the main menu
    public void MainMenu()
    {
        rightRay.SetActive(false);
        Global.timer += 2;
        //gameTimer.GetComponent<GameTimer>().TogglePause();
        Time.timeScale = 1;
        gameTimer.GetComponent<GameTimer>().ResetTimer();
        sceneTransition.GoToScene(0);
    }
    //Function for restart game button
    public void RestartGame()
    {
        rightRay.SetActive(false);
        Global.paused = false;
        Time.timeScale = 1;
        gameTimer.GetComponent<GameTimer>().ResetTimer();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //Function for exit game button (does not work in the testing environment, since that would require closing Unity)
    public void ExitGame()
    {
        rightRay.SetActive(false);
        Application.Quit();
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
}
