// Created 3/7/24 - Cameron Schmidt
// Driver for physics gun room in normal mode minigame one.
// Modification History:
/* 
    3/7/24 - Cameron Schmidt

    3/26/24 - Cameron Schmidt
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysGunRoomDriver : MonoBehaviour
{
    private Transform _environmentObjectsTransform;
    private Transform _targetPositionsTransform;
    private Transform _blastDoorTransform;
    private Vector3 _blastDoorOpenTargetPos;
    private Vector3 _blastDoorCloseTargetPos;
    private bool _physGunRoomComplete;
    private float _blastDoorSpeed = 3f;
    private bool _objectGrabbed;
    [SerializeField]
    private AudioClip _doorOpenFX;
    [SerializeField]
    private AudioClip _doorCloseFX;


    public UnityEvent onPhysGunRoomDone;
    public UnityEvent onPhysGunRoomExited;
    void Start()
    {
        _physGunRoomComplete = false;

        _environmentObjectsTransform = transform.GetChild(0).GetChild(0);
        _targetPositionsTransform = transform.GetChild(2);

        _blastDoorTransform = _environmentObjectsTransform.GetChild(0);
        _blastDoorOpenTargetPos = _targetPositionsTransform.GetChild(0).position;
        _blastDoorCloseTargetPos = _blastDoorTransform.position;

        StartCoroutine(WaitForObjectToBeGrabbed());
    }

    void Update()
    {
        if(_physGunRoomComplete) return;
    }

    private IEnumerator WaitForObjectToBeGrabbed()
    {
        while(!_objectGrabbed)
        {
            yield return null;
        }
        _physGunRoomComplete = true;

        StartCoroutine(OpenBlastDoor());
        
    }

    public void ObjectGrabbedWithPhysGun() //Event fired when phys gun "select enters" the object
    {
        _objectGrabbed = true;
    }

    private IEnumerator OpenBlastDoor()
    {
        SoundFXManager.instance.PlaySoundFXClip(_doorOpenFX, _blastDoorTransform, 0.5f, Vector3.Distance(_blastDoorTransform.position, _blastDoorOpenTargetPos)/_blastDoorSpeed);
        while (_blastDoorTransform.position != _blastDoorOpenTargetPos)
        {
            _blastDoorTransform.position = Vector3.MoveTowards(_blastDoorTransform.position, _blastDoorOpenTargetPos, _blastDoorSpeed * Time.deltaTime);
            yield return null;
        }

        onPhysGunRoomDone.Invoke();
    }

    private IEnumerator CloseBlastDoor()
    {
        SoundFXManager.instance.PlaySoundFXClip(_doorCloseFX, _blastDoorTransform, 0.5f, Vector3.Distance(_blastDoorTransform.position, _blastDoorOpenTargetPos)/_blastDoorSpeed);
        while (_blastDoorTransform.position != _blastDoorCloseTargetPos)
        {
            _blastDoorTransform.position = Vector3.MoveTowards(_blastDoorTransform.position, _blastDoorCloseTargetPos, _blastDoorSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if(collider.tag == "Player"){
            Destroy(GetComponent<BoxCollider>());
            StartCoroutine(CloseBlastDoor());
            onPhysGunRoomExited.Invoke();
        }
    }
}
