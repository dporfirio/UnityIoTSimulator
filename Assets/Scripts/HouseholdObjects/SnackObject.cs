using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnackObject : HouseObject
{
    void Start()
    {
        GameObject g = GameObject.Find("PlayerCanvas");
        this.player = g.GetComponent<Player>();
        this.player.registerObject(this);
        this.actions = new List<HumanAction>();
        this.activities = new List<Activity>();

        // add list of activities and actions
        Activity eatActivity = new HaveSnack(gameObject, this.player);
        this.activities.Add(eatActivity);
    }
}
