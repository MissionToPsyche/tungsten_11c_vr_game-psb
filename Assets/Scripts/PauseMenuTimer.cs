// Created 11/29/23 - Jack Brand
// Controls the functionality of the timer in the pause menu - mimics main timer
//      -> Logic for main timer can be found in GameTimer.cs
// Modification History:
/* 
    11/29/23 - Jack Brand

    4/17/24 - Jack Brand
 */

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenuTimer : MonoBehaviour
{
    public GameTimer mainTimer;
    private Text timerText;
    public GameObject rightRay;

    [SerializeField]    private TextMeshProUGUI minute1P;
    [SerializeField]    private TextMeshProUGUI minute2P;
    [SerializeField]    private TextMeshProUGUI dotsP;
    [SerializeField]    private TextMeshProUGUI second1P;
    [SerializeField]    private TextMeshProUGUI second2P;
    [SerializeField]    private GameObject TimeOutUI;

    void Start()
    {
        timerText = GetComponent<Text>();
    }

    void Update()
    {
        // Check if the main timer is available
        if (mainTimer != null)
        {
            // Update the pause menu timer based on the main timer
            float mainTimerValue = mainTimer.getTimer();
            if (mainTimerValue > 0)
            {
                UpdatePauseMenuTimer(mainTimerValue);
            }
            else
            {
                TimerTimeout();
            }
        }
    }

    void UpdatePauseMenuTimer(float timerValue)
    {
        float minutes = Mathf.FloorToInt(timerValue / 60);
        float seconds = Mathf.FloorToInt(timerValue % 60);

        // formats time
        string currentTime = string.Format("{00:00}{1:00}", minutes, seconds);

        // sets individual time variables 
        minute1P.text = currentTime[0].ToString();
        minute2P.text = currentTime[1].ToString();
        second1P.text = currentTime[2].ToString();
        second2P.text = currentTime[3].ToString();
    }

    void TimerTimeout()
    {

    }
}