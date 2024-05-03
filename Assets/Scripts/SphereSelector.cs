// Created 1/30/24 - Jack Brand
// Controls the functionality of selecting the spheres as select zones in minigame 2
//      
// Modification History:
/* 
    1/30/24 - Jack Brand
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SphereSelector : MonoBehaviour
{
    [SerializeField] private GameObject RayObject;
    private XRRayInteractor XRRay;
    
    private GameObject selectZone;

    public Material hoverMaterial;
    private Material originalMaterial;
    private XRBaseInteractable interactable;

    // Start is called before the first frame update
    void Start()
    {
        XRRay = RayObject.GetComponent<XRRayInteractor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (XRRay != null && selectZone != null)
        {

            
           

            
        }
    }
}
