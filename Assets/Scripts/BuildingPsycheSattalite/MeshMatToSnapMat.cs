// Created 1/25/24 - Cameron Schmidt
// Script for glowing material for snapzone
// Modification History:
/* 
    1/25/24 - Cameron Schmidt
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshMatToSnapMat : MonoBehaviour
{
    public Material glowMaterial;
    void Update()
    {
        for(int i = 0; i < transform.childCount; i++){
            GameObject child = transform.GetChild(i).gameObject;
            child.GetComponent<MeshRenderer>().material = glowMaterial;
        }
    }
}
