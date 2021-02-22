using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Sleep : Activity
{

    // Start is called before the first frame update
    public Sleep(GameObject bed, Player player)
    {
    	this.actingObject = bed;
        this.player = player;
        this.command = "sleep";
        this.description = "sleeping";
    }

    public override bool CheckActivityConditions() {
        bool isClose = this.actingObject.GetComponent<BedObject>().QueryPosition();
        if (!isClose) {
            this.EndAct();
            return false;
        }
        else
            return true;
    }

    public override void Act() {
        this.player.UpdateActivity(this);
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().TimeFly(2000);
    }

    public override void EndAct() {

    }
}
