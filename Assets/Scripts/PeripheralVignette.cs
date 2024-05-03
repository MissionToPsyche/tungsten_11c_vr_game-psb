// Created 1/19/24 - Noah Pfeffer
// Peripheral vignette functionality
// Modification History:
/* 
    1/19/24 - Noah Pfeffer
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PeripheralVignette : MonoBehaviour
{

    public GameObject vignette;

    // Start is called before the first frame update
    void Start()
    {
        if (Global.vignette == "Off"){
            vignette.SetActive(false);
        }
        else{
            vignette.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Global.vignette == "Off"){
            vignette.SetActive(false);
        }
        else{
            vignette.SetActive(true);
        }
    }
}
