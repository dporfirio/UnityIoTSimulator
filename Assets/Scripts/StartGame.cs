using UnityEngine;
using System.Collections;
using UnitySocketIO;
using UnitySocketIO.Events;
using UnityEngine.UI;

class DeviceUpdate
{
    public string msgType;

    public string[] deviceStates;
    public int date;
    public int second;
    public string human;
    public string robot;
}

class HumanActivity
{
    public string msgType;

    public string[] states;
    public string activity;
}


//class Device
//{
//    public string name;

//}


class TimeUpdate
{
    public string date;
    public string hour;
    public string second;
}

class HumanFeedback
{
    public string msgType;

    public string feedback;
}

class PickDate
{
    public string msgType;
    public int date;
    public string participantId;
}


public class StartGame : MonoBehaviour
{
    public InputField partcipantField;
    public Connection conn;

    //public void SendTimeUpdate(string date, string time)
    //{

    //    io.Emit("updatetime", JsonUtility.ToJson("#day:" + date + "#time:"+time));
    //}


    public void SendHumanFeedback(string feedback)
    {
        HumanFeedback hFeed = new HumanFeedback();
        hFeed.feedback = feedback;
        hFeed.msgType = "HumanFeedback";
        this.conn.SendWebSocketMessage(JsonUtility.ToJson(hFeed));
    }

    public void SendDate(int day)
    {
        PickDate date = new PickDate();
        date.date = day;
        date.participantId = partcipantField.text;
        date.msgType = "PickDate";
        Debug.Log("PickDate");
        this.conn.SendWebSocketMessage(JsonUtility.ToJson(date));
    }

    public void SendDeviceUpdate(int day_cnt, int second_cnt_perday, string human, string robot)
    {

        IoTDevice[] devices = Object.FindObjectsOfType<IoTDevice>();

        string[] states = new string[devices.Length - 1]; // -1 to ignore sun
        int cnt = 0;
        foreach (IoTDevice device in devices)
        {
            if (device.name != "Sun")
            {
                states[cnt] = device.state;
                cnt++;
                //Debug.Log(device.name); // Used to debug to check if the list of devices match with the list in the backend's json file
            }
        }

        DeviceUpdate update = new DeviceUpdate();

        update.msgType = "DeviceUpdate";

        update.deviceStates = states;
        update.date = day_cnt;
        update.second = second_cnt_perday;
        update.human = human;
        update.robot = robot;

        this.conn.SendWebSocketMessage(JsonUtility.ToJson(update));
    }


    //public void SendHumanActivity (string human)
    //{
    //    HumanActivity update = new HumanActivity();
    //    update.activity = human;

    //    IoTDevice[] devices = Object.FindObjectsOfType<IoTDevice>();

    //    string[] states = new string[devices.Length - 1]; // -1 to ignore sun
    //    int cnt = 0;
    //    foreach (IoTDevice device in devices)
    //    {
    //        if (device.name != "Sun")
    //        {
    //            states[cnt] = device.state;
    //            cnt++;
    //            //Debug.Log(device.name); // Used to debug to check if the list of devices match with the list in the backend's json file
    //        }
    //    }

    //    update.states = states;

    //    update.msgType = "HumanAct";

    //    GameObject.Find("Player").GetComponent<Connection>().SendWebSocketMessage(JsonUtility.ToJson(update));

    //    //io.Emit("humanActivities", JsonUtility.ToJson(update));
    //}


}