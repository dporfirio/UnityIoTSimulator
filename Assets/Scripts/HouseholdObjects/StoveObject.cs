using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveObject : IoTDevice
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
        Activity drinkActivity = new PrepareLunch(gameObject, this.player);
        this.activities.Add(drinkActivity);
    }
    public void removeAct()
    {
        Activity drinkActivity = new PrepareLunch(gameObject, this.player);
        this.activities.RemoveAt(0);
    }

    public void addAct()
    {
        Activity drinkActivity = new PrepareLunch(gameObject, this.player);
        this.activities.Add(drinkActivity);
    }
}
