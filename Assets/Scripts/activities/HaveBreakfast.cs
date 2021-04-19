using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class HaveBreakfast : Activity
{

    // Start is called before the first frame update
    public HaveBreakfast(GameObject food, Player player)
    {
        this.actingObject = food;
        this.player = player;
        this.command = "have breakfast";
        this.description = "having breakfast";
    }

    public override bool CheckActivityConditions()
    {
        bool isClose = this.actingObject.GetComponent<BreakfastObject>().QueryPosition();
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
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().TimeFly(100, this);
    }

    public override void EndAct()
    {
        GameObject.Find("Breakfast").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Breakfast").GetComponent<BreakfastObject>().distanceBound = 0;
        GameObject.Find("Breakfast").GetComponent<BreakfastObject>().enabled = false;

        GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
    }
}
