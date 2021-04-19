using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PrepareLunch : Activity
{

    // Start is called before the first frame update
    public PrepareLunch(GameObject any, Player player)
    {
        this.actingObject = any;
        this.player = player;
        this.command = "prepare lunch";
        this.description = "preparing lunch";
    }

    public override bool CheckActivityConditions()
    {
        bool isClose = this.actingObject.GetComponent<StoveObject>().QueryPosition();
        if (!isClose || GameObject.Find("Player").GetComponent<PlayerMovement>().canMove)
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
        GameObject.Find("Stove").GetComponent<StoveObject>().state = "on";
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().TimeFly(250, this);
    }

    public override void EndAct()
    {
        GameObject.Find("Stove").GetComponent<StoveObject>().state = "off";
        GameObject.Find("Lunch").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Lunch").GetComponent<LunchObject>().distanceBound = 3;
        GameObject.Find("Lunch").GetComponent<LunchObject>().enabled = true;
        GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
    }
}
