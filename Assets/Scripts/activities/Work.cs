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
        if (!isClose || this.player.currEnergy <= 10) {
            this.EndAct();
            return false;
        }
        else
            return true;
    }

    public override void Act() {

        if (GameObject.Find("PlayerCanvas").GetComponent<Player>().currEnergy > 10) { 
            this.player.UpdateActivity(this);
            //GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().TimeFly(1000);
            GameObject.Find("ComputerOffice").GetComponent<OfficePcObject>().state = "on";
            GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().StartTimeFly(this, 30);
        } else
        {
            Debug.Log("cannot work");
        }
    }

    public override void EndAct() {
        GameObject.Find("ComputerOffice").GetComponent<OfficePcObject>().state = "off";
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().act = null;
        GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().StopCo();
    }
}
