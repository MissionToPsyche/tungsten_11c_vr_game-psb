// Created 10/24/23 - Cameron Joseph Schmidt
// Functionality for the hand controllers and related animations
// Modification History:
/* 
    10/24/23 - Cameron Joseph Schmidt

    3/2/24 - Cameron Joseph Schmidt
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class AnimateHandController : MonoBehaviour
{
    // References to the input action mapping for the grip and trigger
    public InputActionReference gripInputActionReference; // "Select Value", "Select" keyword is refering to grip
    /**

    Difference between just "Select" and "Select Value" is select is
    more of a bool and select value is a float from 0 to 1, same deal
    with the trigger, this is because the controller can detect a
    long range of sensativity with the inputs

    **/
    public InputActionReference triggerInputActionReference; // "Activate Value", "Activate" keyword is refering to trigger

    private Animator _handAnimator;
    private float _gripValue;
    private float _triggerValue;

    private bool _usingSetGripVal;

    private void Start()
    {
        _handAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        AnimateGrip();
        AnimateTrigger();
    }

    private void AnimateGrip()
    {
        if(_usingSetGripVal) return;

        _gripValue = gripInputActionReference.action.ReadValue<float>();
        _handAnimator.SetFloat("Grip", _gripValue);
    }

    private void AnimateTrigger()
    {
        _triggerValue = triggerInputActionReference.action.ReadValue<float>();
        _handAnimator.SetFloat("Trigger", _triggerValue);
    }

    public void GetGrabbableTag(SelectEnterEventArgs arg0)
    {
        if(arg0.interactableObject.transform.CompareTag("GammaraySpectrometer"))
        {
            _usingSetGripVal = true;
            _gripValue = 0.05f;
        }
        else if(arg0.interactableObject.transform.CompareTag("NeutronSpectrometer"))
        {
            _usingSetGripVal = true;
            _gripValue = 0.15f;
        }
        else
        {
            _usingSetGripVal = false;
        }
    }

    public void GrabObjectExited()
    {
        _usingSetGripVal = false;
    }
}
