using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UserInput : MonoBehaviour
{
    // Linked with feedback window
    public InputField inputField;
    public GameObject popUpUI;

    public TimeUpdater tUpdate;

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
        PopUp();
    }

    public void PopUp()
    {
        if (popUpUI.activeSelf)
        {
            popUpUI.SetActive(false);
            tUpdate.StartAll();
        }
        else
        {
            popUpUI.SetActive(true);
            tUpdate.StopAll();
        }
    }
}
