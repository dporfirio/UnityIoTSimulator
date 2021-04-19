using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class WashDish : HumanAction
{

    // Start is called before the first frame update
    public WashDish(GameObject any)
    {
        this.actingObject = any;

        string state = any.GetComponent<DishwasherObject>().state;
        if (state == "on")
            this.command = "turn off dishwasher";
        else
            this.command = "turn on dishwasher";
        this.description = "washing dishes";
    }


    public override void Act()
    {
        GameObject g = GameObject.Find("PlayerCanvas");
        Player player = g.GetComponent<Player>();

        // send action to the backend
        player.UpdateAction(this);

        GameObject dw = GameObject.Find("Dishwasher");
        string state = dw.GetComponent<DishwasherObject>().state;
        if (state == "on")
        {
            dw.GetComponent<DishwasherObject>().state = "off";
            this.command = "turn on dishwasher";
        }
        else
        {
            dw.GetComponent<DishwasherObject>().state = "on";
            this.command = "turn off dishwasher";
        }

    }

}
