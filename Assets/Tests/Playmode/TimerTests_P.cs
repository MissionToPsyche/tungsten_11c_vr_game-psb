// Created 1/28/24 - Jack brand
// Testing timer decreasing
// Modification History:
/* 
    1/28/24 - Jack brand
 */
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TimerTests_P
{
    

    [SetUp]
    public void setup()
    {
        SceneManager.LoadScene("Main");


    }

    [UnityTest]
    public IEnumerator TC025()
    {
        yield return new WaitForSeconds(3);

        Assert.AreEqual(147f, Mathf.Round(Global.timer), "timer not decreasing properly");
        

        yield return null;
    }
}
