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
        this.command = "have a meal";
        this.description = "having a meal";
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
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().TimeFly(200,this);
        GameObject.Find("Oven").GetComponent<OvenObject>().addAct();
        GameObject.Find("Stove").GetComponent<StoveObject>().addAct();
        GameObject.Find("Fridge").GetComponent<FridgeObject>().addAct();
        GameObject.Find("PlayerCanvas").GetComponent<Player>().AddEnergy(50);

    }

    public override void EndAct()
    {
        GameObject.Find("Dinner").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Dinner").GetComponent<DinnerObject>().distanceBound = 0;
        GameObject.Find("Dinner").GetComponent<DinnerObject>().enabled = false;
        //GameObject.Find("Dinner").SetActive(false);
        GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
    }
}
