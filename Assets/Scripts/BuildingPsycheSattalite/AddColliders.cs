// Created 1/21/24 - Cameron Schmidt
// Add colliders to the individual game object children for satellite
// Modification History:
/* 
    1/21/24 - Cameron Schmidt
 */
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AddColliders : MonoBehaviour
{
    void Awake()
    {
        for(int i = 0; i < transform.childCount; i++){
            GameObject child = transform.GetChild(i).gameObject;
            child.AddComponent<MeshCollider>();
            MeshCollider newMeshCollide = child.GetComponent<MeshCollider>();
            newMeshCollide.convex = true;
        }
    }
}
