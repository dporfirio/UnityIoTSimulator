using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Laundary : Activity
{

    // Start is called before the first frame update
    public Laundary(GameObject any, Player player)
    {
        this.actingObject = any;
        this.player = player;
        this.command = "change clothes";
        this.description = "changing clothes";
    }

    public override bool CheckActivityConditions()
    {
        bool isClose = this.actingObject.GetComponent<WasherObject>().QueryPosition();
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
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().TimeFly(500);
    }

    public override void EndAct()
    {

    }
}
