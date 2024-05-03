// Created 3/2/24 - Cameron Schmidt
// Driver for intro room in normal mode minigame one.
// Modification History:
/* 
    3/2/24 - Cameron Schmidt

    3/26/24 - Cameron Schmidt
 */
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class IntroductionRoomDriver : MonoBehaviour
{
    private Transform _environmentObjects;
    private Transform _grabInteractableTransform; // The transform of the grab interactable, which is a ball
    private Transform _targetPositionsTransform;
    private Transform _blastDoorTransform; // The transform of the door behind the monitor
    private Vector3 _blastDoorOpenTargetPos; // The position the door should move to when it's to be opened
    private Vector3 _blastDoorCloseTargetPos; // The position the door should move to when it's to be closed
    private Transform _monitorTransform; // The transform of the monitor
    private Transform _objectPodium;
    private Transform _nextButtonPodium;
    private Transform _skipButtonPodium;
    private Vector3 _nextButtonTargetPos;
    private Vector3 _skipButtonTargetPos;
    private Vector3 _objectPodiumTargetPos;
    private Vector3 _monitorTargetPos; // The position the monitor should move to when it's to be moved upwards, out of the way
    private MeshRenderer _monitorScreen; // The meshrenderer on the monitor, that contains the "slide" material that needs to be swapped with the next "slide" upon the button press
    // private MeshCollider _nextButtonCollider;
    // private BoxCollider _nextButtonTrigger;
    // private BoxCollider _skipButtonTrigger;
    private bool _redButtonPushed; // The bool that's triggered upon the red button being pressed
    private bool _tutorialDone;
    public bool _tutorialSkipped;
    //private bool _exitedSupportRoom;
    private bool _objectGrabbed;
    private bool _menuOpened;
    private bool _menuOpenedDuringSlide;
    private bool _canPushRedButton;
    private int _slideCounter;
    private float _blastDoorSpeed = 3f;
    private float _monitorSpeed = 2f;
    private float _podiumSpeed = 4f;

    [SerializeField]
    private AudioClip _doorOpenFX;
    [SerializeField]
    private AudioClip _doorCloseFX;
    [SerializeField]
    private AudioClip _monitorUpFX;


    public UnityEvent introductionDone;
    public UnityEvent introductionRoomExited;

    public Material[] introSlides;
    public InputActionReference a_InputRef;
    //public InputActionReference menu_InputRef;


    private void Start()
    {
        _canPushRedButton = true;
        _menuOpenedDuringSlide = false;
        _slideCounter = 0;

        _environmentObjects = transform.GetChild(0).GetChild(0);
        _grabInteractableTransform = transform.GetChild(1);
        _targetPositionsTransform = transform.GetChild(2);

        _blastDoorTransform = _environmentObjects.GetChild(0);
        _blastDoorOpenTargetPos = _targetPositionsTransform.GetChild(0).position;
        _blastDoorCloseTargetPos = _blastDoorTransform.position;

        _monitorTransform = _environmentObjects.GetChild(1);
        _monitorTargetPos = _targetPositionsTransform.GetChild(1).position;
        _monitorScreen = _monitorTransform.GetChild(0).GetComponent<MeshRenderer>();

        _nextButtonPodium = _environmentObjects.GetChild(4);
        _skipButtonPodium = _environmentObjects.GetChild(5);
        _objectPodium = _environmentObjects.GetChild(6);

        _nextButtonTargetPos = _targetPositionsTransform.GetChild(2).position;
        _skipButtonTargetPos = _targetPositionsTransform.GetChild(3).position;
        _objectPodiumTargetPos = _targetPositionsTransform.GetChild(4).position;
        



        //_nextButtonTrigger = _environmentObjects.GetChild(4).GetChild(0).GetComponent<BoxCollider>();
        //_skipButtonTrigger = _environmentObjects.GetChild(4).GetChild(1).GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if(_tutorialDone) return;

        _menuOpened = a_InputRef.action.triggered;

        if(_redButtonPushed && _canPushRedButton){
            _redButtonPushed = false;
            StartCoroutine(SlideEvents());
        }

        if(_slideCounter == introSlides.Length-1 || _tutorialSkipped){
            _tutorialDone = true;
            StartCoroutine(CloseMonitorOpenBlastDoor());
        }


    }

    public void NextButtonPressed()
    {
        _redButtonPushed = true;
        //StartCoroutine(SlideEvents());
    }

    public void SkipButtonPressed()
    {
        _tutorialSkipped = true;
        //StartCoroutine(CloseMonitorOpenBlastDoor());
    }

    private IEnumerator SlideEvents()
    {
        _canPushRedButton = false;
        _slideCounter++;

        if(_slideCounter == 2)
        {
            _monitorScreen.material = introSlides[_slideCounter];
            _grabInteractableTransform.GetChild(0).gameObject.SetActive(true);
            while(!_objectGrabbed){
                yield return null;
            }
            _slideCounter++;
            _monitorScreen.material = introSlides[_slideCounter];
        }
        else if(_slideCounter == 4)
        {
            _monitorScreen.material = introSlides[_slideCounter];
            while(!_menuOpenedDuringSlide)
            {
                if(_menuOpened)
                {
                    _menuOpenedDuringSlide = true;
                }
                yield return null;
            }
            //_slideCounter++;
            //_monitorScreen.material = introSlides[_slideCounter];
            _tutorialSkipped = true;
        }
        else{
            _monitorScreen.material = introSlides[_slideCounter];
        }
        _canPushRedButton = true;
    }

    private IEnumerator CloseMonitorOpenBlastDoor()
    {   // Lower the three podiums
        SoundFXManager.instance.PlaySoundFXClip(_doorOpenFX, _skipButtonPodium, 0.25f, Vector3.Distance(_skipButtonPodium.position, _skipButtonTargetPos)/_podiumSpeed);
        while (_skipButtonPodium.position != _skipButtonTargetPos)
        {
            _skipButtonPodium.position = Vector3.MoveTowards(_skipButtonPodium.position, _skipButtonTargetPos, _podiumSpeed * Time.deltaTime);
            yield return null;
        }

        SoundFXManager.instance.PlaySoundFXClip(_doorOpenFX, _nextButtonPodium, 0.25f, Vector3.Distance(_nextButtonPodium.position, _nextButtonTargetPos)/_podiumSpeed);
        while (_nextButtonPodium.position != _nextButtonTargetPos)
        {
            _nextButtonPodium.position = Vector3.MoveTowards(_nextButtonPodium.position, _nextButtonTargetPos, _podiumSpeed * Time.deltaTime);
            yield return null;
        }

        SoundFXManager.instance.PlaySoundFXClip(_doorOpenFX, _objectPodium, 0.25f, Vector3.Distance(_objectPodium.position, _objectPodiumTargetPos)/_podiumSpeed);
        while (_objectPodium.position != _objectPodiumTargetPos)
        {
            _objectPodium.position = Vector3.MoveTowards(_objectPodium.position, _objectPodiumTargetPos, _podiumSpeed * Time.deltaTime);
            yield return null;
        }

        // Raise the monitor
        SoundFXManager.instance.PlaySoundFXClip(_monitorUpFX, _monitorTransform, 0.5f, Vector3.Distance(_monitorTransform.position, _monitorTargetPos)/_monitorSpeed);
        while (_monitorTransform.position != _monitorTargetPos)
        {
            _monitorTransform.position = Vector3.MoveTowards(_monitorTransform.position, _monitorTargetPos, _monitorSpeed * Time.deltaTime);
            yield return null;
        }
        
        // Open the door
        SoundFXManager.instance.PlaySoundFXClip(_doorOpenFX, _blastDoorTransform, 0.5f, Vector3.Distance(_blastDoorTransform.position, _blastDoorOpenTargetPos)/_blastDoorSpeed);
        while (_blastDoorTransform.position != _blastDoorOpenTargetPos)
        {
            _blastDoorTransform.position = Vector3.MoveTowards(_blastDoorTransform.position, _blastDoorOpenTargetPos, _blastDoorSpeed * Time.deltaTime);
            yield return null;
        }

        introductionDone.Invoke();
    }

    private IEnumerator CloseBlastDoor()
    {
        // Close the door
        SoundFXManager.instance.PlaySoundFXClip(_doorCloseFX, _blastDoorTransform, 0.5f, Vector3.Distance(_blastDoorTransform.position, _blastDoorCloseTargetPos)/_blastDoorSpeed);
        while (_blastDoorTransform.position != _blastDoorCloseTargetPos)
        {
            _blastDoorTransform.position = Vector3.MoveTowards(_blastDoorTransform.position, _blastDoorCloseTargetPos, _blastDoorSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void BallGrabbed()
    {
        _objectGrabbed = true;
    }

    public void OnTriggerExit(Collider collider)
    {
        if(collider.tag == "Player"){
            Destroy(GetComponent<BoxCollider>());
            StartCoroutine(CloseBlastDoor());
            introductionRoomExited.Invoke();
        }
    }
}