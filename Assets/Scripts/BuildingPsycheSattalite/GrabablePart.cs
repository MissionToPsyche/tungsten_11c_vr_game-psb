// Created 11/1/23 - Noah Pfeffer
// Handles functionality related to grabable objects. Heavily modified later on.
// Modification History:
/* 
    1/28/24 - Cameron Schmidt

    2/11/24 - Cameron Schmidt

    3/17/24 - Cameron Schmidt
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
//using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabablePart : MonoBehaviour
{
    private XRGrabInteractable _interactor;
    private Rigidbody _rigidBody;
    private GameObject _mesh;
    //private List<Material> _defaultMaterials;
    private Transform _popUpImgTrans;
    private GameObject _glowMesh;
    private Material _defaultGlowMaterial;
    private Vector3 _origin;
    private bool _partSnapped;
    [SerializeField]
    private AudioClip _grabbedFX;
    [SerializeField]
    private AudioClip _droppedFX;
    [SerializeField]
    private AudioClip _collisionFX;

    public Material glowMaterial;

    void Start()
    {
        _origin = transform.position;
        _interactor = GetComponent<XRGrabInteractable>();
        _rigidBody = GetComponent<Rigidbody>();
        _mesh = transform.GetChild(0).gameObject;
        _partSnapped = false;
        if(transform.childCount > 1){
            _popUpImgTrans = transform.GetChild(1);
        }
        _glowMesh = Instantiate(transform.GetChild(0).gameObject);
        _glowMesh.transform.SetParent(transform.GetChild(0));
        _glowMesh.transform.SetAsLastSibling();
        _glowMesh.name = "GlowMesh";
        _glowMesh.transform.localPosition = new Vector3(0f, 0f, 0f);
        _glowMesh.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        _glowMesh.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        _glowMesh.SetActive(false);

        for(int i = 0; i < _glowMesh.transform.childCount; i++)
        {
            _glowMesh.transform.GetChild(i).position = transform.GetChild(0).GetChild(i).position;

            if(_glowMesh.transform.GetChild(i).TryGetComponent<MeshRenderer>(out MeshRenderer meshRend))
            {
                meshRend.material = glowMaterial;
            }

            if(_glowMesh.transform.GetChild(i).TryGetComponent<BoxCollider>(out BoxCollider boxColid))
            {
                Destroy(boxColid);
            }

            if(_glowMesh.transform.GetChild(i).TryGetComponent<CapsuleCollider>(out CapsuleCollider capColid))
            {
                Destroy(capColid);
            }

            if(_glowMesh.transform.GetChild(i).TryGetComponent<SphereCollider>(out SphereCollider sphColid))
            {
                Destroy(sphColid);
            }

            if(_glowMesh.transform.GetChild(i).TryGetComponent<MeshCollider>(out MeshCollider meshColid))
            {
                Destroy(meshColid);
            }
        }
    }

    void Update()
    {
        if(transform.position.y < -20 && !_partSnapped){
            transform.position = _origin;
            SoundFXManager.instance.PlaySoundFXClip(_collisionFX, transform, 0.25f, _collisionFX.length);
        }
    }

    // public void GlowSoft(){
    //     foreach(MeshRenderer mesh in _mesh.GetComponentsInChildren<MeshRenderer>()){
    //         _defaultMaterials.Add(mesh.material);
    //         mesh.material = glowMaterial;
    //     }
    // }

    public void EnterSnapOrGlow(SelectEnterEventArgs arg0)
    {
        if(_popUpImgTrans != null){
            _popUpImgTrans.gameObject.SetActive(false);
        }
        if(arg0.interactorObject.transform.CompareTag("SnapZone")){
            _partSnapped = true;
            _interactor.smoothPosition = false;
            _glowMesh.SetActive(false);
            for(int i = 0; i < _mesh.transform.childCount; i++){
                if(_mesh.transform.GetChild(i).TryGetComponent<BoxCollider>(out BoxCollider bCollider)){
                    bCollider.enabled = false;
                }
                if(_mesh.transform.GetChild(i).TryGetComponent<MeshCollider>(out MeshCollider mCollider)){
                    mCollider.enabled = false;
                }
            }
            Destroy(transform.GetComponent<GrabablePart>());
            return;
        }else{
            _interactor.smoothPosition = true;
            _glowMesh.SetActive(true);
        }
    }

    public void Default(SelectExitEventArgs arg0)
    {
        arg0.interactableObject.transform.GetComponent<XRGrabInteractable>().smoothPosition = false;
        arg0.interactableObject.transform.GetComponent<XRGrabInteractable>().smoothRotation = false;

        _glowMesh.SetActive(false);
        // int i = 0;
        // foreach(MeshRenderer mesh in _mesh.GetComponentsInChildren<MeshRenderer>()){
        //     mesh.material = _defaultMaterials.ToArray()[i];
        //     i++;
        // }
    }

    public void GrabSound(SelectEnterEventArgs arg0)
    {
        SoundFXManager.instance.PlaySoundFXClip(_grabbedFX, transform, 1f, _grabbedFX.length);
    }

    public void DropSound(SelectExitEventArgs arg0)
    {
        SoundFXManager.instance.PlaySoundFXClip(_droppedFX, transform, 1f, _droppedFX.length);
    }

    public void OnCollisionEnter(Collision collision)
    {
        //SoundFXManager.instance.PlaySoundFXClip(_collisionFX, transform, 1f, _collisionFX.length);
    }

    public void SetOrigin(Vector3 newOrigin)
    {
        _origin = newOrigin;
    }
}
