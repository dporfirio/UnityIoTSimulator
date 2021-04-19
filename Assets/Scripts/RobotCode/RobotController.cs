using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RobotController : MonoBehaviour
{

	public AIDestinationSetter pathfinder;
	private GameObject rg;
	private Program packageRetrieve;
    private Program vacuum;
    //private Program coffee;
    private Program phonecall;
	public Trigger triggers;
    private EventHub ehub;
    private GameObject[] goals;
    private int goalIdx;

    public string state;

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
        this.vacuum = GameObject.Find("VacuumProgram").gameObject.GetComponent<VacuumProgram>();
        //this.coffee = GameObject.Find("MakeCoffeeProgram").gameObject.GetComponent<MakeCoffeeProgram>();
        this.phonecall = GameObject.Find("PhoneCallProgram").gameObject.GetComponent<PhoneCallProgram>();

        // register each individual trigger
        this.triggers = new Trigger();
        this.triggers.RegisterActionWithTrigger("PackageArrives",this.packageRetrieve);
        this.triggers.RegisterActionWithTrigger("VacuumTime", this.vacuum);
        //this.triggers.RegisterActionWithTrigger("MakeCoffeeTime", this.coffee);
        this.triggers.RegisterActionWithTrigger("PhoneCalls", this.phonecall);


        this.goals = null;
        this.goalIdx = -1;

        this.state = "Idle";
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

        GameObject home = GameObject.Find("Window_right");
        Transform tar = this.pathfinder.target;

        switch(this.state)
        {
            case "Vacuum":
                // check if all goals have been completed
                if (goalIdx != -1)
                {
                    if (Vector2.Distance(this.rg.transform.position, this.pathfinder.target.transform.position) <= 3.2F)
                    {
                        // unfinish all goals yet
                        if (goalIdx != goals.Length - 1)
                        {
                            Debug.Log("Complete current goal");
                            goalIdx += 1;
                            SetDestination(goals[goalIdx]);
                        }
                        else
                        {
                            Debug.Log("Finish all goals");
                            goalIdx = -1;
                            goals = null;
                            FindObjectOfType<AudioManager>().Stop("Vacuum");
                            this.state = "Idle";
                        }

                    }
                }
                break;
            case "Retrieve Package":
                GameObject pg = GameObject.Find("PackageDelivery");
                // check if package is picked up
                if (pg != null && tar == pg.transform)
                {
                    if (Vector2.Distance(this.rg.transform.position, tar.position) <= 1.5F)
                    {
                        Debug.Log("PkgArrival1");
                        GameObject.Find("PackageDelivery").GetComponent<PgController>().MoveToChair();
                        SetDestination(GameObject.Find("Chair_office"));
                    }
                }

                // check if package is put in office
                GameObject chair = GameObject.Find("Chair_office");
                if (chair != null && tar == chair.transform)
                {
                    if (Vector2.Distance(this.rg.transform.position, this.pathfinder.target.transform.position) <= 2.3F)
                    {
                        Debug.Log("PkgArrival2");
                        SetDestination(home);
                        pg.name = "ArrivedPackage";
                        this.state = "Idle";
                    }
                }
                break;
            case "Remind Phonecall":
                // check if reminded user already
                GameObject player = GameObject.Find("Player");
                if (player != null && tar == player.transform)
                {
                    if (Vector2.Distance(this.rg.transform.position, this.pathfinder.target.transform.position) <= 2F)
                    {
                 
                        FindObjectOfType<AudioManager>().Stop("PhoneCall3");
                        SetDestination(home);
                        this.state = "Idle";
                    }
                }
                break;
        }

        // check if arrives home
        if (tar == home.transform)
        {
            if (Vector2.Distance(this.rg.transform.position, this.pathfinder.target.transform.position) <= 1.3F)
            {
                Debug.Log("Home");
                ResetDest();
            }
        }
    }

    public void SetDestination(GameObject any) {
        //this.pathfinder.target = this.rg.transform;
        this.pathfinder.target = any.transform;
    }

    public void ResetDest()
    {

        this.pathfinder.target = null;
    }

    public void SetGoals(GameObject[] any)
    {
        goals = any;
        goalIdx = 0;
        SetDestination(goals[goalIdx]);
    }

    public void HearTrigger(string trigger) {

    }
}
