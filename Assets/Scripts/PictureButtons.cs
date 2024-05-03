// Created 11/24/23 - Noah Pfeffer
// This script keeps track of the game state and functionality for the camera minigame
// Modification History:
/* 
    11/24/23 - Noah Pfeffer
    1/30/24 - Jack Brand
    2/8/24 - Jack Brand
    2/24/24 - Noah Pfeffer
    3/16/24 - Noah Pfeffer
    4/23/24 - Noah Pfeffer
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Events;

public class PictureButtons : MonoBehaviour
{
    //Color buttons will change to when selected with camera
    public Color completedColor;
    //Locations the camera can select
    public Button locationOne;
    public Button locationTwo;
    public Button locationThree;
    public Button locationFour;

    public GameObject location1;
    public GameObject location2;
    public GameObject location3;
    public GameObject location4;
    public GameObject location5;

    public GameObject b1, b2, b3, b4, b5;

    public Material startMat;
    public Material endMat;
    //Message displayed to user when game is completed
    public GameObject winMenu;
    //Booleans to keep track of which locations are selected
    public bool locationOneFinished = false;
    public bool locationTwoFinished = false;
    public bool locationThreeFinished = false;
    public bool locationFourFinished = false;
    public bool locationFiveFinished = false;

    public GameObject rightRay;

    public GameObject pictureUI;
    public GameObject descriptionOne;
    public GameObject descriptionTwo;
    public GameObject descriptionThree;
    public GameObject descriptionFour;
    public GameObject descriptionFive;

    public GameObject narrationTextBox;

    public PauseMenu pauseMenu;

    public UnityEvent LocationOneFired;
    public UnityEvent LocationTwoFired;
    public UnityEvent LocationThreeFired;
    public UnityEvent LocationFourFired;

    public UnityEvent LocationFiveFired;

    public GameObject XRRig;

    // Start is called before the first frame update
    void Start()
    {
        winMenu.SetActive(false);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(locationOneFinished == true && locationTwoFinished == true && locationThreeFinished == true && locationFourFinished == true && locationFiveFinished == true)
        {
            DisplayWinMenu();
        }
    }
    //Change color of buttons when they are selected by camera
    public void changeColors(GameObject location){
        
        if (!(location.GetComponent<MeshRenderer>().material == endMat)){
            location.GetComponent<MeshRenderer>().material = endMat;
            
        }
    }

    public void removeText(GameObject location)
    {
        location.SetActive(false);
    }
    //When location one is selected by camera
    public void LocationOnePressed()
    {
        changeColors(location1);
        removeText(b1);
        locationOneFinished = true;

        LocationOneFired.Invoke();
        //narrationTextBox.GetComponent<NarrationTextBoxMinigameTwo>().PictureOne();

        //pictureUI.SetActive(true);
        //descriptionOne.SetActive(true);
        //descriptionTwo.SetActive(false);
        //descriptionThree.SetActive(false);
        //descriptionFour.SetActive(false);
        //descriptionFive.SetActive(false);
        //rightRay.SetActive(true);
        //pauseMenu.tutorialActive = true;
    }
    //When location two is selected by camera
    public void LocationTwoPressed()
    {
        changeColors(location2);
        removeText(b2);
        locationTwoFinished = true;

        LocationTwoFired.Invoke();
        //narrationTextBox.GetComponent<NarrationTextBoxMinigameTwo>().PictureTwo();

        //pictureUI.SetActive(true);
        //descriptionOne.SetActive(false);
        //descriptionTwo.SetActive(true);
        //descriptionThree.SetActive(false);
        //descriptionFour.SetActive(false);
        //descriptionFive.SetActive(false);
        //rightRay.SetActive(true);
        //pauseMenu.tutorialActive = true;
    }
    //When location three is selected by camera
    public void LocationThreePressed()
    {
        changeColors(location3);
        removeText(b3);
        locationThreeFinished = true;

        LocationThreeFired.Invoke();
        //narrationTextBox.GetComponent<NarrationTextBoxMinigameTwo>().PictureThree();
        
        //pictureUI.SetActive(true);
        //descriptionOne.SetActive(false);
        //descriptionTwo.SetActive(false);
        //descriptionThree.SetActive(true);
        //descriptionFour.SetActive(false);
        //descriptionFive.SetActive(false);
        //rightRay.SetActive(true);
        //pauseMenu.tutorialActive = true;
    }
    //When location four is selected by camera
    public void LocationFourPressed()
    {
        changeColors(location4);
        removeText(b4);
        locationFourFinished = true;

        LocationFourFired.Invoke();
        //narrationTextBox.GetComponent<NarrationTextBoxMinigameTwo>().PictureFour();

        //pictureUI.SetActive(true);
        //descriptionOne.SetActive(false);
        //descriptionTwo.SetActive(false);
        //descriptionThree.SetActive(false);
        //descriptionFour.SetActive(true);
        //descriptionFive.SetActive(false);
        //rightRay.SetActive(true);
        //pauseMenu.tutorialActive = true;
    }

    //When location five is selected by camera
    public void LocationFivePressed()
    {
        changeColors(location5);
        removeText(b5);
        locationFiveFinished = true;

        LocationFiveFired.Invoke();
        //narrationTextBox.GetComponent<NarrationTextBoxMinigameTwo>().PictureFive();

        //pictureUI.SetActive(true);
        //descriptionOne.SetActive(false);
        //descriptionTwo.SetActive(false);
        //descriptionThree.SetActive(false);
        //descriptionFour.SetActive(false);
        //descriptionFive.SetActive(true);
        //rightRay.SetActive(true);
        //pauseMenu.tutorialActive = true;
    }

    // Display the menu based on if the points have all been selected
    public void DisplayWinMenu()
    {
            winMenu.SetActive(true);
    }
    
    // Transition when player presses Y at end of level
    public void YButtonPressed(InputAction.CallbackContext context)
    {
        if(locationOneFinished == true && locationTwoFinished == true && locationThreeFinished == true && locationFourFinished == true && locationFiveFinished == true)
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
            SceneManager.LoadScene("GravityScienceRoom");
        }

        yield return null;
    }
}
