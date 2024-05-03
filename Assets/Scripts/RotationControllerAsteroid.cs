// Created 2/4/24 Jack Brand
// This script controls functionality for rotating the asteroid in minigame 2
// Modification History:
/* 
    2/4/24 - Jack Brand
    2/8/24 - Jack Brand
 */

//using Codice.CM.Common.Tree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RotationControllerAsteroid : MonoBehaviour
{
    
    [SerializeField] private GameObject _astModel;
    
    private Vector3 origin;
    
    // Start is called before the first frame update
    void Start()
    {
        //_astModel = transform.parent.GetChild(0).gameObject;
        origin = transform.position;
        


    }

    // Update is called once per frame
    void Update()
    {
        _astModel.transform.rotation = transform.rotation;

    }

    public void RetainPosition(SelectExitEventArgs arg0)
    {
        transform.position = origin;
    }
}
