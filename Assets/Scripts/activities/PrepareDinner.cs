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
        this.command = "prepare a meal";
        this.description = "preparing a meal";
    }

    public override bool CheckActivityConditions()
    {
        bool isClose = this.actingObject.GetComponent<OvenObject>().QueryPosition();
        if (!isClose || this.canMove)
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
        GameObject.Find("Oven").GetComponent<OvenObject>().state = "on";
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().TimeFly(250, this);
        GameObject.Find("Stove").GetComponent<StoveObject>().removeAct();
        GameObject.Find("Fridge").GetComponent<FridgeObject>().removeAct();
        GameObject.Find("Oven").GetComponent<OvenObject>().removeAct();
    }

    public override void EndAct()
    {
        GameObject.Find("Oven").GetComponent<OvenObject>().state = "off";
        GameObject.Find("Dinner").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Dinner").GetComponent<DinnerObject>().distanceBound = 3;
        GameObject.Find("Dinner").GetComponent<DinnerObject>().enabled = true;
        GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
    }
}
