using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiningMotionSensor : IoTDevice
{
    private bool isClose;
    public GameObject playerObj;
    // Start is called before the first frame update
    void Start()
    {
        GameObject g = GameObject.Find("PlayerCanvas");
        this.player = g.GetComponent<Player>();
        this.isActive = true;
        this.state = "Idle";
        this.isClose = false;
        
    }

    void Update()
    {
        if (playerObj != null)
        {
            this.isClose = this.QueryPosition();
        }
        
        if (!this.isClose)
        {
            this.state = "Idle";
        }
        else
            this.state = "Detect";
    }

}
