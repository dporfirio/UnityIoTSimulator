using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHub : MonoBehaviour
{

	public List<ExternalEvent> externalEvents;
    public Dictionary<string,int> triggers;

    // Start is called before the first frame update
    void Start()
    {
        // initialize trigger dictionary
        this.triggers = new Dictionary<string,int>();

        // initialize the individual events
        externalEvents = new List<ExternalEvent>();
        PackageEvent pe = this.transform.Find("PackageEvent").GetComponent<PackageEvent>();
        pe.SetEventHub(this);
        externalEvents.Add(pe);

        VacuumEvent ve = this.transform.Find("VacuumEvent").GetComponent<VacuumEvent>();
        ve.SetEventHub(this);
        externalEvents.Add(ve);

        MakeFoodEvent me = this.transform.Find("MakeFoodEvent").GetComponent<MakeFoodEvent>();
        me.SetEventHub(this);
        externalEvents.Add(me);

        PhoneCallEvent pce = this.transform.Find("PhoneCallEvent").GetComponent<PhoneCallEvent>();
        pce.SetEventHub(this);
        externalEvents.Add(pce);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (KeyValuePair<string, int> entry in this.triggers) {
            if (entry.Value == 0)
                RemoveTrigger(entry.Key);
            else if (entry.Value > 0)
                this.triggers[entry.Key] -= 1;
        }
    }

    //updating the external events
    public void UpdateExternalEvents(int day, int hour, int minute, int second) {
    	foreach (ExternalEvent extEvent in externalEvents) {
    		extEvent.ReceiveTimeUpdate(day, hour, minute, second);
    	}
    }

    public void AddTrigger(string trigger, bool persistence) {
        if (persistence)
            this.triggers.Add(trigger,-1);
        else
            this.triggers.Add(trigger,1);   // trigger goes away after 5 update calls
    }

    public void RemoveTrigger(string trigger) {
        this.triggers.Remove(trigger);
    }

    public List<string> GetAllTriggers() {
        List<string> toReturn = new List<string>();

        foreach (KeyValuePair<string, int> entry in this.triggers) {
            toReturn.Add(entry.Key);
        }

        return toReturn;
    }
}
