using UnityEngine;
using System.Collections;

public abstract class ExternalEvent : MonoBehaviour {

	// keep track of the event hub
	public EventHub ehub;
	
	// receive updates from the clock
	public abstract void ReceiveTimeUpdate(int day, int hour, int min, int sec);

	// execute the event
	public abstract void Execute();

}