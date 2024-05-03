/* Minigame3Tests_P.cs
 * @author Jack Brand - created 4/11/24
 * Modified 4/13/24 - Jack Brand
 *  > This file contains the tests for Minigame 3 functionality
 *  > All Tests contained in this file are a part of Test Suite 8 - Minigame 3 Tests
 * 
 */

using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.XR.Content.Interaction;

public class Minigame3Tests_P
{


    [SetUp]
    public void setup()
    {
        SceneManager.LoadScene("GravityScienceRoom");
    }

    // Test Case 039 - Launch Button Test
    // Unit Test
    [UnityTest]
    public IEnumerator TC039()
    {
        GameObject sat = GameObject.Find("Satellite");
        Assert.IsNotNull(sat, "satellite not found");

        OrbitPsyche OP = sat.GetComponent<OrbitPsyche>();
        Assert.IsNotNull(OP, "OrbitPsyche not found");

        Rigidbody satRB = sat.GetComponent<Rigidbody>();
        Assert.IsNotNull(satRB, "satRB not found");

        GameObject btn = GameObject.Find("Button");
        Assert.IsNotNull(btn, "Button not found");

        LaunchButton LB = btn.GetComponent<LaunchButton>();
        Assert.IsNotNull(LB, "LaunchButton not found");

        LB.onPress.Invoke();

        // Waits for function to process and for satellite to begin motion
        yield return new WaitForSeconds(1);


        //Ensures that the satellite object has begun motion when the button is pressed
        Assert.AreNotEqual(satRB.velocity, Vector3.zero);
        
        yield return null;
    }


    // Test Case 041 - Colored Trail Existence Test
    // Unit Test
    [UnityTest]
    public IEnumerator TC041() 
    {
        GameObject sat = GameObject.Find("Satellite");
        Assert.IsNotNull(sat, "satellite not found");

        OrbitPsyche OP = sat.GetComponent<OrbitPsyche>();
        Assert.IsNotNull(OP, "OrbitPsyche not found");

        OP.Launch();

        // Waits for function to process and for satellite to begin motion
        yield return new WaitForSeconds(1);

        // Ensures that the satellite object has a child with a TrailRenderer Component
        TrailRenderer trail = sat.GetComponentInChildren<TrailRenderer>();
        Assert.IsNotNull(trail, "trail null");

        yield return null;
    }

    // Test Case 042 - Gravity Equation Existence Test
    // Unit Test
    [UnityTest]
    public IEnumerator TC042()
    {
        GameObject equation = GameObject.Find("Equation");
        Assert.IsNotNull(equation, "equation not found");

        GameObject ME = GameObject.Find("Main Equation");
        GameObject Force = GameObject.Find("Force");
        GameObject High = GameObject.Find("High");
        GameObject Low = GameObject.Find("Low");

        Assert.IsNotNull(ME, "ME Null");
        Assert.IsNotNull(Force, "Force Null");
        Assert.IsNotNull(High, "High Null");
        Assert.IsNotNull(Low, "Low Null");

        yield return null;
    }
}
