using Random=System.Random;
using System;
using UnityEngine;
using Pathfinding;

public class MakeCoffeeEvent : ExternalEvent {


	void Start() {

	}

	public override void ReceiveTimeUpdate(int day, int hour, int min, int sec) {
	
		if (hour == 8 && min == 00) { 
			this.Execute();
		}
	}

	public override void Execute() {
		Debug.Log("Executing Making Coffee event!");


		// send the trigger
		this.ehub.AddTrigger("MakeCoffeeTime", true);
	}

	public void SetEventHub(EventHub eh) {
		this.ehub = eh;
	}

}