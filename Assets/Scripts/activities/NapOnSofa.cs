using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class RelaxSofa : Activity
{

    // Start is called before the first frame update
    public RelaxSofa(GameObject bed, Player player)
    {
    	this.actingObject = bed;
        this.player = player;
        this.command = "relax on sofa";
        this.description = "relaxing on sofa";
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
