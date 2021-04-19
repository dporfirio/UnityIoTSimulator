using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Leave : Activity
{

    // Start is called before the first frame update
    public Leave(GameObject bed, Player player)
    {
    	this.actingObject = bed;
        this.player = player;
        this.command = "leave";
        this.description = "leaving";
    }

    public override bool CheckActivityConditions() {
        bool isClose = this.actingObject.GetComponent<FrontDoorObject>().QueryPosition();
        if (!isClose) {
            this.EndAct();
            return false;
        }
        else
            return true;
    }

    public override void Act() {
        this.player.UpdateActivity(this);
    }

    public override void EndAct() {

    }
}
