// Created 11/24/23 - Noah Pfeffer
// This script controls the rotation of the Psyche asteroid during the camera minigame (old script for when button was used to rotate)
// Modification History:
/* 
    11/24/23 - Noah Pfeffer
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotatePsyche : MonoBehaviour
{
    //How much Psyche will rotate
    public float rotationSpeed;
    //The game object for Psyche
    public GameObject psyche;

    // Rotate Psyche when the A button is pressed on the right controller
    public void AButtonpressed(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Rotate();
        }
    }
    //Rotates Psyche depending on rotationSpeed
    public void Rotate()
    {
        psyche.transform.Rotate(new Vector3(0, rotationSpeed, 0));
    }
}
