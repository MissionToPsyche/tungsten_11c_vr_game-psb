// Created 3/22/24 - Peyton O'Boyle
// Handles covers for buttons on console
// Modification History:
/* 
    3/22/24 - Peyton O'Boyle
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class ConsoleManager : MonoBehaviour
{
    //The button and slider game object
    public GameObject button;
    public GameObject slider;

    //The covers for both button and cover
    public GameObject buttonCover;
    public GameObject buttonCoverHinge;

    public GameObject sliderCover;
    public GameObject sliderCoverHinge;

    private bool openButtonCover;
    private bool openSliderCover;

    // Start is called before the first frame update
    void Start()
    {
        //Unactivate the button and slider
        button.GetComponent<LaunchButton>().enabled = false;
        slider.GetComponent<GravitySlider>().enabled = false;

        openButtonCover = false;
        openSliderCover = false;

        //UncoverButton();
        //UncoverSlider();
    }

    void Update()
    {
        if (openButtonCover && buttonCover.transform.localRotation.eulerAngles.z > 270)
        {
            Debug.Log(buttonCover.transform.localRotation.eulerAngles.z);
            buttonCover.transform.RotateAround(buttonCoverHinge.transform.position, new Vector3(1f, 0f, 0f), 20f * Time.deltaTime);
        }

        if (openSliderCover && sliderCover.transform.localRotation.eulerAngles.z > 270)
        {
            Debug.Log(sliderCover.transform.localRotation.eulerAngles.z);
            sliderCover.transform.RotateAround(sliderCoverHinge.transform.position, new Vector3(1f, 0f, 0f), 20f * Time.deltaTime);
        }
    }

    public void UncoverButton()
    {
        //Move the button cover off using hingejoint
        openButtonCover = true;

        //Activate the button
        button.GetComponent<LaunchButton>().enabled = true;
    }

    public void UncoverSlider()
    {
        //Move the slider cover off
        openSliderCover = true;

        //Activate the slider
        slider.GetComponent<GravitySlider>().enabled = true;
    }

    //To Do After:
    //Add Ending Narration
    //Add Controller Graph
    //Replace Asteroid and Satellitte models
    //Add Skybox
    //Dupe this into different environment
    //Add Camera Room

    //Feedback:
    //Make grab instructions bigger
    //Fix pressing button multiple times glitch
}
