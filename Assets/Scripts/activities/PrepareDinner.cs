using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PrepareDinner : Activity
{

    // Start is called before the first frame update
    public PrepareDinner(GameObject any, Player player)
    {
        this.actingObject = any;
        this.player = player;
        this.command = "prepare dinner";
        this.description = "preparing dinner";
    }

    public override bool CheckActivityConditions()
    {
        bool isClose = this.actingObject.GetComponent<OvenObject>().QueryPosition();
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
        GameObject.Find("Dinner").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Dinner").GetComponent<DinnerObject>().enabled = true;
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().TimeFly(500);
    }

    public override void EndAct()
    {

    }
}
