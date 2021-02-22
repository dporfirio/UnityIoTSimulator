﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestroomObject : HouseObject
{
    void Start()
    {
        GameObject g = GameObject.Find("PlayerCanvas");
        this.player = g.GetComponent<Player>();
        this.player.registerObject(this);
        this.actions = new List<Action>();
        this.activities = new List<Activity>();

        // add list of activities and actions
        Activity activity = new ReadBook(gameObject, this.player);
        this.activities.Add(activity);
    }
}
