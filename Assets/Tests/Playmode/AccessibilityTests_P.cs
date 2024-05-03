// Created 4/12/24 - Jack brand
// Testing the peripheral vignette
// Modification History:
/* 
    4/12/24 - Jack Brand
 */
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class AccessibilityTests_P
{
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    // Setup function for tests 
    // loads into Main scene (construction minigame - full version)
    [SetUp]
    public void setup()
    {
        SceneManager.LoadScene("Main");
    }

    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    // Test Case 052 - Peripheral Vignette Visiblity Test
    // Unit Test
    [UnityTest]
    public IEnumerator TC052()
    {
        // Find Object with Peripheral Vignette Component, then save that component & assert not null
        PeripheralVignette Vignette = Object.FindObjectOfType<PeripheralVignette>().GetComponent<PeripheralVignette>();
        Assert.IsNotNull(Vignette, "Vignette Not Found");

        // Makes sure vignette is properly disabled
        Assert.IsFalse(Vignette.vignette.activeSelf, "vignette active when should be disabled");

        Global.vignette = "On";

        // wait one second for Update function to take effect
        yield return new WaitForSeconds(1);

        // Assert that vignette is now active
        Assert.IsTrue(Vignette.vignette.activeSelf, "Vignette not active");

        yield return null;
    }
    // ------------------------------------------------------------------------------------------------------------------------------------------------------


}
