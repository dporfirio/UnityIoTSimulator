using UnityEngine;
using System.Collections;
using UnitySocketIO;
using UnitySocketIO.Events;

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


public class StartGame : MonoBehaviour
{

    public SocketIOController io;

    void Start()
    {
        io.On("connect", (SocketIOEvent e) => {
            Debug.Log("SocketIO connected");
            //io.Emit("GameStart#");
        });

        io.On("start", (SocketIOEvent e) =>
        {
            Debug.Log("Start Game");
            GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().ChangeInc(1);
        });


        io.On("robotprogram", (SocketIOEvent e) =>
        {
            Debug.Log("Receive new robot programs");
        });

        io.On("update_condition", (SocketIOEvent e) =>
        {
            Debug.Log("Update condition for robot programs");
        });


        io.Connect();
        Debug.Log("Try to Connect");

        //TestObject t = new TestObject();
        //t.test = 123;
        //t.test2 = "test1";

        //io.Emit("test-event2", JsonUtility.ToJson(t));

        //TestObject t2 = new TestObject();
        //t2.test = 1234;
        //t2.test2 = "test2";

        //io.Emit("test-event3", JsonUtility.ToJson(t2), (string data) => {
        //    Debug.Log(data);
        //});
    }

    //public void SendTimeUpdate(string date, string time)
    //{

    //    io.Emit("updatetime", JsonUtility.ToJson("#day:" + date + "#time:"+time));
    //}


    public void SendHumanFeedback(string feedback)
    {
        HumanFeedback hFeed = new HumanFeedback();
        hFeed.feedback = feedback;
        hFeed.msgType = "HumanFeedback";
        GameObject.Find("Player").GetComponent<Connection>().SendWebSocketMessage(JsonUtility.ToJson(hFeed));
        //io.Emit("sendfeedback", JsonUtility.ToJson(hFeed));
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

        GameObject.Find("Player").GetComponent<Connection>().SendWebSocketMessage(JsonUtility.ToJson(update));
        //io.Emit("updatedevices", JsonUtility.ToJson(update));
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