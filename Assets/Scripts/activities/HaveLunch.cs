﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class HaveLunch : Activity
{

    // Start is called before the first frame update
    public HaveLunch(GameObject food, Player player)
    {
        this.actingObject = food;
        this.player = player;
        this.command = "have lunch";
        this.description = "having lunch";
    }

    public override bool CheckActivityConditions()
    {
        bool isClose = this.actingObject.GetComponent<LunchObject>().QueryPosition();
        if (!isClose)
        {
            this.EndAct();
            return false;
        }
        else
            return true;
    }

    public override void Act()
    {
        this.player.UpdateActivity(this);
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().TimeFly(150, this);


    }

    public override void EndAct()
    {
        GameObject.Find("Lunch").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Lunch").GetComponent<LunchObject>().distanceBound = 0;
        GameObject.Find("Lunch").GetComponent<LunchObject>().enabled = false;

        GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
    }
}
