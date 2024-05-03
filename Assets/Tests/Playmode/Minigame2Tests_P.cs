// Created 1/21/24 - Jack brand
// Testing components in minigame two
// Modification History:
/* 
    1/21/24 - Jack Brand

    2/17/24 - Jack Brand
 */
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class Minigame2Tests_P
{
    [SetUp]
    public void setup()
    {
        // Loading second scene
        SceneManager.LoadScene("Second");
    }


    [UnityTest]
    public IEnumerator TC030()
    {
        // Finds Game Object Asteroid Controller
        GameObject astController = GameObject.Find("Ast Controller");
        Assert.IsNotNull(astController, "asteroid controller not found");

        // Finds Game Object Main Asteroid
        GameObject largeAst = GameObject.Find("Main Asteroid");
        Assert.IsNotNull(largeAst, "large ast not found");

        // Changes rotation of the Asteroid Controller GO
        astController.transform.rotation.Set(1, 2, 3, 4);

        // Checks that the GO Main Asteroid's rotation has changed to reflect the new rotation of Asteroid Controller
        Assert.AreEqual(astController.transform.rotation, largeAst.transform.rotation, "rotations not equal");

        yield return null;
    }

    [UnityTest]
    public IEnumerator TC031()
    {
        // Finds Game Object Location 1
        GameObject loc1 = GameObject.Find("Locationone");
        Assert.IsNotNull(loc1, "location 1 not found");

        // Finds Game Object Location One Button
        GameObject btn1 = GameObject.Find("LocationOneButton");
        Assert.IsNotNull(btn1, "Button not found");
        // Getting button component from GO
        Button button = btn1.GetComponent<Button>();
        Assert.IsNotNull(button, "button component not found");
        
        // Invoking button
        button.onClick.Invoke();

        // Getting material component of Location 1 GO
        Material loc1_mat = loc1.GetComponent<MeshRenderer>().material;
        Assert.IsNotNull(loc1_mat, "location 1 material is null");

        // Checking that the material of the object has changed to the correct new material
        Assert.AreEqual("m_Selected (Instance)", loc1_mat.name, "materials do not match");

        yield return null;
    }
}
