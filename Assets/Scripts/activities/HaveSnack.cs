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
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().TimeFly(500);
        GameObject.Find("Breakfast").SetActive(false);
    }

    public override void EndAct()
    {

    }
}
