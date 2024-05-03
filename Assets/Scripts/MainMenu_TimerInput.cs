// Created 11/29/23 - Jack Brand
// Controls functionality of the input field in the main menu - setting the game timer
// Modification History:
/* 
    11/29/23 - Jack Brand
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MainMenu_TimerInput : MonoBehaviour
{
    private InputField input;
    [SerializeField]
    private UnityEngine.UI.Button b1;
    [SerializeField]
    private UnityEngine.UI.Button b2;
    [SerializeField]
    private UnityEngine.UI.Button b3;
    [SerializeField]
    private UnityEngine.UI.Button b4;
    [SerializeField]
    private UnityEngine.UI.Button b5;
    [SerializeField]
    private UnityEngine.UI.Button b6;
    [SerializeField]
    private UnityEngine.UI.Button b7;
    [SerializeField]
    private UnityEngine.UI.Button b8;
    [SerializeField]
    private UnityEngine.UI.Button b9;
    [SerializeField]
    private UnityEngine.UI.Button bdot;
    [SerializeField]
    private UnityEngine.UI.Button b0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMainTimerFromInput()
    {
        // Parse the user input from the text field and set it in the main timer
        if (float.TryParse(input.text, out float newTime))
        {
            Global.timer = newTime * 60f;
        }
        else
        {
            Debug.LogWarning("Invalid input. Please enter a valid number.");
        }
    }
}
