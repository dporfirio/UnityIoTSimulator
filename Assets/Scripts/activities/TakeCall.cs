using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TakeCall : Activity
{

    // Start is called before the first frame update
    public TakeCall(GameObject any, Player player)
    {
        this.actingObject = any;
        this.player = player;
        this.command = "take phone call";
        this.description = "talking on the phone";
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
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().TimeFlyAndStop(200, this);
    }

    public override void EndAct()
    {
        GameObject.Find("ComputerOffice").GetComponent<PhoneObject>().distanceBound = 0;
    }
}
