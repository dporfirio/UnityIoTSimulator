using Random=System.Random;
using System;
using UnityEngine;
using Pathfinding;

public class PackageEvent : ExternalEvent {

	private int currDay;
	private int selectedHour;
	private int selectedMin;
	private Random rand;
	private bool willReceive;

	// UI components
	public GameObject packagePrefab;

	void Start() {
		this.currDay = -1;
		this.selectedHour = -1;
		this.selectedMin = -1;
		this.selectedMin = -1;
		this.willReceive = false;
		this.rand = new Random();
	}

	public override void ReceiveTimeUpdate(int day, int hour, int min, int sec) {
		// set the current day and probability that a package will be received
		if (this.currDay == -1 || this.currDay != day) {
			this.currDay = day;
			this.willReceive = this.DetermineIfPackageReceivedOnDay();

			// if will receive, then determine the time of receipt
			if (this.willReceive) {

				// uniformly determine a time-of-day
				this.selectedMin = this.rand.Next(60);

				// approximate the normal distribution using the Box-Muller transform
				double u1 = 1.0-rand.NextDouble(); //uniform(0,1] random doubles
				double u2 = 1.0-rand.NextDouble();
				double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
				             Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
				this.selectedHour = (int)Math.Round(13 + randStdNormal); //random normal(mean,stdDev^2) // range from 10 to 16

				Debug.Log("Package selected to be received at: " + this.selectedHour + " " + this.selectedMin);
			}
		}

		if (this.willReceive) {
			if (hour == this.selectedHour && min == this.selectedMin) {
				this.Execute();
				this.willReceive = false;
			}
		}
		
	}

	public override void Execute() {
		Debug.Log("Executing package event!");

		// add the package
		GameObject package = Instantiate(packagePrefab);
		package.name = "PackageDelivery";

		// send the trigger
		this.ehub.AddTrigger("PackageArrives", true);
	}

	public void SetEventHub(EventHub eh) {
		this.ehub = eh;
	}

	private bool DetermineIfPackageReceivedOnDay() {
		double p = 0.0;
		if (this.currDay == 1) { // Mon
			p = 0.0;
		}
		else if (this.currDay == 7) { // Sun
			p = 0.8;
		}
		else {
			p = 0.4;
		}

		double a = rand.NextDouble();
		if (a>p)
			return true;
		return false;
	}

}