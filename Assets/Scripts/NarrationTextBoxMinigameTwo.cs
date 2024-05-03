// Created 3/19/24 - Noah Pfeffer
// Narration text box functionality for minigame two
// Modification History:
/* 
    3/19/24 - Noah Pfeffer

    3/26/24 - Noah Pfeffer

 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.VisualScripting;
using UnityEngine.Animations;

public class NarrationTextBoxMinigameTwo : MonoBehaviour
{
    

    //The text to be displayed by the text box
    private string[] _narrationTextLines;
    
    //The audio clips of the narration for each line
    private AudioClip[] _narrationAudioClips;

    private AudioSource _narrationAudioSource;

    private bool _defaultInteractorSet;

    private IXRSelectInteractor _initialInteractor;
    private TextMeshPro _textBox;

    //Below is the init of each public array of audio clips for a specific topic
    [SerializeField]
    private AudioClip[] introductionAudioClips;
    [SerializeField]
    private AudioClip[] pictureOneAudioClips;
    [SerializeField]
    private AudioClip[] pictureTwoAudioClips;
    [SerializeField]
    private AudioClip[] pictureThreeAudioClips;
    [SerializeField]
    private AudioClip[] pictureFourAudioClips;
    [SerializeField]
    private AudioClip[] pictureFiveAudioClips;
    [SerializeField]
    private Transform _narrationTextBoxTrans;
    
    public bool tookPicture = false;

    private Queue<IEnumerator> _coroutineQueue = new Queue<IEnumerator>();
    private bool _canRunNext;

    void Start()
    {
        _textBox = _narrationTextBoxTrans.GetChild(0).GetChild(0).GetComponent<TextMeshPro>();
        //_narrationTextBoxTransformList = _narrationTextBoxTrans.parent.GetChild(1);
        _defaultInteractorSet = false;
        _narrationAudioSource = GetComponent<AudioSource>();

        StartCoroutine(OnMinigameTwoStart());
    }

    // IEnumerator NarrationCoroutineCoordinator()
    // {
    //     while(true)
    //     {
    //         while(_coroutineQueue.Count > 0)
    //         {
    //             yield return StartCoroutine(_coroutineQueue.Dequeue());
    //         }
    //         yield return null;
    //     }
    // }

    //Displays lines of text one by one and plays the associated narration.
    IEnumerator StartTextBox()
    {
        yield return new WaitForSeconds(1);
        
        _narrationTextBoxTrans.gameObject.SetActive(true);


        for (int i = 0; i < _narrationTextLines.Length; i++)
        {
            //Display the line in the text
            _textBox.text = _narrationTextLines[i];

            //Play the audio associated with the line
            _narrationAudioSource.clip = _narrationAudioClips[i];
            _narrationAudioSource.Play();

            //Wait for audio to finish
            
            yield return new WaitForSeconds(_narrationAudioClips[i].length);
        }

        yield return new WaitForSeconds(1);

        _narrationTextBoxTrans.gameObject.SetActive(false);
    }

    public IEnumerator OnMinigameTwoStart()
    {
        yield return new WaitForSeconds(4);


        if(tookPicture == false){

        
            _narrationTextLines = new string[] {
                "Welcome to the inside of the Psyche Satellite’s Multispectral Imager, we’re going to be using it to take pictures of some points of interest on the Psyche asteroid.",
                "Make sure to keep in mind that in real life you wouldn’t be able to shrink down and go inside of the Multispectral Imager.",
                "This is just meant to show how the multispectral imager will be used during the mission.",
                "The real multispectral imager uses a combination of filters to capture specific wavelengths of light. With this, it can produce high resolution images and collect information not visible to the human eye.",
                "To get started, grab each side of the camera by gripping it with the triggers on both hand controllers. Then, aim the reticle at the glowing areas on the asteroids and press A.",
                "There may be points of interest that you can not see in the asteroid’s current position, so a mini asteroid is provided to act as a controller to control the big asteroid. Grab it with either hand and maneuver it as needed."
            };

            _narrationAudioClips = introductionAudioClips;

            StopCoroutine(StartTextBox());
            StartCoroutine(StartTextBox());
        }
    }

    public void PictureOne()
    {
        tookPicture = true;

        //_narrationTextBoxTrans.SetPositionAndRotation(_narrationTextBoxTransformList.GetChild(1).position,
        //                                                _narrationTextBoxTransformList.GetChild(1).rotation);

        _narrationTextLines = new string[] {
            "Psyche is predicted to be a metal-rich asteroid with materials such as iron and nickel.",
        };

        _narrationAudioClips = pictureOneAudioClips;

        StopCoroutine(StartTextBox());
        StartCoroutine(StartTextBox());
    }

    public void PictureTwo()
    {
        tookPicture = true;

        //_narrationTextBoxTrans.SetPositionAndRotation(_narrationTextBoxTransformList.GetChild(1).position,
        //                                                _narrationTextBoxTransformList.GetChild(1).rotation);

        _narrationTextLines = new string[] {
            "Psyche’s orbit is located between Mars and Jupiter.",
        };

        _narrationAudioClips = pictureTwoAudioClips;

        StopCoroutine(StartTextBox());
        StartCoroutine(StartTextBox());
    }

    public void PictureThree()
    {
        tookPicture = true;

        //_narrationTextBoxTrans.SetPositionAndRotation(_narrationTextBoxTransformList.GetChild(1).position,
        //                                                _narrationTextBoxTransformList.GetChild(1).rotation);

        _narrationTextLines = new string[] {
            "Psyche is thought to have a radius of 70.215 miles.",
        };

        _narrationAudioClips = pictureThreeAudioClips;

        StopCoroutine(StartTextBox());
        StartCoroutine(StartTextBox());
    }

    public void PictureFour()
    {
        tookPicture = true;

        //_narrationTextBoxTrans.SetPositionAndRotation(_narrationTextBoxTransformList.GetChild(1).position,
        //                                                _narrationTextBoxTransformList.GetChild(1).rotation);

        _narrationTextLines = new string[] {
            "Psyche has been observed to have an ellipsoid shape with some notable deficits in mass and shape.",
        };

        _narrationAudioClips = pictureFourAudioClips;

        StopCoroutine(StartTextBox());
        StartCoroutine(StartTextBox());
    }

    public void PictureFive()
    {
        tookPicture = true;

        //_narrationTextBoxTrans.SetPositionAndRotation(_narrationTextBoxTransformList.GetChild(1).position,
        //                                                _narrationTextBoxTransformList.GetChild(1).rotation);

        _narrationTextLines = new string[] {
            "Psyche was the 16th asteroid to be discovered.",
        };

        _narrationAudioClips = pictureFiveAudioClips;

        StopCoroutine(StartTextBox());
        StartCoroutine(StartTextBox());
    }
}
