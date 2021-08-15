using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BrushTeeth : Activity
{

    // Start is called before the first frame update
    public BrushTeeth(GameObject any, Player player)
    {
        this.actingObject = any;
        this.player = player;
        this.command = "brush teeth";
        this.description = "brushing teeth";
    }

    public override bool CheckActivityConditions()
    {
        bool isClose = this.actingObject.GetComponent<SinkObject>().QueryPosition();
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
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().TimeFly(150, this);
    }

    public override void EndAct()
    {
        GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
    }
}
