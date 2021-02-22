using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class HaveDinner : Activity
{

    // Start is called before the first frame update
    public HaveDinner(GameObject food, Player player)
    {
        this.actingObject = food;
        this.player = player;
        this.command = "have dinner";
        this.description = "having dinner";
    }

    public override bool CheckActivityConditions()
    {
        bool isClose = this.actingObject.GetComponent<DinnerObject>().QueryPosition();
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
        GameObject.Find("Dinner").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Dinner").GetComponent<DinnerObject>().enabled = false;
    }

    public override void EndAct()
    {

    }
}
