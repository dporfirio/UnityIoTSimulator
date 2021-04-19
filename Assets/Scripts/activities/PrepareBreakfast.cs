using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PrepareBreakfast : Activity
{

    // Start is called before the first frame update
    public PrepareBreakfast(GameObject any, Player player)
    {
        this.actingObject = any;
        this.player = player;
        this.command = "prepare breakfast";
        this.description = "preparing breakfast";
    }

    public override bool CheckActivityConditions()
    {
        bool isClose = this.actingObject.GetComponent<FridgeObject>().QueryPosition();
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
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().TimeFly(75, this);
    }

    public override void EndAct()
    {
        GameObject.Find("Breakfast").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Breakfast").GetComponent<BreakfastObject>().distanceBound = 2;
        GameObject.Find("Breakfast").GetComponent<BreakfastObject>().enabled = true;
        GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
    }
}
