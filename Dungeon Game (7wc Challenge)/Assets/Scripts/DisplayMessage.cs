using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMessage : MonoBehaviour
{
    public bool isDisplayingMsg;

    public void displayMessage(string message)
    {
        GetComponent<UnityEngine.UI.Text>().text = message;
    }
    void Update()
    {
        if(isDisplayingMsg == false)
        {
            GetComponent<UnityEngine.UI.Text>().text = "";
        }
    }
}
