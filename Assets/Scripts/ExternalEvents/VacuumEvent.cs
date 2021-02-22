using Random=System.Random;
using System;
using UnityEngine;
using Pathfinding;

public class VacuumEvent : ExternalEvent {


	void Start() {

	}

	public override void ReceiveTimeUpdate(int day, int hour, int min, int sec) {
		if (day == 0 || day == 2 || day == 4) {
			if (hour == 16 && min == 30) {
				this.Execute();
			}
		}
		
	}

	public override void Execute() {
		Debug.Log("Executing Vacuum event!");


		// send the trigger
		this.ehub.AddTrigger("VacuumTime", true);
	}

	public void SetEventHub(EventHub eh) {
		this.ehub = eh;
	}

}