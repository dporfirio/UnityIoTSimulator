using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedObject : HouseObject
{
    // Start is called before the first frame update
    void Start()
    {
        this.player.registerObject(this);
		this.actions = new List<Action>();
		this.activities = new List<Activity>();

		// add list of activities and actions
		Activity sleepActivity = new Sleep(gameObject,this.player);
		this.activities.Add(sleepActivity);
    }

}
