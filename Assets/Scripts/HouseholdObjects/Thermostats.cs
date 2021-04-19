using System.Collections;
using Random = System.Random;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Thermostats : IoTDevice
{
    private Random rand;
    private int temp;
    private int prevHour;
    // Start is called before the first frame update
    void Start()
    {
        this.isActive = true;
        this.rand = new Random();
        this.temp = 0;
        this.prevHour = GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().hour;
    }

    void Update()
    {
        if (this.prevHour != GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().hour)
        {
            // approximate the normal distribution using the Box-Muller transform
            double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            this.temp = (int)Math.Round(70 + 2 * randStdNormal); //random normal(mean,stdDev^2)
            this.state = "" + this.temp;
            this.prevHour = GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().hour;
        }


    }

}
