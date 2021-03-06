using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Work : Activity
{

    // Start is called before the first frame update
    public Work(GameObject chair, Player player)
    {
    	this.actingObject = chair;
        this.player = player;
        this.command = "work";
        this.description = "working";
    }

    public override bool CheckActivityConditions() {
        bool isClose = this.actingObject.GetComponent<OfficeChairObject>().QueryPosition();
        if (!isClose) {
            this.EndAct();
            return false;
        }
        else
            return true;
    }

    public override void Act() {
        this.player.UpdateActivity(this);
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().TimeFly(1000);
    }

    public override void EndAct() {

    }
}
