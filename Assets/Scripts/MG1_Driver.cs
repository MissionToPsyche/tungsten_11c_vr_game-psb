// Created 4/4/24 - Cameron Schmidt
// Driver for event mode minigame one
// Modification History:
/* 
    4/4/24 - Cameron Schmidt
    4/15/24 - Noah Pfeffer
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MG1_Driver : MonoBehaviour
{
    private Transform _environmentTransform;
    private Transform _grabInteractableTransform;
    private Transform _incompletePsycheTransform;
    private Transform _snapZonePartTransforms;
    private Transform _multispectralImagerSnapTransform;
    private Transform _supportBoomOneSnapTransform;
    private Transform _supportBoomTwoSnapTransform;

    public bool minigameOver;

    public GameObject XRRig;

    //Message displayed to user when game is completed
    public GameObject winMenu;

    void Start()
    {
        _environmentTransform = transform.GetChild(0);

        _grabInteractableTransform = transform.GetChild(1);
        _incompletePsycheTransform = _grabInteractableTransform.GetChild(0);
        _snapZonePartTransforms = _incompletePsycheTransform.GetChild(0);
        _multispectralImagerSnapTransform = _snapZonePartTransforms.GetChild(0);
        _supportBoomOneSnapTransform = _snapZonePartTransforms.GetChild(1);
        _supportBoomTwoSnapTransform = _snapZonePartTransforms.GetChild(2);


        StartCoroutine(MinigameOverCheck());
    }

    IEnumerator MinigameOverCheck()
    {
        // Wait until multispectral imager connected
        while(!_multispectralImagerSnapTransform.GetComponent<SnapZone>().hasSelected)
        {
            yield return null;
        }

        while(!_supportBoomOneSnapTransform.GetComponent<SnapZone>().hasSelected)
        {
            yield return null;
        }

        while(!_supportBoomTwoSnapTransform.GetComponent<SnapZone>().hasSelected)
        {
            yield return null;
        }

        float elapsed = 0.0f;

        while(elapsed < 3.0f)
        {
            _incompletePsycheTransform.rotation = Quaternion.Slerp(_incompletePsycheTransform.rotation, _incompletePsycheTransform.rotation * Quaternion.Euler(Vector3.up * -90), Time.deltaTime/1.0f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        minigameOver = true;

        DisplayWinMenu();
    }

    // Display the menu based if finished
    public void DisplayWinMenu()
    {
            winMenu.SetActive(true);
    }
    
    // Transition when player presses Y at end of level
    public void YButtonPressed(InputAction.CallbackContext context)
    {
        if(minigameOver == true)
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
}
