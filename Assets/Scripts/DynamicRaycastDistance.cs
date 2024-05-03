//Not being used - Cameron Schmidt
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class DynamicRaycastDistance : MonoBehaviour
{
    private XRRayInteractor interactor;
    public InputActionReference inputActionReference;

    private void Start(){
        interactor = GetComponent<XRRayInteractor>();
    }

    private void Update(){

    }
}
