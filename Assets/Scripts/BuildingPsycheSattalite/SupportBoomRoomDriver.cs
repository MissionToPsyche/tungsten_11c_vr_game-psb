// Created 3/7/24 - Cameron Schmidt
// Driver for support boom room
// Modification History:
/* 
    3/7/24 - Cameron Schmidt

    3/26/24 - Cameron Schmidt
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class SupportBoomRoomDriver : MonoBehaviour
{
    private Transform _grabInteractableTransform;
    private Transform _targetPositionsTransform;
    private Transform _shutterTransform;
    private Transform _environmentObjectsTransform;
    private Transform _supportBoomTransform;
    private Transform _blastDoorTransform;
    private Transform _shutterOpenTargetTransform;
    private Transform _shutterCloseTargetTransform;
    private Vector3 _supportBoomTargetPos;
    private Vector3 _blastDoorOpenTargetPos;
    private Vector3 _blastDoorCloseTargetPos;
    private Vector3 _supportBoomHangerPos;
    private SupportBoom _supportBoomDriver;
    private bool _supportRoomComplete;
    [SerializeField]
    private AudioClip _doorOpenFX;
    [SerializeField]
    private AudioClip _doorCloseFX;
    [SerializeField]
    private AudioClip _craneFX;
    [SerializeField]
    private AudioClip _craneReleaseFX;
    [SerializeField]
    private AudioClip _boomFallFX;
    [SerializeField]
    private AudioClip _shutterOpenFX;
    [SerializeField]
    private AudioClip _shutterCloseFX;
    [SerializeField]
    private AudioClip _boomCrashFX;
    private float _blastDoorSpeed = 3f;
    private float _shutterSpeed = 120f;
    private float _craneSpeed = 2f;

    public UnityEvent onSupportBoomRoomDone;
    public UnityEvent onSupportBoomRoomExited;

    void Start()
    {
        _supportRoomComplete = false;

        _environmentObjectsTransform = transform.GetChild(0).GetChild(0);
        _grabInteractableTransform = transform.GetChild(1);
        _targetPositionsTransform = transform.GetChild(2);

        _supportBoomTransform = _grabInteractableTransform.GetChild(0);
        _supportBoomDriver = _supportBoomTransform.GetComponent<SupportBoom>();
        _supportBoomTargetPos = _targetPositionsTransform.GetChild(1).position;
        _supportBoomHangerPos = _targetPositionsTransform.GetChild(3).position;

        _shutterTransform = _environmentObjectsTransform.GetChild(0);
        _shutterOpenTargetTransform = _targetPositionsTransform.GetChild(0);
        _shutterCloseTargetTransform = _targetPositionsTransform.GetChild(4);

        _blastDoorTransform = _environmentObjectsTransform.GetChild(1);
        _blastDoorOpenTargetPos = _targetPositionsTransform.GetChild(2).position;
        _blastDoorCloseTargetPos = _blastDoorTransform.position;
    }

    void Update()
    {
        if(_supportRoomComplete) return;

        if(_supportBoomDriver.GetAllSnapZonesConnected()){
            _supportRoomComplete = true;
            StartCoroutine(OpenShuttersDropSupportBoom());
        }
    }

    private IEnumerator OpenShuttersDropSupportBoom()
    {
        // Open shutters over a period of time
        SoundFXManager.instance.PlaySoundFXClip(_shutterOpenFX, _shutterTransform, 0.5f, _shutterOpenFX.length);
        while (_shutterTransform.rotation != _shutterOpenTargetTransform.rotation)
        {
            _shutterTransform.rotation = Quaternion.RotateTowards(_shutterTransform.rotation, _shutterOpenTargetTransform.rotation, _shutterSpeed * Time.deltaTime);
            yield return null;
        }

        // Move support boom backwards
        SoundFXManager.instance.PlaySoundFXClip(_craneFX, _supportBoomTransform, 0.5f, Vector3.Distance(_supportBoomTransform.position, _supportBoomTargetPos)/_craneSpeed);
        while (_supportBoomTransform.position != _supportBoomTargetPos)
        {
            _supportBoomTransform.position = Vector3.MoveTowards(_supportBoomTransform.position, _supportBoomTargetPos, _craneSpeed * Time.deltaTime);
            yield return null;
        }

        // Unfreeze position of the support boom rigid body so it falls
        SoundFXManager.instance.PlaySoundFXClip(_craneReleaseFX, _supportBoomTransform, 0.5f, _craneReleaseFX.length);
        SoundFXManager.instance.PlaySoundFXClip(_boomFallFX, _supportBoomTransform, 0.5f, _boomFallFX.length);
        _supportBoomDriver.GetSupportBoomRigidBody().constraints = ~RigidbodyConstraints.FreezeAll;
        _supportBoomDriver.GetXRGrabInteractable().enabled = true;

        yield return new WaitForSeconds(2);

        SoundFXManager.instance.PlaySoundFXClip(_boomCrashFX, _shutterTransform, 0.2f, _boomCrashFX.length);

        // Close shutters over a period of time
        SoundFXManager.instance.PlaySoundFXClip(_shutterCloseFX, _shutterTransform, 0.5f, _shutterCloseFX.length);
        while (_shutterTransform.rotation != _shutterCloseTargetTransform.rotation)
        {
            _shutterTransform.rotation = Quaternion.RotateTowards(_shutterTransform.rotation, _shutterCloseTargetTransform.rotation, _shutterSpeed  * Time.deltaTime);
            yield return null;
        }

        StartCoroutine(OpenBlastDoor());
    }

    private IEnumerator OpenBlastDoor()
    {
        SoundFXManager.instance.PlaySoundFXClip(_doorOpenFX, _blastDoorTransform, 0.5f, Vector3.Distance(_blastDoorTransform.position, _blastDoorOpenTargetPos)/_blastDoorSpeed);
        while (_blastDoorTransform.position != _blastDoorOpenTargetPos)
        {
            _blastDoorTransform.position = Vector3.MoveTowards(_blastDoorTransform.position, _blastDoorOpenTargetPos, _blastDoorSpeed * Time.deltaTime);
            yield return null;
        }

        onSupportBoomRoomDone.Invoke();
    }

    private IEnumerator CloseBlastDoor()
    {
        SoundFXManager.instance.PlaySoundFXClip(_doorCloseFX, _blastDoorTransform, 0.5f, Vector3.Distance(_blastDoorTransform.position, _blastDoorCloseTargetPos)/_blastDoorSpeed);
        while (_blastDoorTransform.position != _blastDoorCloseTargetPos)
        {
            _blastDoorTransform.position = Vector3.MoveTowards(_blastDoorTransform.position, _blastDoorCloseTargetPos, _blastDoorSpeed * Time.deltaTime);
            yield return null;
        }

        onSupportBoomRoomExited.Invoke();
    }

    public void OnTriggerExit(Collider collider)
    {
        if(collider.tag == "Player"){
            Destroy(GetComponent<BoxCollider>());
            StartCoroutine(CloseBlastDoor());
        }
    }

    public void SendSupportBoomPartToDropPos()
    {
        _supportBoomTransform.gameObject.layer = 11;
        _supportBoomTransform.position = _supportBoomHangerPos;
        _supportBoomTransform.GetComponent<GrabablePart>().SetOrigin(_supportBoomHangerPos);
    }
}
