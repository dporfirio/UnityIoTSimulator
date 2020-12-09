using Random=System.Random;
using System;
using UnityEngine;

public class PackageEvent : ExternalEvent {

	private int currDay;
	private int selectedHour;
	private int selectedMin;
	private Random rand;
	private bool willReceive;

	public PackageEvent() {
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
				this.selectedHour = (int)Math.Round(13 + 1.5 * randStdNormal); //random normal(mean,stdDev^2)

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
		Debug.Log("Executing package behavior!");
	}

	private bool DetermineIfPackageReceivedOnDay() {
		double p = 0.0;

		if (this.currDay == 0) { // Sunday
		p = 0.0;
		}
		else if (this.currDay == 7) { // Saturday
			p = 0.3;
		}
		else {
			p = 0.6;
		}

		double a = rand.NextDouble();
		if (a>p)
			return true;
		return false;
	}

}