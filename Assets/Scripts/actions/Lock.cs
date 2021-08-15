using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Lock : HumanAction
{

    // Start is called before the first frame update
    public Lock(GameObject any)
    {
        this.actingObject = any;

        string state = any.GetComponent<FrontDoorObject>().state;
        if (state == "Lock")
            this.command = "unlock frontdoor";
        else
            this.command = "lock frontdoor";
        this.description = "lock/unlock";
    }


    public override void Act()
    {
        GameObject dw = GameObject.Find("Door");
        string state = dw.GetComponent<FrontDoorObject>().state;
        if (state == "Lock")
        {
            dw.GetComponent<FrontDoorObject>().state = "Unlock";
            this.command = "lock frontdoor";
        }
        else
        {
            dw.GetComponent<FrontDoorObject>().state = "Lock";
            this.command = "unlock frontdoor";
        }

    }

}
