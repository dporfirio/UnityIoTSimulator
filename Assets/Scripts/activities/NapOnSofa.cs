using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class NapSofa : Activity
{

    // Start is called before the first frame update
    public NapSofa(GameObject bed, Player player)
    {
    	this.actingObject = bed;
        this.player = player;
        this.command = "nap on sofa";
        this.description = "napping on sofa";
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
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().StartTimeFly(this, 10);

    }

    public override void EndAct() {
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().act = null;
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().StopCo();
    }
}
