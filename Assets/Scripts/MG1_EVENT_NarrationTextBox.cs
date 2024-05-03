// Created 4/4/24 - Cameron Schmidt
// Narration text box for minigame one event mode
// Modification History:
/* 
    4/4/24 - Cameron Schmidt
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.VisualScripting;
using UnityEngine.Animations;

public class MG1_EVENT_NarrationTextBox : MonoBehaviour
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
    [SerializeField]
    private Transform _narrationTextBoxTrans;
    [SerializeField]
    private AudioClip[] _startFacts;
    [SerializeField]
    private AudioClip[] _magnetometerFacts;
    [SerializeField]
    private AudioClip[] _gammarayFacts;
    [SerializeField]
    private AudioClip[] _neutronFacts;
    [SerializeField]
    private AudioClip[] _multispectralImagerFacts;
    [SerializeField]
    private AudioClip[] _supportBoomOneFacts;
    [SerializeField]
    private AudioClip[] _supportBoomTwoFacts;
    

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

        _coroutineQueue.Enqueue(OnStart());
    }
    private IEnumerator OnStart()
    {
        yield return new WaitForSeconds(5);

        _narrationTextBoxTrans.SetPositionAndRotation(_narrationTextBoxTransformList.GetChild(0).position,
                                                        _narrationTextBoxTransformList.GetChild(0).rotation);

        _narrationTextLines = new string[] {
            "Hello there! And welcome to the NASA Psyche Satellite Construction Center!",
            "Today, we have one simple task for you, to complete the construction of the NASA Psyche Satellite",
            "The NASA Psyche satellite is a spacecraft meant to explore the metallic asteroid 16 Psyche, believed to be the exposed iron-nickel core of a protoplanet.",
            "Its mission is to study the asteroid's composition, magnetic field, and surface features, aiming to provide insight, into planetary formation.",
            "To your left and right are rooms for the different parts that make up the Psyche Satellite, feel free to start with whatever room you'd like!",
        };

        _narrationAudioClips = _startFacts;

        StartCoroutine(StartTextBox());
    }
    bool magnetometerCheck = false;
    public void MagnetometerText()
    {
        if(magnetometerCheck) return;
        magnetometerCheck = true;

        _coroutineQueue.Enqueue(OnMagnetometerSelected());
    }
    private IEnumerator OnMagnetometerSelected()
    {
        while(!_canRunNext)
        {
            yield return null;
        }
        _narrationTextBoxTrans.SetPositionAndRotation(_narrationTextBoxTransformList.GetChild(1).position,
                                                        _narrationTextBoxTransformList.GetChild(1).rotation);

        _narrationTextLines = new string[] {
            "The Psyche Magnetometer is designed to detect and measure the remanent magnetic field of the 16 Psyche asteroid.",
            "It is composed of two identical high-sensitivity magnetic field sensors, that are attached to a support boom.",
        };

        _narrationAudioClips = _magnetometerFacts;

        StartCoroutine(StartTextBox());
    }

    bool neutronCheck = false;
    public void NeutronText()
    {
        if(neutronCheck) return;
        neutronCheck = true;

        _coroutineQueue.Enqueue(OnNeutronSelected());
    }

    private IEnumerator OnNeutronSelected()
    {
        //StopCoroutine(StartTextBox());
        while(!_canRunNext)
        {
            yield return null;
        }
        _narrationTextBoxTrans.SetPositionAndRotation(_narrationTextBoxTransformList.GetChild(2).position,
                                                        _narrationTextBoxTransformList.GetChild(2).rotation);

        _narrationTextLines = new string[] {
            "This is the part of the Gamma Ray and Neutron Spectrometer, that will detect neutrons",
            "Emitted neutrons and gamma rays can be detected by the spectrometer and analyzed by scientists, who can match their properties to those emitted by known elements to determine what Psyche is made of.",
        };

        _narrationAudioClips = _neutronFacts;

        StartCoroutine(StartTextBox());
    }

    bool gammarayCheck = false;
    public void GammarayText()
    {
        if(gammarayCheck) return;
        gammarayCheck = true;

        _coroutineQueue.Enqueue(OnGammaraySelected());
    }

    private IEnumerator OnGammaraySelected()
    {
        while(!_canRunNext)
        {
            yield return null;
        }

        _narrationTextBoxTrans.SetPositionAndRotation(_narrationTextBoxTransformList.GetChild(3).position,
                                                        _narrationTextBoxTransformList.GetChild(3).rotation);

        _narrationTextLines = new string[] {
            "This is the part of the Gamma Ray and Neutron Spectrometer, that will detect gamma gays",
            "As cosmic rays and high energy particles impact the surface of Psyche, the elements that make up the surface material absorb the energy and in response emit neutrons and gamma rays of varying energy levels.",
        };

        _narrationAudioClips = _gammarayFacts;

        StartCoroutine(StartTextBox());
    }

    bool multispectralImagerCheck = false;
    public void MultispectralImagerText()
    {
        if(multispectralImagerCheck) return;
        multispectralImagerCheck = true;

        _coroutineQueue.Enqueue(OnMultispectralImagerSelected());
    }
    private IEnumerator OnMultispectralImagerSelected() //Called when you finish the first support boom room
    {
        while(!_canRunNext)
        {
            yield return null;
        }

        _narrationTextBoxTrans.SetPositionAndRotation(_narrationTextBoxTransformList.GetChild(4).position,
                                                        _narrationTextBoxTransformList.GetChild(4).rotation);

        _narrationTextLines = new string[] {
            "The Multispectral Imager provides high-resolution images using filters to discriminate between the 16 Psyche asteroid's metallic and silicate constituents.",
            "The instrument consists of a pair of identical cameras designed to acquire geological, compositional, and topographical data.",
        };

        _narrationAudioClips = _multispectralImagerFacts;

        StartCoroutine(StartTextBox());
    }

    bool supportOneCheck = false;
    public void SupportOneText()
    {
        if(supportOneCheck) return;
        supportOneCheck = true;

        _coroutineQueue.Enqueue(OnSupportBoomOneSelected());
    }
    private IEnumerator OnSupportBoomOneSelected() // Called upon the first support boom room door closing, so it's essentially the start of the second room
    {
        while(!_canRunNext)
        {
            yield return null;
        }

        _narrationTextBoxTrans.SetPositionAndRotation(_narrationTextBoxTransformList.GetChild(5).position,
                                                        _narrationTextBoxTransformList.GetChild(5).rotation);

        _narrationTextLines = new string[] {
            "The Psyche Mission is NASA’s first mission to study an asteroid that has more metal than rock or ice.",
            "Psyche was discovered in 1852 by Italian astronomer Annibale de Gasparis.",
            "It’s named for the goddess of the soul in ancient Greek mythology, often depicted as a butterfly-winged female figure."
        };

        _narrationAudioClips = _supportBoomOneFacts;

        StartCoroutine(StartTextBox());
    }

    bool supportTwoCheck = false;
    public void SupportTwoText()
    {
        if(supportTwoCheck) return;
        supportTwoCheck = true;

        _coroutineQueue.Enqueue(OnSupportBoomTwoSelected());
    }
    private IEnumerator OnSupportBoomTwoSelected() //Called when you finish the Phys Gun room
    {
        while(!_canRunNext)
        {
            yield return null;
        }

        _narrationTextBoxTrans.SetPositionAndRotation(_narrationTextBoxTransformList.GetChild(6).position,
                                                        _narrationTextBoxTransformList.GetChild(6).rotation);

        _narrationTextLines = new string[] {
            "Scientists think Psyche may consist of significant amounts of metal from the core of a planetesimal, one of the building blocks of our solar system. ",
            "The asteroid is most likely a survivor of multiple violent planetary collisions, common when the solar system was forming. ",
        };

        _narrationAudioClips = _supportBoomTwoFacts;

        StartCoroutine(StartTextBox());
        
    }
}