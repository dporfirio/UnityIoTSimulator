using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ChangeCloth : Activity
{

    // Start is called before the first frame update
    public ChangeCloth(GameObject any, Player player)
    {
        this.actingObject = any;
        this.player = player;
        this.command = "change clothes";
        this.description = "changing clothes";
    }

    public override bool CheckActivityConditions()
    {
        bool isClose = this.actingObject.GetComponent<WardrobeObject>().QueryPosition();
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
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().TimeFly(200, this);
    }

    public override void EndAct()
    {
        GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
    }
}
