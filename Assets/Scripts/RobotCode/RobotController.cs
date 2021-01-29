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
            //foreach (string ev in events) {
            for (int i = events.Count - 1; i >= 0; i--) {
                string ev = events[i];
                if (this.triggers.CheckTrigger(ev)) {
                    events.RemoveAt(i);
                    this.ehub.RemoveTrigger(ev);
                }
            }
        }
        
        // check if package is picked up
        if (this.pathfinder.target != null)
        {
            if (Vector2.Distance(this.rg.transform.position, this.pathfinder.target.transform.position) <= 0.1f)
            {
                Debug.Log("Arrival");
                this.pathfinder.target = null;
                GameObject.Find("PackageDelivery").GetComponent<PgController>().SetDestination();

                this.pathfinder.target = GameObject.Find("Chair_office").transform;
            }
            
        }

    }

    public void SetDestination(GameObject package) {
        //this.pathfinder.target = this.rg.transform;
        Debug.Log(package.transform);
        this.pathfinder.target = package.transform;
    }

    public void HearTrigger(string trigger) {

    }
}
