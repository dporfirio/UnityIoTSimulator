using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SurfInternet : Activity
{

    // Start is called before the first frame update
    public SurfInternet(GameObject any, Player player)
    {
        this.actingObject = any;
        this.player = player;
        this.command = "surf web";
        this.description = "surfing the internet";
    }

    public override bool CheckActivityConditions()
    {
        bool isClose = this.actingObject.GetComponent<OfficePcObject>().QueryPosition();
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
        GameObject.Find("ComputerOffice").GetComponent<OfficePcObject>().state = "on";
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().StartTimeFly(this, 20);
    }

    public override void EndAct()
    {
        GameObject.Find("ComputerOffice").GetComponent<OfficePcObject>().state = "off";
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().act = null;
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().StopCo();
    }
}
