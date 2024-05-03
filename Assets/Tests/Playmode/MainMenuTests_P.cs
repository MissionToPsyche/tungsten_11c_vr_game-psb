// Created 1/21/24 - Jack brand
// Testing main menu functionality
// Modification History:
/* 
    1/21/24 - Jack Brand
 */
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class MainMenuTests_P
{
    // Setup
    [SetUp]
    public void setup()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    [UnityTest]
    public IEnumerator TC020()
    {

        GameObject StartButton = GameObject.Find("StartButton");
        Assert.IsNotNull(StartButton, "Start Button not found");

        Button StartButtonReference = StartButton.GetComponent<Button>();
        Assert.IsNotNull(StartButtonReference, "Button component not found");
        StartButtonReference.onClick.Invoke();

        GameObject TimerSettings = GameObject.Find("TimerSettings");
        Assert.IsNotNull(TimerSettings, "timerSettings null");
        Button TSButtonRef = TimerSettings.GetComponent<Button>();
        Assert.IsNotNull(TSButtonRef, "no button component on TS");
        TSButtonRef.onClick.Invoke();

        GameObject numpad = GameObject.Find("NumPad");
        Assert.IsNotNull(numpad, "numpad not found");
        Assert.AreEqual(true, numpad.activeSelf, "numpad is inactive");
       
        yield return null;
    }


}
