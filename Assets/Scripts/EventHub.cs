using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHub : MonoBehaviour
{

	public List<ExternalEvent> externalEvents;

    // Start is called before the first frame update
    void Start()
    {
        // initialize the individual events
        externalEvents = new List<ExternalEvent>();
        externalEvents.Add(this.transform.Find("PackageEvent").GetComponent<PackageEvent>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //updating the external events
    public void UpdateExternalEvents(int day, int hour, int minute, int second) {
    	foreach (ExternalEvent extEvent in externalEvents) {
    		extEvent.ReceiveTimeUpdate(day, hour, minute, second);
    	}
    }
}
