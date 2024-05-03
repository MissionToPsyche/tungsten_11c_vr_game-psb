// Created 4/5/24 - Jack brand
// Testing narration text boxes in minigame one
// Modification History:
/* 
    4/5/24 - Jack Brand

    4/12/24 - Jack Brand

    4/13/24 - Jack Brand
 */
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
using TMPro;


public class TextNarrationTests_P
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
    // Test Case 054 - Playing Audio Clip test
    // Unit Test
    [UnityTest]
    public IEnumerator TC054()
    {
        // finds audio source component attached to XR rig and asserts not null
        AudioSource AS = Object.FindObjectOfType<NarrationTextBox>().GetComponent<AudioSource>();
        Assert.IsNotNull(AS, "Audio source not found");

        // waits for fade to finish, and for audio clip to start playing
        yield return new WaitForSeconds(6);

        // asserts that audio clip is playing 
        Assert.IsTrue(AS.isPlaying, "Audio source not playing");


        yield return null;
    }
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    // Test Case 055 - 
    [UnityTest]
    public IEnumerator TC055()
    {
        // finds text box game object
        GameObject textBox = GameObject.Find("NarrationTextBox");
        Assert.IsNotNull(textBox, "text box is null");
                
        // gets the text attached to the textbox
        TextMeshPro text = textBox.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>();
        Assert.IsNotNull(text, "text null");

        // waits for designated time before text starts appearing
        yield return new WaitForSeconds(6);

        // asserts that the text box is visible and that the text contained in the text box is not empty
        Assert.IsTrue(textBox.activeSelf, "text box not active");
        Assert.IsFalse(text.text.Equals(""));

        yield return null;
    }
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
}
