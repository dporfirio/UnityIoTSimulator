using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class WatchTV : Activity
{

    // Start is called before the first frame update
    public WatchTV(GameObject bed, Player player)
    {
    	this.actingObject = bed;
        this.player = player;
        this.command = "watch TV";
        this.description = "watching TV";
    }

    public override bool CheckActivityConditions() {
        bool isClose = this.actingObject.GetComponent<TVObject>().QueryPosition();
        if (!isClose) {
            this.EndAct();
            return false;
        }
        else
            return true;
    }

    public override void Act() {
        this.player.UpdateActivity(this);
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().StartTimeFly();

    }

    public override void EndAct() {

    }
}
