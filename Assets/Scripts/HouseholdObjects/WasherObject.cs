using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasherObject : IoTDevice
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
        HumanAction action = new Laundary(gameObject);
        this.actions.Add(action);
    }
}
