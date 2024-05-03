// Created 11/8/23 - Noah Pfeffer
// Functionality of snapzone
// Modification History:
/* 
    11/8/23 - Noah Pfeffer

    1/25/24 - Cameron Schmidt

    2/2/24 - Cameron Schmidt
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapZone : MonoBehaviour
{
    private Transform _meshTrans;
    private Transform _popUpTrans;
    private bool _selected = false;
    //Socket for snap zone that is being referred to
    private XRSocketInteractor _xrSocketInteractor;
    //Whether the snap zone has selected something or not
    public bool hasSelected = false;

    void Start()
    {
        _meshTrans = transform.GetChild(0);
        _popUpTrans = transform.GetChild(1);
        _popUpTrans.gameObject.SetActive(false);

        _xrSocketInteractor = GetComponent<XRSocketInteractor>();
    }
    void Update()
    {
        if(_selected) return;
        //Check to see if something has been selected by snap zone
        hasSelected = _xrSocketInteractor.hasSelection;
        
        if(hasSelected){
            _meshTrans.gameObject.SetActive(false);
            _popUpTrans.gameObject.SetActive(false);
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            if(gameObject.TryGetComponent<BoxCollider>(out BoxCollider bCollider)){
                bCollider.isTrigger = false;
            }

            if(gameObject.TryGetComponent<CapsuleCollider>(out CapsuleCollider cCollider)){
                cCollider.isTrigger = false;
            }

            if(gameObject.TryGetComponent<MeshCollider>(out MeshCollider mCollider)){
                mCollider.isTrigger = false;
            }
            _selected = true;
        }
    }

    public void EnablePopUp()
    {
        if(_selected) return;
        _popUpTrans.gameObject.SetActive(true);
    }

    public void DisablePopUp()
    {
        if(_selected) return;
        _popUpTrans.gameObject.SetActive(false);
    }
}

