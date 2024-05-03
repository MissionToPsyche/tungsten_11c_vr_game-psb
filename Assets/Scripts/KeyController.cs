// Created 11/29/23 - Jack Brand
// Controls vr keyboard input functionality 
// Modification History:
/* 
    11/29/23 - Jack Brand
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyController : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField input;
    private string num;

    // Start is called before the first frame update
    void Start()
    {
        num = GetComponentInChildren<TMP_Text>().text;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void keyClicked()
    {
        if (num == "del")
        {
            string temp = input.text.Substring(0, input.text.Length - 1);
            input.text = temp;

        }
        else
        {
           input.text += num;
        }
        
    }
}
