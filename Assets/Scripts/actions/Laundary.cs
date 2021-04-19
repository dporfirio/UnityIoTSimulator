using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Laundary : HumanAction
{

    // Start is called before the first frame update
    public Laundary(GameObject any)
    {
        this.actingObject = any;

        string state = any.GetComponent<WasherObject>().state;
        if (state == "on")
            this.command = "turn off washer";
        else
            this.command = "turn on washer";
        this.description = "doing laundry";
    }


    public override void Act()
    {
        GameObject g = GameObject.Find("PlayerCanvas");
        Player player = g.GetComponent<Player>();

        // send action to the backend
        player.UpdateAction(this);

        GameObject dw = GameObject.Find("Washer");
        string state = dw.GetComponent<WasherObject>().state;
        if (state == "on")
        {
            dw.GetComponent<WasherObject>().state = "off";
            this.command = "turn on washer";
        }
        else
        {
            dw.GetComponent<WasherObject>().state = "on";
            this.command = "turn off washer";
        }

    }

}
