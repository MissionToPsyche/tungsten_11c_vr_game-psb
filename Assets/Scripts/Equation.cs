// Created 2/1/24 - Peyton O'Boyle
// Handles functionality of equation
// Modification History:
/* 
    2/1/24 - Peyton O'Boyle

    2/11/24 - Peyton O'Boyle
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Equation : MonoBehaviour
{
    public GameObject equation;
    public GameObject forceDisplay;

    public GameObject high;
    public GameObject low;

    private double gravitationalConstant = 6.674e-11f;

    //Masses in kg
    public float satelliteMass;
    private float asteroidMass;

    private float distance;

    private Color[,] colors;
    private int colorIndex;

    enum DisplayMode {Variables, Numbers};
    private DisplayMode displayMode;

    void Start()
    {
        setColors();
        colorIndex = 0;
        displayMode = DisplayMode.Variables;
    }

    // Update is called once per frame
    void Update()
    {
        //Display the gravity equation
        if (displayMode == DisplayMode.Variables)
        {
            //Change the equation itself
            equation.GetComponent<TextMeshPro>().text = "<color=#FFFFFF><size=9>G * m<sub>sat</sub> * m<sub>ast</sub></size><size=12><line-height=10%>\n______</line-height>\n</size>     <size=9>d<sup>2</sup></size></color>";

            //Change the force side of the equation
            forceDisplay.GetComponent<TextMeshPro>().text = "<color=#FFFFFF><size=10>F</size> <size=8>=</size>";
        }

        else
        {
            //Change the equation itself
            string satelliteMassColor = "<color=#"+ ColorUtility.ToHtmlStringRGB(Color.Lerp(colors[colorIndex, 1], colors[colorIndex, 0], (asteroidMass - 2.287e+19f + 7e+18f) / (2 * 7e+18f))) + ">";
            string distanceColor = "<color=#" + ColorUtility.ToHtmlStringRGB(Color.Lerp(colors[colorIndex, 0], colors[colorIndex, 1], distance / 1000000f)) + ">";

            equation.GetComponent<TextMeshPro>().text = "<color=#FFFFFF><size=9>G * " + satelliteMass + " * " + satelliteMassColor + asteroidMass + "</color></size><size=12><line-height=10%>\n__________</line-height>\n</size>      <size=9>" + distanceColor + distance + "<sup>2</sup></size></color></color>";

            //Change the force side of the equation
            float force = Mathf.Round((float)(gravitationalConstant * (satelliteMass * asteroidMass) / Mathf.Pow(distance, 2)));
            string forceColor = "<color=#" + ColorUtility.ToHtmlStringRGB(Color.Lerp(colors[colorIndex, 1], colors[colorIndex, 0], Mathf.Round((float)(gravitationalConstant * (satelliteMass * asteroidMass) / Mathf.Pow(distance, 2)) / 300))) + ">";

            forceDisplay.GetComponent<TextMeshPro>().text = "<color=#FFFFFF><size=10>" + forceColor + force + "</color></size> <size=8>=</size></color>";
        }

        //Display current colorscolors[colorIndex, 0]
        high.GetComponent<TextMeshPro>().text = "<color=#" + ColorUtility.ToHtmlStringRGB(colors[colorIndex, 0]) + ">Strongly affecting gravity</color>";
        low.GetComponent<TextMeshPro>().text = "<color=#" + ColorUtility.ToHtmlStringRGB(colors[colorIndex, 1]) + ">Weakly affecting gravity</color>";
    }

    public void setAsteroidMass(float asteroidMass)
    {
        this.asteroidMass = asteroidMass;
    }

    public void setDistance(float distance)
    {
        this.distance = distance;
    }

    //Send in 0 to set to variables and 1 to set to numbers
    public void setDisplayMode(int displayMode)
    {
        if (displayMode == 0)
        {
            this.displayMode = DisplayMode.Variables;
        }
        else if (displayMode == 1)
        {
            this.displayMode = DisplayMode.Numbers;
        }
    }

    private void setColors()
    {
        //Create the list
        colors = new Color[5, 2];

        //Set the colors of the colors list
        colors[0,0] = Color.red;
        colors[0,1] = Color.green;
        colors[1,0] = Color.blue;
        colors[1,1] = Color.yellow;
        colors[2,0] = Color.blue;
        colors[2,1] = new Color(255f / 255f, 120f / 255f, 0f / 255f, 1f);
        colors[3,0] = new Color(255f / 255f, 0f / 255f, 239f / 255f, 1f);
        colors[3,1] = Color.yellow;
        colors[4,0] = Color.red;
        colors[4,1] = Color.blue;
    }

    public void changeColor()
    {
        if (colorIndex >= 4)
        {
            colorIndex = 0;
        }

        else
        {
            colorIndex++;
        }
    }
}
