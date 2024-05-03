// Created 3/7/24 - Cameron Schmidt
// Driver for final room in normal mode minigame one. Handles assembly of the satellite and end of game flow.
// Modification History:
/* 
    3/7/24 - Cameron Schmidt

    3/26/24 - Cameron Schmidt

    4/13/24 - Noah Pfeffer - Added some functionality for ending transition
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalConstructionRoomDriver : MonoBehaviour
{
    private Transform _environmentTransform;
    private Transform _grabInteractableTransform;
    private Transform _targetPositionsTransform;
    private Transform _incompletePsycheTransform;
    private Transform _snapZonePartTransforms;
    private Transform _blastDoorTransform;
    private Transform _multispectralImagerSnapTransform;
    private Transform _supportBoomOneSnapTransform;
    private Transform _supportBoomTwoSnapTransform;

    public UnityEvent onMultispectralImagerSnapped; // Narration text box talk about support booms
    public UnityEvent onSupportBoomsAttached; // Narration text box talk about array panels
    public UnityEvent onArrayPanelsAttached; // Talk about mission overview and how your done with the minigame

    private bool finished;

    //Message displayed to user when game is completed
    public GameObject winMenu;

    public GameObject XRRig;

    void Start()
    {
        finished = false;
        _environmentTransform = transform.GetChild(0);

        _grabInteractableTransform = transform.GetChild(1);
        _incompletePsycheTransform = _grabInteractableTransform.GetChild(0);
        _snapZonePartTransforms = _incompletePsycheTransform.GetChild(0);
        _multispectralImagerSnapTransform = _snapZonePartTransforms.GetChild(0);
        _supportBoomOneSnapTransform = _snapZonePartTransforms.GetChild(1);
        _supportBoomTwoSnapTransform = _snapZonePartTransforms.GetChild(2);
        //_arrayPanelOneSnapTransform = _snapZonePartTransforms.GetChild(3);
        //_arrayPanelTwoSnapTransform = _snapZonePartTransforms.GetChild(4);
        
        _targetPositionsTransform = transform.GetChild(2);
        // _stageTwoTargetPos = _targetPositionsTransform.GetChild(0).position;
        // //_supportBoomOneDropPos = _targetPositionsTransform.GetChild(1).position;
        // //_supportBoomTwoDropPos = _targetPositionsTransform.GetChild(2).position;

        // _finalConstructionRoomComplete = false;

        // _movingToPoint = false;
        
        // _allSnapZonesConnected = false;
        // _snapZones = new List<SnapZone>();

        // foreach(Transform snapZone in _snapZonePartTransforms)
        // {
        //     _snapZones.Add(snapZone.GetComponent<SnapZone>());
        // }


        StartCoroutine(MultispectralImagerSnap());
    }

    void Update()
    {
        // if(_allSnapZonesConnected && _movingToPoint){
        //     //_supportBoomRigidBody.constraints = ~RigidbodyConstraints.FreezeAll;
        //     //xRGrabInteractable.enabled = true;
        //     _movingToPoint = false;
        //     StartCoroutine(MoveToStageTwoPoint());
        // }

        // if(_finalConstructionRoomComplete) return;

        // if(_allSnapZonesConnected = AllSnapZonesConnected()){
        //     _finalConstructionRoomComplete = true;

        //     onArrayPanelsAttached.Invoke();
        // }
    }

    // private bool AllSnapZonesConnected(){
    //     foreach(SnapZone snapZone in _snapZones){
    //         if(!snapZone.hasSelected){
    //             return false;
    //         }
    //     }
    //     return true;
    // }

    IEnumerator MultispectralImagerSnap()
    {
        // Wait until multispectral imager connected
        while(!_multispectralImagerSnapTransform.GetComponent<SnapZone>().hasSelected)
        {
            yield return null;
        }

        onMultispectralImagerSnapped.Invoke();

        float elapsed = 0.0f;

        while(elapsed < 3.0f)
        {
            _incompletePsycheTransform.rotation = Quaternion.Slerp(_incompletePsycheTransform.rotation, _incompletePsycheTransform.rotation * Quaternion.Euler(Vector3.down * 90), Time.deltaTime/1.0f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(SupportBoomSnap());
    }

    IEnumerator SupportBoomSnap()
    {
        while(!_supportBoomOneSnapTransform.GetComponent<SnapZone>().hasSelected)
        {
            yield return null;
        }

        while(!_supportBoomTwoSnapTransform.GetComponent<SnapZone>().hasSelected)
        {
            yield return null;
        }

        onSupportBoomsAttached.Invoke();

        finished = true;

        DisplayWinMenu();

        //StartCoroutine(MoveToStageTwoPoint());
    }

    // Display the menu based if finished
    public void DisplayWinMenu()
    {
            winMenu.SetActive(true);
    }
    
    // Transition when player presses Y at end of level
    public void YButtonPressed(InputAction.CallbackContext context)
    {
        if(finished == true)
        {
            if(context.performed)
            {
                StartCoroutine(GoToNextScene());
            }
        }
    }

    public IEnumerator GoToNextScene()
    {
        XRRig.GetComponent<Movement>().OnExit();

        yield return new WaitForSeconds(1);

        if(Global.full == false)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene("Second");
        }

        yield return null;
    }

    // IEnumerator ArrayPanelSnap()
    // {
    //     StartCoroutine(MoveToStageTwoPoint());
    // }

    // IEnumerator MoveToStageTwoPoint() //Fix
    // {
    //     // Move towards the target point
    //     while (_incompletePsycheTransform.position != _stageTwoTargetPos)
    //     {
    //         _incompletePsycheTransform.position = Vector3.MoveTowards(_incompletePsycheTransform.position, _stageTwoTargetPos, 5f * Time.deltaTime);
    //         yield return null;
    //     }
        
    //     // _arrayPanelOneSnapTransform.gameObject.SetActive(true);
    //     // _arrayPanelTwoSnapTransform.gameObject.SetActive(true);

    //     onArrayPanelsAttached.Invoke();

    //     _finalConstructionRoomComplete = true;

    //     //_incompletePsycheTransform.rotation = _targetPositionsTransform.GetChild(0).rotation;
    // }
}
