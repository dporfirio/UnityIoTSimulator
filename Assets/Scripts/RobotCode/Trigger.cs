using System.Collections.Generic;

public class Trigger {

	//private Dictionary<TriggerType,Program> TAP;
	private Dictionary<string, Program> TAP;

	public Trigger() {
		//this.TAP = new Dictionary<TriggerType,Program>();
		this.TAP = new Dictionary<string, Program>();
	}

	public void RegisterActionWithTrigger(string trigger, Program action) {
		this.TAP.Add(trigger, action);
    }

	public void RegisterActionWithConditional(Conditional cond, Program action) {

	}

	public void CheckConditional(Conditional cond) {

		// lookup if conditional in dictionary

		// check if allowed to fire

		// if allowed, fire program

	}

	public bool CheckTrigger(string trigger) {

		// lookup trigger in dictionary

		// check if allowed to fire

		// if allowed, fire program
		Program value;

		// check if it's in dict
		if (this.TAP.TryGetValue(trigger, out value)) {
			// CheckConditional()
			// skip check restriction for now
			value.Execute();
			return true;
		}

		return false;

	}

	private class TriggerType {

	}
	
}