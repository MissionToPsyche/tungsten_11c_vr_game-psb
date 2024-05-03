//Not being used
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PointCondition : MonoBehaviour
{
    public bool condition;
    // Start is called before the first frame update
    void Start()
    {
        condition = false;
    }

    public void ConditionCheck(SelectEnterEventArgs arg0)
    {
        condition = true;
    }
}
