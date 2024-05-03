// Created 1/19/24 - Noah Pfeffer
// Movement functionality and swapping with accessibility options
// Modification History:
/* 
    1/19/24 - Noah Pfeffer

    4/23/24 - Noah Pfeffer

    4/25/24 - Noah Pfeffer
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Movement : MonoBehaviour
{
    public GameObject locomotion;
    public GameObject teleportRay;
    public GameObject move;
    public GameObject continuousTurn;
    public GameObject snapTurn;

    private ActionBasedContinuousTurnProvider continuousTurnProvider;
    private ActionBasedSnapTurnProvider snapTurnProvider;

    // Start is called before the first frame update
    void Start()
    {
        ContinuousMoveProviderBase continuous = move.GetComponent<ContinuousMoveProviderBase>();
        TeleportationProvider teleport = locomotion.GetComponent<TeleportationProvider>();
        continuousTurnProvider = continuousTurn.GetComponent<ActionBasedContinuousTurnProvider>();
        snapTurnProvider = snapTurn.GetComponent<ActionBasedSnapTurnProvider>();

        /*if(Global.turnSet == true)
        {
            Debug.Log("Global continuous turn is set");
        }
        else
        {
            Global.rightHandTurnAction = continuousTurnProvider.rightHandTurnAction;
            Global.turnSet = true;
        }

        if(Global.snapTurnSet == true)
        {
            Debug.Log("Global snap turn is set");
        }
        else
        {
            Global.rightHandSnapTurnAction = snapTurnProvider.rightHandSnapTurnAction;
            Global.snapTurnSet = true;
        }*/

        //rightHandTurnAction = Instantiate(continuousTurnProvider.rightHandTurnAction);
        //rightHandSnapTurnAction = Instantiate(snapTurnProvider.rightHandSnapTurnAction);

        OnUnpause();
    }

    public void OnUnpause()
    {
        ContinuousMoveProviderBase continuous = move.GetComponent<ContinuousMoveProviderBase>();
        TeleportationProvider teleport = locomotion.GetComponent<TeleportationProvider>();

        if (Global.movement == "Continuous"){
            continuous.enabled = true;
            teleport.enabled = false;
            if(Global.paused == false){
                teleportRay.SetActive(false);
            }
        }
        else{
            continuous.enabled = false;
            teleport.enabled = true;
            if(Global.paused == false){
                teleportRay.SetActive(true);
            }
        }

        if (Global.turn == "Continuous"){
            snapTurnProvider.rightHandSnapTurnAction.action.Disable();
            continuousTurnProvider.rightHandTurnAction.action.Enable();
            //continuousTurnProvider.enabled = true;
            //snapTurnProvider.enabled = false;
        }
        else{
            continuousTurnProvider.rightHandTurnAction.action.Disable();
            snapTurnProvider.rightHandSnapTurnAction.action.Enable();
            //snapTurnProvider.enabled = true;
            //continuousTurnProvider.enabled = false;
        }

    }

    public void OnExit(){
        //continuousTurnProvider.enabled = true;
        //snapTurnProvider.enabled = true;
        //continuousTurnProvider.rightHandTurnAction = null;
        //snapTurnProvider.rightHandSnapTurnAction = null;
        snapTurnProvider.rightHandSnapTurnAction.action.Enable();
        continuousTurnProvider.rightHandTurnAction.action.Enable();
    }

    void OnTeleportActivate(InputAction.CallbackContext context){
        teleportRay.SetActive(true);
    }

    void OnTeleportCancel(InputAction.CallbackContext context){
        teleportRay.SetActive(false);
    }
}
