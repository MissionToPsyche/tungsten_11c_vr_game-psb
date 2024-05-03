// Created 3/31/24 - Cameron Schmidt
// Functionality for physics gun
// Modification History:
/* 
    3/31/24 - Cameron Schmidt

    4/1/24 - Cameron Schmidt
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PowerGloveController : MonoBehaviour
{
    public InputActionReference _triggerInputActionReference;
    // [SerializeField]
    // private InputActionReference _gripInputActionReference;
    private Transform _physicsRay;
    private float _triggerValue;
    private AudioClip _beamInUse;

    public GameObject pauseMenu;

    public GameObject gameTimer;

    public void Start()
    {
        _triggerValue = 0f;
        for(int i = 0; i < transform.parent.childCount; i++)
        {
            if(transform.parent.GetChild(i).CompareTag("PhysicsRay"))
            {
                _physicsRay = transform.parent.GetChild(i);
            }
        }
    }
    public void Update()
    {
        _triggerValue = _triggerInputActionReference.action.ReadValue<float>();

        if((gameTimer.GetComponent<GameTimer>().hasEnded == false) && pauseMenu.GetComponent<PauseMenu>().activeWristUI == false){
            if(_triggerValue > 0.65f)
            {
                _physicsRay.gameObject.SetActive(true);
            }
            else
            {
                _physicsRay.gameObject.SetActive(false);
            }
        }
        else
        {
            _physicsRay.gameObject.SetActive(false);
        }
    }
}
