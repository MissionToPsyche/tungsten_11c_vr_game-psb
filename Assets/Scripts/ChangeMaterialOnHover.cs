// Created 11/8/23 - Noah Pfeffer
// Handles hovering color for snap zone
// Modification History:
/* 
    11/8/23 - Noah Pfeffer
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class ChangeMaterialOnHover : MonoBehaviour
{
    public Material hoverMaterial;
    private Material originalMaterial;
    private XRBaseInteractable interactable;

    private void Start()
    {
        interactable = GetComponent<XRBaseInteractable>();

        // Store the original material of the object
        originalMaterial = GetComponent<Renderer>().material;
    }

    private void OnEnable()
    {
        // Subscribe to the hover events
        interactable.onHoverEntered.AddListener(OnHoverEnter);
        interactable.onHoverExited.AddListener(OnHoverExit);
    }

    private void OnDisable()
    {
        // Unsubscribe from the hover events
        interactable.onHoverEntered.RemoveListener(OnHoverEnter);
        interactable.onHoverExited.RemoveListener(OnHoverExit);
    }

    private void OnHoverEnter(XRBaseInteractor interactor)
    {
        // Change the material to the hover material
        GetComponent<Renderer>().material = hoverMaterial;
    }

    private void OnHoverExit(XRBaseInteractor interactor)
    {
        // Change the material back to the original material
        GetComponent<Renderer>().material = originalMaterial;
    }
}

