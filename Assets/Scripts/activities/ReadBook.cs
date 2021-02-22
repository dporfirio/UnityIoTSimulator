using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ReadBook : Activity
{

    // Start is called before the first frame update
    public ReadBook(GameObject book, Player player)
    {
    	this.actingObject = book;
        this.player = player;
        this.command = "read book";
        this.description = "reading books";
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
