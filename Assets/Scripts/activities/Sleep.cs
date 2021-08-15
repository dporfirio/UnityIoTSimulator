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
        Debug.Log("close? " + isClose);
        if (!isClose) {
            this.EndAct();
            return false;
        }
        else
            return true;
    }

    public override void Act() {
        this.player.UpdateActivity(this);
        //GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().TimeFlyAndStop(4500, this);
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().StartTimeFly(this, 40);
    }

    public override void EndAct() {
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().act = null;
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().StopCo();
    }
}
