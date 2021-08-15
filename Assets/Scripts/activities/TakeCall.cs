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
        //bool isClose = this.actingObject.GetComponent<OfficePcObject>().QueryPosition();
        bool isClose = this.actingObject.GetComponent<PhoneObject>().QueryPosition();
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
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().TimeFly(200, this);
        GameObject.Find("Robot").GetComponent<RobotController>().called = true;
    }

    public override void EndAct()
    {
        //GameObject.Find("ComputerOffice").GetComponent<PhoneObject>().distanceBound = 0;
        GameObject.Find("Robot").GetComponent<PhoneObject>().distanceBound = 0;

        GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
    }
}
