using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using NativeWebSocket;

[Serializable]
class CloudMessage
{
    public string msgType;
    public string data = null;
}

[Serializable]
class StringArray
{
    public string[] array;
}

[Serializable]
class String2DArray
{
    public StringArray[] arr;
}

[Serializable]
class DeviceUpdateMsg
{
    public string behavior;
    public String2DArray activating;
    public String2DArray deactivating;
}

public class Connection : MonoBehaviour
{
    WebSocket websocket;

    // Start is called before the first frame update
    async void Start()
    {
        websocket = new WebSocket("ws://localhost:8080");
        //websocket = new WebSocket("ws://34.123.149.60:8765");
        //websocket = new WebSocket("ws://34.123.149.60:8765");

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
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
                    HandleCUpdate(cm);
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
        //Time.timeScale = 0;
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().StopAll();
        //GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = false;

        // Disable trigger feedback
        GameObject.Find("StartInput").GetComponent<StartInput>().RemoveKey(KeyCode.Return);
    }

    void HandleCUpdate(CloudMessage cm)
    {
        DeviceUpdateMsg cu = JsonUtility.FromJson<DeviceUpdateMsg>(cm.data);
        bool stop = false;

        // if it's stop or not
        if (cu.behavior.Contains("stop"))
        {
            stop = true;
        } 
        Program pro = null;
        string beh;
        if (stop)
        {
            beh = cu.behavior.Split('_')[1];
        } else
        {
            beh = cu.behavior;
        }

        switch(beh)
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
        }
        if (stop)
        {
            foreach (StringArray tmp in cu.deactivating.arr)
            {
                Conditional cond = new Conditional(); // AND condition
                 
                
                foreach (string devicename in tmp.array)
                {
                    cond.RegisterStopCondition(devicename.Split('_')[0], devicename.Split('_')[1]);
                }

                GameObject.Find("Robot").GetComponent<RobotController>().triggers.RegisterActionWithConditional(pro, cond);

            }
        } else
        {
            foreach (StringArray tmp in cu.activating.arr)
            {
                foreach (string devicename in tmp.array)
                {
                    // TODO!
                }
            }
        }
    }

    void HandleStart(CloudMessage cm)
    {
        //Time.timeScale = 1;
        Debug.Log("received to Start!!");
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().StartAll();
        //GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
        // Enable trigger feedback
        GameObject.Find("StartInput").GetComponent<StartInput>().AddKey(KeyCode.Return, GameObject.Find("PlayerCanvas").GetComponent<Player>().StartInputFunc);
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