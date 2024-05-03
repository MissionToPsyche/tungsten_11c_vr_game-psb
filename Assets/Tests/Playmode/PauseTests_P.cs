// Created 1/28/24 - Jack brand
// Testing timer in pause menu
// Modification History:
/* 
    1/28/24 - Jack Brand
 */
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class PauseTest
{
    // A Test behaves as an ordinary method
    [SetUp]
    public void setup()
    {
        SceneManager.LoadScene("Main");
    }



    [UnityTest]
    public IEnumerator TC018()
    {


        SceneManager.LoadScene (SceneManager.GetActiveScene().name) ;

        if (Global.timerInit == 0)
        {
            Assert.AreEqual(150f, Global.timer);
        }
        else
        {
            Assert.AreEqual(Global.timerInit, Global.timer);
        }
        

        yield return null;
    }
}
