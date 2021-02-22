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
        GameObject.Find("Lunch").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Lunch").GetComponent<LunchObject>().enabled = true;
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().TimeFly(500);
    }

    public override void EndAct()
    {

    }
}
