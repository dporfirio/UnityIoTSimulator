using Random=System.Random;
using System;
using UnityEngine;
using Pathfinding;

public class PhoneCallEvent : ExternalEvent {
	private int cnt;
	private int selectedHour;
	private int selectedMin;
	private Random rand;
	private bool hasDecided;

	void Start() {
		this.selectedHour = -1;
		this.selectedMin = -1;
		this.selectedMin = -1;
		this.rand = new Random();
        //this.cnt = this.rand.Next(1,3);
        this.cnt = 2;
        this.hasDecided = false;
	}

	public override void ReceiveTimeUpdate(int day, int hour, int min, int sec) {
		// random times of phone call from 0-3
		if (!this.hasDecided && this.cnt > 0) { 

			// uniformly determine a time-of-day
			this.selectedMin = this.rand.Next(60);

			// approximate the normal distribution using the Box-Muller transform
			double u1 = 1.0-rand.NextDouble(); //uniform(0,1] random doubles
			double u2 = 1.0-rand.NextDouble();
			double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
				            Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
			if (this.cnt == 2)
            {
				this.selectedHour = (int)Math.Round(10 + 0.5 *randStdNormal); //random normal(mean,stdDev^2) // range from 8.5 to 11.5
				this.hasDecided = true;
				Debug.Log("PhoneCall selected to be received at: " + this.selectedHour + " " + this.selectedMin);
			} else if (this.cnt == 1)
            {
				this.selectedHour = (int)Math.Round(14 + 0.5 * randStdNormal); //random normal(mean,stdDev^2) // range from 12.5 to 15.5
				Debug.Log("PhoneCall selected to be received at: " + this.selectedHour + " " + this.selectedMin);
				this.hasDecided = true;
			}
		}

		if (this.cnt > 0) {
			if (hour == this.selectedHour && min == this.selectedMin) {
				this.Execute();
				this.hasDecided = false;
				this.cnt--;
			}
		}
		
	}

	public override void Execute() {
		Debug.Log("Executing phone call event!");
		this.ehub.AddTrigger("PhoneCalls", true);
		//GameObject.Find("ComputerOffice").GetComponent<PhoneObject>().distanceBound = 5;
		GameObject.Find("Robot").GetComponent<PhoneObject>().distanceBound = 5;
	}

	public void SetEventHub(EventHub eh) {
		this.ehub = eh;
	}

}