using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightDevice : IoTDevice {

	void Start() {
		this.player.registerObject(this);
		this.isActive = false;
		this.determineState(); // false is off
		this.actions = new List<Action>();
		this.activities = new List<Activity>();

		// add list of activities and actions
		Action lightAction = new TurnLight(this,gameObject);
		this.actions.Add(lightAction);
	}

	private void determineState() {
		if (gameObject.GetComponent<Light2D>().intensity == 0.1)
			this.state = "off";
		else
			this.state = "on";
	}

	public void turnOn() {
		gameObject.GetComponent<Light2D>().intensity = 1;
		this.state = "on";
	}

	public void turnOff() {
		gameObject.GetComponent<Light2D>().intensity = 0.1F;
		this.state = "off";
	}

}