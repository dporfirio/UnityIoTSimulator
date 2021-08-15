using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TakeShower: Activity
{

    // Start is called before the first frame update
    public TakeShower(GameObject obj, Player player)
    {
        this.actingObject = obj;
        this.player = player;
        this.command = "take shower";
        this.description = "taking shower";
    }

    public override bool CheckActivityConditions()
    {
        bool isClose = this.actingObject.GetComponent<BathSinkObject>().QueryPosition();
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
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().TimeFly(300, this);
    }

    public override void EndAct()
    {
        GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
    }
}
