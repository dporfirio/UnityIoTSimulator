using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Drink : Activity
{

    // Start is called before the first frame update
    public Drink(GameObject drink, Player player)
    {
        this.actingObject = drink;
        this.player = player;
        this.command = "drink";
        this.description = "drinking";
    }

    public override bool CheckActivityConditions()
    {
        bool isClose = this.actingObject.GetComponent<DrinkKitchenObject>().QueryPosition();
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
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().TimeFly(250, this);
    }

    public override void EndAct()
    {
        GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
    }
}
