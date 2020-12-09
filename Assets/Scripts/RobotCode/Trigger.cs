using System.Collections.Generic;

public class Trigger {

	private Dictionary<TriggerType,Program> TAP;

	public Trigger() {
		this.TAP = new Dictionary<TriggerType,Program>();
	}

	public void RegisterActionWithTrigger(string trigger, Program action) {

	}

	public void RegisterActionWithConditional(Conditional cond, Program action) {

	}

	public void CheckConditional(Conditional cond) {

		// lookup if conditional in dictionary

		// check if allowed to fire

		// if allowed, fire program

	}

	public void CheckTrigger(string trigger) {

		// lookup trigger in dictionary

		// check if allowed to fire

		// if allowed, fire program 

	}

	private class TriggerType {

	}
	
}