// Created 11/28/23 - Jack Brand
// Controls the functionality of the main game timer
// Modification History:
/* 
    11/28/23 - Jack Brand

    11/29/23 - Jack Brand

    4/17/24 - Jack Brand
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using System;
//using Codice.Client.Common;

public class GameTimer : MonoBehaviour
{
    //                     minutes   seconds
    private float duration = 2.5f  *   60f;

    private float timer;
    private bool running = false;
    private float lastTime;
    [SerializeField] private GameObject TimeOutUI;
    public GameObject rightRay;
    private bool isPaused;

    public bool hasEnded = false;

    // SerializeField so they show up in editor
    [SerializeField]
    private TextMeshProUGUI minute1;
    [SerializeField]
    private TextMeshProUGUI minute2;
    [SerializeField]
    private TextMeshProUGUI dots;
    [SerializeField]
    private TextMeshProUGUI second1;
    [SerializeField]
    private TextMeshProUGUI second2;

    // Initial Start State
    void Start()
    {
        if (!running)
        {
            running = true;
            lastTime = UnityEngine.Time.time;
            if (Global.timer == 0) { ResetTimer(); }
            
        }
        
    }

    // Updates each frame
    void Update()
    {
        
        if (Global.timer > 0)
        {
            if (isPaused == false)
            {
            float curr = UnityEngine.Time.time;
            // subtracts time that has passed between frames
            Global.timer -= (curr - lastTime);
            lastTime = curr;
            //Debug.Log(UnityEngine.Time.deltaTime + " ," +  timer);
            Global.timer = Mathf.Clamp(Global.timer, 0, Mathf.Infinity);

            // Updates the timer display in-game
            // floortoint rounds value down
            float minutes = Mathf.FloorToInt(Global.timer / 60);
            float seconds = Mathf.FloorToInt(Global.timer % 60);

            // formats time
            string currentTime = string.Format("{00:00}{1:00}", minutes, seconds);

            // sets individual time variables 
            minute1.text = currentTime[0].ToString();
            minute2.text = currentTime[1].ToString();
            second1.text = currentTime[2].ToString();
            second2.text = currentTime[3].ToString();
            }
        }
        else
        {
            if(hasEnded == false){
                // Flashes when timer is done
                EndTimer();
                hasEnded = true;
            }
        }
                  
    }

    // To Reset the timer to default
    public void ResetTimer()
    {
        lastTime = UnityEngine.Time.time;
        if (Global.timerInit != 0)
        {
            Global.timer = Global.timerInit;
        }
        else
        {
            Global.timer = duration;
        }
        
        
    }

    public void TogglePause()
    {
        
        //isPaused = !isPaused;
        
    }


    // Flashes timer display
    private void EndTimer()
    {
        TimeOutUI.SetActive(true);
        rightRay.SetActive(true);
        Time.timeScale = 0;
    }

    public float getTimer()
    {
        return Global.timer;
    }

    public void setDuration(float d)
    {
        duration = d;
    }

    public float getDuration()
    {
        return duration;
    }

    public bool getRunning()
    {
        return running;
    }

    public float getLastTime()
    {
        return lastTime;
    }

    public void setLastTime(float t)
    {
        lastTime = t;
    }
}
