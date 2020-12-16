using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RobotController : MonoBehaviour
{

	public AIDestinationSetter pathfinder;
	private GameObject rg;
	private Program packageRetrieve;
	private Trigger triggers;
    private EventHub ehub;

    // Start is called before the first frame update
    void Start()
    {

        // get event hub
        this.ehub = GameObject.Find("EventHub").gameObject.GetComponent<EventHub>();

    	// get robot sprite
		this.rg = GameObject.Find("RobotGraphics");

    	// get access to the pathfinder
    	this.pathfinder = this.gameObject.GetComponent<AIDestinationSetter>();
        
        // setup each individual program that the robot can run
        this.packageRetrieve = GameObject.Find("RetrievePackageProgram").gameObject.GetComponent<RetrievePackageProgram>();

        // register each individual trigger
        this.triggers = new Trigger();
        this.triggers.RegisterActionWithTrigger("PackageArrives",this.packageRetrieve);
    }

    // Update is called once per frame
    void Update()
    {
        // get any events from the hub
        List<string> events = this.ehub.GetAllTriggers();
        if (events.Count > 0) {
            foreach (string ev in events) {
                
            }
        }

    }

    public void SetDestination(GameObject package) {
    	this.pathfinder.target = this.rg.transform;
    }

    public void HearTrigger(string trigger) {

    }
}
