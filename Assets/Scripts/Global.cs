// Created 11/24/23 - Noah Pfeffer
// Holds global information during gameplay
// Modification History:
/* 
    11/24/23 - Noah Pfeffer

    11/29/23 - Jack Brand

    1/30/24 - Noah Pfeffer

    3/26/24 - Noah Pfeffer

    4/23/24 - Noah Pfeffer
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public static class Global
{
    //If full game mode is selected or not
    public static bool full = true;

    //If in pause menu
    public static bool paused = false;

    //Timer variables
    public static float timerInit = 0;
    public static float timer = 0;
    public static string input = null;

    //Accessibility Variables
    public static string vignette = "Off";

    public static string movement = "Continuous";

    public static string turn = "Continuous";

    public static float volume = 0.5f;
}
