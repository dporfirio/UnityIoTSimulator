using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public StartGame sg;
    public void ChooseDay(int day)
    {
        // Send to ROS
        sg.SendDate(day);
        
    }

}
