using System.Collections.Generic;
using UnityEngine;

public class Trigger {

	//private Dictionary<TriggerType,Program> TAP;
	private Dictionary<string, Program> TAP;
	private Dictionary<Program, List<Conditional>> CAP;

	public Trigger() {
		//this.TAP = new Dictionary<TriggerType,Program>();
		this.TAP = new Dictionary<string, Program>();
		this.CAP = new Dictionary<Program, List<Conditional>>();
	}

	public void RegisterActionWithTrigger(string trigger, Program action) {
		this.TAP.Add(trigger, action);
    }

	public void CleanConditionals(Program act)
    {
		List<Conditional> conds;
		if (this.CAP.TryGetValue(act, out conds))
		{
			conds.Clear();
		}
	}

	public void RegisterActionWithConditional(Program action, Conditional cond) {
		List<Conditional> conds;
		if (this.CAP.TryGetValue(action, out conds))
        {
			conds.Add(cond);
        }
			
	}

	//public bool CheckConditional(Conditional cond) {

	//	// lookup if conditional in dictionary
		

	//	return true;
	//}

	public bool CheckTrigger(string trigger) {

		// lookup trigger in dictionary

		// check if allowed to fire

		// if allowed, fire program
		Program value;

		// if allowed, check condition
		List<Conditional> conds;

		// check if it's in dict
		if (this.TAP.TryGetValue(trigger, out value)) {
			Debug.Log(this.CAP.Count);
			if (this.CAP.Count == 0)
            {
				value.Execute();
				return true;
			}


			if(this.CAP.TryGetValue(value, out conds))
            {
				bool tmpAct = false;

				foreach (Conditional cond in conds) // OR
				{
					if (cond.CompareCondition(false)) // AND
					{
						tmpAct = true;
					}
				}

				// FIND!!
				bool tmpStop = false;

				foreach (Conditional cond in conds) // OR
                {
					if (cond.CompareCondition(true)) // AND
					{
						tmpStop = true;
					}
				}
				if (tmpAct && !tmpStop)
                {
					value.Execute();
					return true;
				} else
                {
					Debug.Log("Current beh is banned!");
                }

            }

		}

		return false;

	}

	private class TriggerType {

	}
	
}