// Created 3/2/24 - Cameron Schmidt
// Handles button press event for intro room in minigame one
// Modification History:
/* 
    3/2/24 - Cameron Schmidt
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonDriver : MonoBehaviour
{
    public UnityEvent buttonPressed;

    public void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Button"){
            buttonPressed.Invoke();
        }
    }
}
