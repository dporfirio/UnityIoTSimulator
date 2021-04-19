using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TurnLight : HumanAction
{

	private LightDevice device;

    // Start is called before the first frame update
    public TurnLight(LightDevice device, GameObject lightComponent)
    {
    	this.device = device;
    	this.actingObject = lightComponent;
    	string state = this.device.state;
    	if (state == "on") 
        	this.command = "turn light off";
        else
        	this.command = "turn light on";
        this.description = "flipping light";
    }

    public override void Act() {
    	string state = this.device.state;
    	if (state == "on") {
    		this.device.turnOff();
    		this.command = "turn light on";
    	}
    	else {
    		this.device.turnOn();
    		this.command = "turn light off";
    	}
    }
}
