// Created 1/29/23 - Jack Brand
// This script loads scene in conjuction with the timer functionality (not being used currently)
// Modification History:
/* 
    11/29/23 - Jack Brand
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // This method will be called whenever a scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);

        Global.timerInit = UnityEngine.Time.time;
        
    }


    // Don't forget to unsubscribe from the event when the script is destroyed
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}