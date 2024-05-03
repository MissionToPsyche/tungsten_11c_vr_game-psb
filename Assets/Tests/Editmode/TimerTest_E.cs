// Created 1/28/24 - Jack brand
// Tests related to game timer that are not based on direct user interaction
// Modification History:
/* 
    1/28/24 - Jack Brand
 */
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

public class TimerTest_E
{

    [Test]
    public void TC020()
    {
        GameObject NumPad = new GameObject();
        NumPad.SetActive(false);

        Button StartButton = new Button();
        StartButton.clicked += () => { NumPad.SetActive(true); };



    }

    [Test]
    public void TC022()
    {
        GameObject time = new GameObject();
        GameTimer timer = time.AddComponent<GameTimer>();
        timer.ResetTimer();

        Assert.AreEqual(2.5f * 60f, Global.timer);
    }


    [Test]
    public void TC024()
    {
        // Creates Game Object Timer
        GameObject timer = new GameObject();
        // Adds GameTimer component to Timer GO
        GameTimer timerGT = timer.AddComponent<GameTimer>();
        var timeToSet = 10f * 60f;

        // Sets Global variable to 10 minutes
        Global.timerInit = timeToSet;
        // Calls ResetTimer functionf of timerGT (timer component of GO)
        timerGT.ResetTimer();

        // Checks that the Global timer time has been correctly set based on the timerInit value
        Assert.AreEqual(timeToSet, Global.timer);
    }

    

}