// Created 1/30/24 - Noah Pfeffer
// Handles functionality of fader for scene transitions
// Modification History:
/* 
    1/30/24 - Noah Pfeffer

    2/1/24 - Noah Pfeffer

    2/9/24 - Noah Pfeffer
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 2;
    public Color fadeColor;
    private Renderer rend;
    /*public GameObject tutorial;
    public GameObject rightRay;*/

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        if(fadeOnStart){
            FadeIn();
        }
    }

    public void FadeIn(){
        //Time.timeScale = 1;
        Fade(1,0);
    }

    public void FadeOut(){
        Fade(0,1);
    }


    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut){
        float timer = 0;
        while(timer <= fadeDuration){

            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer/fadeDuration);

            rend.material.SetColor("_BaseColor", newColor);

            timer += Time.deltaTime;
            yield return null;
        }

        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;

        rend.material.SetColor("_Color", newColor2);

        /*if(alphaIn == 1){
            if(tutorial != null){
                tutorial.SetActive(true);
                rightRay.SetActive(true);
            }
        }*/
    }
}
