using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class HaveSnack : Activity
{

    // Start is called before the first frame update
    public HaveSnack(GameObject food, Player player)
    {
        this.actingObject = food;
        this.player = player;
        this.command = "have snack";
        this.description = "having snack";
    }

    public override bool CheckActivityConditions()
    {
        bool isClose = this.actingObject.GetComponent<SnackObject>().QueryPosition();
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
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().TimeFly(150, this);
        GameObject.Find("PlayerCanvas").GetComponent<Player>().AddEnergy(10);
    }

    public override void EndAct()
    {
        GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
    }
}
