// Created 3/31/24 - Cameron Schmidt
// Enables access to power glove/physics gun
// Modification History:
/* 
    3/31/24 - Cameron Schmidt

    4/1/24 - Cameron Schmidt
 */
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class TakePowerGlove : MonoBehaviour
{
    public void EnableRayUse(SelectEnterEventArgs arg0)
    {
        if(arg0.interactableObject.transform.CompareTag("PhysicsRay")){
            Transform controllerTransform = arg0.interactorObject.transform.parent;

            for(int i = 0; i < controllerTransform.childCount; i++)
            {
                Transform currentChildTransform = controllerTransform.GetChild(i).transform;
                
                if(currentChildTransform.CompareTag("PowerGlove"))
                {
                    currentChildTransform.gameObject.SetActive(true);
                    controllerTransform.GetComponent<ActionBasedController>().model = currentChildTransform;
                }

                if(currentChildTransform.CompareTag("PhysicsRay"))
                {
                    currentChildTransform.gameObject.SetActive(true); //Enable ray
                }

                if(currentChildTransform.CompareTag("Hand"))
                {
                    Destroy(currentChildTransform.gameObject); // Destroy direct interactor and normal hand model
                }
            }

            Destroy(gameObject); // Destroy power glove that was picked up
        }
    }
}