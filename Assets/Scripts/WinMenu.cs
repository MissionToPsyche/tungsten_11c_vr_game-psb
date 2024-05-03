// Created 11/15/23 - Noah Pfeffer
// This script does checking to see if snapzones for satellite minigame are attached to an object (old script from when minigame one was still one empty room)
// Modification History:
/* 
    11/15/23 - Noah Pfeffer

    11/24/23 - Noah Pfeffer
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class WinMenu : MonoBehaviour
{
    //Snap zones
    public GameObject partOne;
    public GameObject partTwo;
    //Menu displayed when minigame completed
    public GameObject winMenu;
    //Boolean to keep track of when game is finished
    public bool finished = false;
    

    // Start is called before the first frame update
    void Start()
    {
        finished = false;
    }

    // Gets info to check if player has completed the minigame
    void FixedUpdate()
    {
        SnapZone snapOne = partOne.GetComponent<SnapZone>();
        SnapZone snapTwo = partTwo.GetComponent<SnapZone>();
        DisplayWinMenu(snapOne, snapTwo);
    }
    // Display the menu based on if the objects are in the snap locations
    public void DisplayWinMenu(SnapZone snapOne, SnapZone snapTwo)
    {
        if(finished == false){
            if(snapOne.hasSelected && snapTwo.hasSelected)
            {
                
                //Open menu
                winMenu.SetActive(true);

                finished = true;
                
            }
            else
            {
                winMenu.SetActive(false);
            }
        }
        else
        {
            //Open menu
            winMenu.SetActive(true);
        }
    }
    
    // Transition when player presses Y at end of level
    public void YButtonPressed(InputAction.CallbackContext context)
    {
        if(finished == true){
            if(context.performed)
            {
                if(Global.full == false)
                {
                    SceneManager.LoadScene("MainMenu");
                }
                else
                {
                    SceneManager.LoadScene("Second");
                }
            }
        }
    }
}
