using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UserInput : MonoBehaviour
{
    
    public InputField inputField;
    //public GameObject textDisplay;
    public GameObject popUpUI;
    

    private void Start()
    {
        popUpUI = GameObject.Find("DialogUI");
        popUpUI.SetActive(false);
    }

    public void StoreInput()
    {
        string s = "";
        s = inputField.text;
        inputField.Select();
        inputField.text = "";
        GameObject.Find("Canvas").GetComponent<StartGame>().SendHumanFeedback(s);
    }

    public void PopUp()
    {
        if (popUpUI.activeSelf) {
            popUpUI.SetActive(false);
            GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().StartAll();
        }
        else
        {
            popUpUI.SetActive(true);
            GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().StopAll();
        }
        
    }
}
