//Not being used anymore - Noah Pfeffer
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Launcher : MonoBehaviour
{
    public GameObject satellite;

    public void AButtonpressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            satellite.GetComponent<OrbitPsyche>().Launch();
        }
    }
}
