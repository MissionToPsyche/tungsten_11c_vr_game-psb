// Created 11/1/23 - Peyton O'Boyle
// Narration text box functionality for normal mode minigame one
// Modification History:
/* 
    11/1/23 - Peyton O'Boyle

    2/3/24 - Cameron Schmidt

    3/16/24 - Cameron Schmidt
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.VisualScripting;
using UnityEngine.Animations;

public class NarrationTextBox : MonoBehaviour
{
    

    //The text to be displayed by the text box
    private string[] _narrationTextLines;
    
    //The audio clips of the narration for each line
    private AudioClip[] _narrationAudioClips;

    private AudioSource _narrationAudioSource;

    // private bool _defaultInteractorSet;

    // private IXRSelectInteractor _initialInteractor;
    private TextMeshPro _textBox;
    private Transform _narrationTextBoxTransformList;

    private Queue<IEnumerator> _coroutineQueue = new Queue<IEnumerator>();
    private bool _canRunNext;

    //Below is the init of each public array of audio clips for a specific topic

    //public AudioClip[] magnetometerAudioClips;

    public AudioClip[] introductionAudioClips;
    public AudioClip[] onIntroductionRoomDoneAudioClips;
    public AudioClip[] onIntroductionRoomExitAudioClips;
    public AudioClip[] onSupportBoomRoomOneDoneAudioClips;
    public AudioClip[] onSupportBoomRoomOneExitAudioClips;
    public AudioClip[] onPhysGunRoomDoneAudioClips;
    public AudioClip[] onPhysGunRoomExitedAudioClips;
    public AudioClip[] onSupportBoomRoomTwoDoneAudioClips;
    public AudioClip[] onSupportBoomRoomTwoExitAudioClips;
    public AudioClip[] onMultispectralImagerSnapped;
    public AudioClip[] onSupportBoomsAttached;
    public Transform _narrationTextBoxTrans;
    

    void Start()
    {
        _textBox = _narrationTextBoxTrans.GetChild(0).GetChild(0).GetComponent<TextMeshPro>();
        _narrationTextBoxTransformList = _narrationTextBoxTrans.parent.GetChild(1);
        //_defaultInteractorSet = false;

        StartCoroutine(NarrationCoroutineCoordinator());
        OnStartFunc();
    }

    IEnumerator NarrationCoroutineCoordinator()
    {
        while(true)
        {
            while(_coroutineQueue.Count > 0)
            {
                yield return StartCoroutine(_coroutineQueue.Dequeue());
            }
            yield return null;
        }
    }

    //Displays lines of text one by one and plays the associated narration.
    IEnumerator StartTextBox()
    {
        _textBox.transform.parent.gameObject.SetActive(true);

        _narrationAudioSource = GetComponent<AudioSource>();
        _canRunNext = false;
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

        _textBox.transform.parent.gameObject.SetActive(false);

        _canRunNext = true;
    }

    bool startCheck = false;
    public void OnStartFunc()
    {
        if(startCheck) return;
        startCheck = true;

        _coroutineQueue.Enqueue(OnMinigameOneStart());
    }

    public IEnumerator OnMinigameOneStart()
    {
        yield return new WaitForSeconds(1);

        _narrationTextBoxTrans.SetPositionAndRotation(_narrationTextBoxTransformList.GetChild(0).position,
                                                        _narrationTextBoxTransformList.GetChild(0).rotation);

        _narrationTextLines = new string[] {
            "Hello there! And welcome to the NASA Psyche Satellite Construction Center!",
            "Today, we have one simple task for you, to complete the construction of the NASA Pysche Satellite",
            "The NASA Psyche satellite is a spacecraft meant to explore the metallic asteroid 16 Psyche, believed to be the exposed iron-nickel core of a protoplanet.",
            "Its mission is to study the asteroid's composition, magnetic field, and surface features, aiming to provide insight, into planetary formation.",
            "In front of you is a tutorial for how to move around and interact with the facility, press down on the red button to get started. If you’d like to skip the tutorial, press down on the blue button.",
        };

        _narrationAudioClips = introductionAudioClips;

        StartCoroutine(StartTextBox());
    }

    bool introDoneCheck = false;
    public void OnIntroductionDoneText()
    {
        if(introDoneCheck) return;
        introDoneCheck = true;

        _coroutineQueue.Enqueue(OnIntroductionDone());
    }

    public IEnumerator OnIntroductionDone()
    {
        while(!_canRunNext)
        {
            yield return null;
        }

        _narrationTextBoxTrans.SetPositionAndRotation(_narrationTextBoxTransformList.GetChild(1).position,
                                                        _narrationTextBoxTransformList.GetChild(1).rotation);

        _narrationTextLines = new string[] {
            "Now, if you could please enter the next room, we can get started.",
        };

        _narrationAudioClips = onIntroductionRoomDoneAudioClips;

        StartCoroutine(StartTextBox());
    }

    bool introExitCheck = false;
    public void OnIntroductionExitText()
    {
        if(introExitCheck) return;
        introExitCheck = true;

        _coroutineQueue.Enqueue(OnIntroductionRoomExited());
    }

    public IEnumerator OnIntroductionRoomExited()
    {
        while(!_canRunNext)
        {
            yield return null;
        }

        _narrationTextBoxTrans.SetPositionAndRotation(_narrationTextBoxTransformList.GetChild(2).position,
                                                        _narrationTextBoxTransformList.GetChild(2).rotation);

        _narrationTextLines = new string[] {
            "On top of the pedestal to your right you will see a High-Sensitivity Magnetic Field Sensor, which is apart of the Psyche Magnetometer.",
            "The Psyche Magnetometer is designed to detect and measure the remanent magnetic field of the 16 Psyche asteroid.",
            "It is composed of two identical high-sensitivity magnetic field sensors located at the middle and outer end of the support boom you see in front of you.",
            "Please attach the Magnetic Field Sensor to the support boom, by grabbing it, walking over to the support, and placing it in the indicated spot."
        };

        _narrationAudioClips = onIntroductionRoomExitAudioClips;

        StartCoroutine(StartTextBox());
    }

    bool SupportOneDoneCheck = false;
    public void OnSupportOneDoneText()
    {
        if(SupportOneDoneCheck) return;
        SupportOneDoneCheck = true;

        _coroutineQueue.Enqueue(OnSupportBoomRoomOneDone());
    }

    public IEnumerator OnSupportBoomRoomOneDone() //Called when you finish the first support boom room
    {
        while(!_canRunNext)
        {
            yield return null;
        }

        _narrationTextBoxTrans.SetPositionAndRotation(_narrationTextBoxTransformList.GetChild(3).position,
                                                        _narrationTextBoxTransformList.GetChild(3).rotation);

        _narrationTextLines = new string[] {
            "Good job! Please enter the next room through the door on the left side of the room",
        };

        _narrationAudioClips = onSupportBoomRoomOneDoneAudioClips;

        StartCoroutine(StartTextBox());
    }

    bool SupportOneExitCheck = false;
    public void OnSupportOneExitText()
    {
        if(SupportOneExitCheck) return;
        SupportOneExitCheck = true;

        _coroutineQueue.Enqueue(OnSupportBoomRoomOneExited());
    }
    public IEnumerator OnSupportBoomRoomOneExited() // Called upon the first support boom room door closing, so it's essentially the start of the second room
    {
        while(!_canRunNext)
        {
            yield return null;
        }

        _narrationTextBoxTrans.SetPositionAndRotation(_narrationTextBoxTransformList.GetChild(4).position,
                                                        _narrationTextBoxTransformList.GetChild(4).rotation);

        _narrationTextLines = new string[] {
            "In front of you is a special, imaginary tool, called the Power Glove.",
            "We will use the Power Glove to help us build the rest of the Psyche Satellite.",
            "To use the Power Glove, grab it with your left or right hand, to attach it to you hand",
            "Then press and hold the trigger to activate the ray and then hold grip to grab an object. Try grabbing the object in front of you"
        };

        

        _narrationAudioClips = onSupportBoomRoomOneExitAudioClips;
        
        StartCoroutine(StartTextBox());
    }

    bool PhysRoomDoneCheck = false;
    public void PhysRoomDoneText()
    {
        if(PhysRoomDoneCheck) return;
        PhysRoomDoneCheck = true;

        _coroutineQueue.Enqueue(OnPhysGunRoomDone());
    }
    public IEnumerator OnPhysGunRoomDone() //Called when you finish the Phys Gun room
    {
        while(!_canRunNext)
        {
            yield return null;
        }

        _narrationTextBoxTrans.SetPositionAndRotation(_narrationTextBoxTransformList.GetChild(7).position,
                                                        _narrationTextBoxTransformList.GetChild(7).rotation);

        _narrationTextLines = new string[] {
            "You can also press the two buttons on your controller to move the object you've just selected forwards and backwards",
            "We can use the Power Glove we can use it to lift heavier instruments.",
        };

        _narrationAudioClips = onPhysGunRoomDoneAudioClips;
        
        StartCoroutine(StartTextBox());
        
    }

    bool PhysRoomExitCheck = false;
    public void PhysRoomExitText()
    {
        if(PhysRoomExitCheck) return;
        PhysRoomExitCheck = true;

        _coroutineQueue.Enqueue(OnPhysGunRoomExited());
    }

    public IEnumerator OnPhysGunRoomExited() // Called upon the Phys Gun room door closing and entering the second support room
    {
        while(!_canRunNext)
        {
            yield return null;
        }

        _narrationTextBoxTrans.SetPositionAndRotation(_narrationTextBoxTransformList.GetChild(8).position,
                                                        _narrationTextBoxTransformList.GetChild(8).rotation);

        _narrationTextLines = new string[] {
            "On your right you should see two parts for the Gamma-Ray and Neutron Spectrometer.",
            "The Gamma-Ray and Neutron Spectrometer is an instrument that will detect, measure, and map the 16 Psyche asteroids elemental composition.",
            "Try using the Power Glove to attach each part to the support boom in front of you, by grabbing the part with the trigger and releasing the part in the indicated spot.",
        };

        _narrationAudioClips = onPhysGunRoomExitedAudioClips;

        StartCoroutine(StartTextBox());
    }

    bool SupportTwoDoneCheck = false;
    public void OnSupportTwoDoneText()
    {
        if(SupportTwoDoneCheck) return;
        SupportTwoDoneCheck = true;

        _coroutineQueue.Enqueue(OnSupportBoomRoomTwoDone());
    }

    public IEnumerator OnSupportBoomRoomTwoDone() //Called when you finish the second support boom room
    {
        while(!_canRunNext)
        {
            yield return null;
        }

        _narrationTextBoxTrans.SetPositionAndRotation(_narrationTextBoxTransformList.GetChild(5).position,
                                                        _narrationTextBoxTransformList.GetChild(5).rotation);

        _narrationTextLines = new string[] {
            "Nice work! Please continue to the final construction room through the door on your left.",
        };

        _narrationAudioClips = onSupportBoomRoomTwoDoneAudioClips;

        StartCoroutine(StartTextBox());
    }

    bool SupportTwoExitCheck = false;
    public void OnSupportTwoExitText()
    {
        if(SupportTwoExitCheck) return;
        SupportTwoExitCheck = true;

        _coroutineQueue.Enqueue(OnSupportBoomRoomTwoExited());
    }

    public IEnumerator OnSupportBoomRoomTwoExited() // Called upon the second support boom room door closing, so it's essentially the start of the hanger
    {
        while(!_canRunNext)
        {
            yield return null;
        }

        _narrationTextBoxTrans.SetPositionAndRotation(_narrationTextBoxTransformList.GetChild(6).position,
                                                        _narrationTextBoxTransformList.GetChild(6).rotation);

        _narrationTextLines = new string[] {
            "Welcome to the final construction room, this is where we will complete the assembly of the Psyche Satellite.",
            "On top of the crate to your right is the Psyche Multispectral Imager.",
            "The Multispectral Imager provides high-resolution images using filters to discriminate between the 16 Psyche asteroid's metallic and silicate constituents.",
            "The instrument consists of a pair of identical cameras designed to acquire geological, compositional, and topographical data.",
            "Use the Power Glove to attach the multispectral imager to the Psyche Satellite in front of you.",
        };

        _narrationAudioClips = onSupportBoomRoomTwoExitAudioClips;
        
        StartCoroutine(StartTextBox());
    }

    bool MultispectralImagerSnappedCheck = false;
    public void MultispectralImagerSnappedText()
    {
        if(MultispectralImagerSnappedCheck) return;
        MultispectralImagerSnappedCheck = true;

        _coroutineQueue.Enqueue(OnMultispectralImagerSnapped());
    }

    public IEnumerator OnMultispectralImagerSnapped()
    {
        while(!_canRunNext)
        {
            yield return null;
        }

        _narrationTextBoxTrans.SetPositionAndRotation(_narrationTextBoxTransformList.GetChild(6).position,
                                                        _narrationTextBoxTransformList.GetChild(6).rotation);

        _narrationTextLines = new string[] {
            "I see the two support booms that you constructed earlier have finally arrived!",
            "Use the Power Glove to attach the two support booms to the indicated spots in front of you",
        };

        _narrationAudioClips = onMultispectralImagerSnapped;
        
        StartCoroutine(StartTextBox());
    }

    bool SupportBoomsAttachedCheck = false;
    public void SupportBoomsAttachedText()
    {
        if(SupportBoomsAttachedCheck) return;
        SupportBoomsAttachedCheck = true;

        _coroutineQueue.Enqueue(OnSupportBoomsAttached());
    }

    public IEnumerator OnSupportBoomsAttached()
    {
        while(!_canRunNext)
        {
            yield return null;
        }

        _narrationTextBoxTrans.SetPositionAndRotation(_narrationTextBoxTransformList.GetChild(6).position,
                                                        _narrationTextBoxTransformList.GetChild(6).rotation);

        _narrationTextLines = new string[] {
            "Congratulations! You have completed the assembly of the Psyche Satellite.",
        };

        _narrationAudioClips = onSupportBoomsAttached;
        
        StartCoroutine(StartTextBox());
    }
}