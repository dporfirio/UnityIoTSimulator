using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DishwasherObject : IoTDevice
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
        HumanAction drinkActivity = new WashDish(gameObject);
        this.actions.Add(drinkActivity);
    }
}
