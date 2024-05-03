// Created 2/1/24 - Cameron Schmidt
// Functionality of support boom and their assembly
// Modification History:
/* 
    2/1/24 - Cameron Schmidt

    3/5/24 - Cameron Schmidt
 */
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SupportBoom : MonoBehaviour
{
    private Rigidbody _supportBoomRigidBody;

    private XRGrabInteractable _xRGrabInteractable;

    private List<SnapZone> _snapZones; 

    private bool _allSnapZonesConnected;

    void Start()
    {
        _supportBoomRigidBody = GetComponent<Rigidbody>();
        _xRGrabInteractable = GetComponent<XRGrabInteractable>();
        _xRGrabInteractable.enabled = false;
        _allSnapZonesConnected = false;
        _snapZones = new List<SnapZone>();

        for(int i = 0; i < transform.GetChild(0).childCount; i++){
            if(transform.GetChild(0).GetChild(i).TryGetComponent<SnapZone>(out SnapZone snapZone)){
                _snapZones.Add(snapZone);
            }
        }
        
    }

    void Update()
    {
        if(_allSnapZonesConnected) return;

        _allSnapZonesConnected = AllSnapZonesConnected();

        // if(_allSnapZonesConnected){
        //     _supportBoomRigidBody.constraints = ~RigidbodyConstraints.FreezeAll;
        //     _xRGrabInteractable.enabled = true;
        // }
    }

    private bool AllSnapZonesConnected(){
        foreach(SnapZone snapZone in _snapZones){
            if(!snapZone.hasSelected){
                return false;
            }
        }
        return true;
    }

    public Rigidbody GetSupportBoomRigidBody(){
        return _supportBoomRigidBody;
    }

    public XRGrabInteractable GetXRGrabInteractable(){
        return _xRGrabInteractable;
    }

    public bool GetAllSnapZonesConnected(){
        return _allSnapZonesConnected;
    }
}
