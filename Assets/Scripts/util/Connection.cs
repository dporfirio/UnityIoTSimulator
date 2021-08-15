using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.SceneManagement;

using NativeWebSocket;

class CloudMessage
{
    public string msgType;
    public string data;
}

class StringArray
{
    public string[] array;
}

class String2DArray
{
    public StringArray[] arr;
}

class DeviceUpdateMsg
{
    public string behavior;
    public String2DArray activating;
    public String2DArray deactivating;
}

public class Connection : MonoBehaviour
{
    WebSocket websocket;
    public RegisterKey rKey;

    // Start is called before the first frame update
    async void Start()
    {
        websocket = new WebSocket("wss://constrainrobot.cs.wisc.edu");
        //websocket = new WebSocket("ws://localhost:8080");

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
            GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().StopAll();

            // Disable trigger feedback
            rKey.RemoveKey(KeyCode.Return);

        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) =>
        {
            // Reading a plain text message
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log("Received OnMessage! (" + bytes.Length + " bytes) " + message);
            CloudMessage cm = JsonUtility.FromJson<CloudMessage>(message);
            switch (cm.msgType)
            {
                case "Pause":
                    HandlePause();
                    break;
                case "CUpdate":
                    //Debug.Log(cm.data);
                    //HandleCUpdate(cm);

                    HandleCUpdate(JSON.Parse(message));
                    break;
                case "Start":
                    HandleStart(cm);
                    break;
            }

        };

        // Keep sending messages at every 0.3s
        InvokeRepeating("KeepConnected", 0.0f, 0.3f);

        // waiting for messages
        await websocket.Connect();
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
#endif
    }

    void HandlePause()
    {
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().StopAll();

        // Disable trigger feedback
        rKey.RemoveKey(KeyCode.Return);
    }

    void HandleCUpdate(JSONNode cm)
    {
        bool stop = false;

        // if it's stop or not
        if (cm["behavior"].Value.Contains("stop"))
        {
            stop = true;
        }
        Program pro = null;
        string beh;
        if (stop)
        {
            beh = cm["behavior"].Value.Split('_')[1];
        }
        else
        {
            beh = cm["behavior"].Value;
        }

        switch (beh)
        {
            case "remindcalls":
                pro = GameObject.Find("PhoneCallProgram").gameObject.GetComponent<PhoneCallProgram>();
                break;
            case "vacuum":
                pro = GameObject.Find("VacuumProgram").gameObject.GetComponent<VacuumProgram>();
                break;
            case "pickuppackage":
                pro = GameObject.Find("RetrievePackageProgram").gameObject.GetComponent<RetrievePackageProgram>();
                break;
            case "makefood":
                pro = GameObject.Find("MakeFoodProgram").gameObject.GetComponent<MakeFoodProgram>();
                break;
        }
        if (stop)
        {
            foreach (JSONNode tmp in cm["deactivating"]["arr"].Values)
            {
                Conditional cond = new Conditional(); // AND condition


                foreach (string devicename in tmp["array"].Values)
                {
                    cond.RegisterCondition(devicename.Split('_')[0], devicename.Split('_')[1], true);
                }

                GameObject.Find("Robot").GetComponent<RobotController>().triggers.RegisterActionWithConditional(pro, cond);

            }
        }
        else
        {
            // currently this part is not used.
            foreach (JSONNode tmp in cm["activating"]["arr"].Values)
            {
                Conditional cond = new Conditional(); // AND condition
                foreach (string devicename in tmp["array"].Values)
                {
                    cond.RegisterCondition(devicename.Split('_')[0], devicename.Split('_')[1], false);
                }
                GameObject.Find("Robot").GetComponent<RobotController>().triggers.RegisterActionWithConditional(pro, cond);
            }
        }
    }

    // Pop up the feedback window
    void StartInputFunc()
    {
        GameObject.Find("ControlInput").GetComponent<UserInput>().PopUp();
    }

    void HandleStart(CloudMessage cm)
    {
        if (GameObject.Find("LoadScreen") != null)
        {
            GameObject.Find("LoadScreen").SetActive(false);
            GameObject.Find("Background").SetActive(false);
        }
        //Time.timeScale = 1;
        Debug.Log("received to Start!!");
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().StartAll();
        //GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
        // Enable trigger feedback
        rKey.AddKey(KeyCode.Return, StartInputFunc);
    }

    async void KeepConnected()
    {
        if (websocket.State == WebSocketState.Open)
        {
            await websocket.SendText("ping");

        }
    }

    async public void SendWebSocketMessage(String msg)
    {

        if (websocket.State == WebSocketState.Open)
        {
            // Sending plain text
            await websocket.SendText(msg);
        }
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }

}