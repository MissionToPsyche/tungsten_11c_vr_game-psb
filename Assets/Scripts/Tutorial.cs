//Not being used anymore (when tutorial was a static menu)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.Windows;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour{
    public GameObject rightRay;

    public GameObject tutorialUI;

    void Start(){

        Time.timeScale = 0;
    }

    public void ClickPlay(){
        
        Time.timeScale = 1;

        rightRay.SetActive(false);

        tutorialUI.SetActive(false);
    }
}