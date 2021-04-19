using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficePcObject : IoTDevice
{
    void Start()
    {
        GameObject g = GameObject.Find("PlayerCanvas");
        this.player = g.GetComponent<Player>();
        this.player.registerObject(this);
        this.actions = new List<HumanAction>();
        this.activities = new List<Activity>();
        this.state = "off";

        // add list of activities and actions
        Activity workActivity = new SurfInternet(gameObject, this.player);
        this.activities.Add(workActivity);
    }
}
