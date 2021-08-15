using Random=System.Random;
using System;
using UnityEngine;
using Pathfinding;

public class MakeFoodEvent : ExternalEvent {


	void Start() {

	}

	public override void ReceiveTimeUpdate(int day, int hour, int min, int sec) {
	
		if ((hour == 11 && min == 30) || (hour == 17 && min == 30)) { 
			this.Execute();
		}
	}

	public override void Execute() {

        // send the trigger
        this.ehub.AddTrigger("MakeFoodTime", true);
    }

	public void SetEventHub(EventHub eh) {
		this.ehub = eh;
	}

}