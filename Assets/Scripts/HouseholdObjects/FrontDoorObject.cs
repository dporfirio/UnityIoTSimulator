using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDoorObject : IoTDevice
{
    // Start is called before the first frame update
    void Start()
    {
    	GameObject g = GameObject.Find("PlayerCanvas");
        this.player = g.GetComponent<Player>();
        this.player.registerObject(this);
		this.actions = new List<HumanAction>();
		this.activities = new List<Activity>();

        // add list of activities and actions
        //Activity leaveActivity = new Leave(gameObject,this.player);
        //this.activities.Add(leaveActivity);
        this.state = "Lock";

        // add list of activities and actions
        HumanAction action = new Lock(gameObject);
        this.actions.Add(action);
        Leave act2 = new Leave(gameObject, this.player);
        this.activities.Add(act2);
    }

}
