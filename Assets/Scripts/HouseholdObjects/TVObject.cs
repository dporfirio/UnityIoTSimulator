using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVObject : IoTDevice
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
		Activity watchActivity = new WatchTV(gameObject,this.player);
		this.activities.Add(watchActivity);
        this.state = "off";
    }

}
