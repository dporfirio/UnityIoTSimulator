using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightDevice : IoTDevice {

	public float maxIntensity;
	public float minIntensity;

	void Start() {
		GameObject g = GameObject.Find("PlayerCanvas");
        this.player = g.GetComponent<Player>();
		this.player.registerObject(this);
		this.isActive = false;
		this.determineState(); // false is off
		this.actions = new List<Action>();
		this.activities = new List<Activity>();

		// set the max intensity of the light
		this.maxIntensity = 1.0F;
		this.minIntensity = 0.1F;

		// add list of activities and actions
		Action lightAction = new TurnLight(this,gameObject);
		this.actions.Add(lightAction);
	}

	public void updateIntensity() {
		if (this.state == "off")
			gameObject.GetComponent<Light2D>().intensity = this.minIntensity;
		else 
			gameObject.GetComponent<Light2D>().intensity = this.maxIntensity;
	}

	private void determineState() {
		if (gameObject.GetComponent<Light2D>().intensity == this.minIntensity)
			this.state = "off";
		else
			this.state = "on";
	}

	public void turnOn() {
		gameObject.GetComponent<Light2D>().intensity = this.maxIntensity;
		this.state = "on";
	}

	public void turnOff() {
		gameObject.GetComponent<Light2D>().intensity = this.minIntensity;
		this.state = "off";
	}

}