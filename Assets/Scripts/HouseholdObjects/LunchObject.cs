﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunchObject : HouseObject
{
    void Start()
    {
        GameObject g = GameObject.Find("PlayerCanvas");
        this.player = g.GetComponent<Player>();
        this.player.registerObject(this);
        this.actions = new List<Action>();
        this.activities = new List<Activity>();

        // add list of activities and actions
        Activity eatActivity = new HaveLunch(gameObject, this.player);
        this.activities.Add(eatActivity);
    }
}
