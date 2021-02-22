using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UserInput : MonoBehaviour
{
    public string s;
    public GameObject inputField;
    public GameObject textDisplay;
    public GameObject popUpUI;
    

    private void Start()
    {
        popUpUI = GameObject.Find("DialogUI");
        popUpUI.SetActive(false);
    }

    public void StoreInput()
    {
        s = inputField.GetComponent<Text>().text;
        textDisplay.GetComponent<Text>().text = "The user said" + s;

    }

    public void PopUp()
    {
        if (popUpUI.activeSelf) {
            popUpUI.SetActive(false);
        }
        else
        {
            popUpUI.SetActive(true);
        }
        
    }
}
