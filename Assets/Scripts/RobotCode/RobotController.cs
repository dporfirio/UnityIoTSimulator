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
    private Program food;
    private Program phonecall;
	public Trigger triggers;
    private EventHub ehub;
    private GameObject[] goals;
    private int goalIdx;
    private GameObject player;
    public GameObject home;
    private Player playerCanvas;

    private GameObject lunch;
    private GameObject stove;
    private OvenObject oven;
    private GameObject pg;
    private GameObject chair;
    private FridgeObject fridge;

    public string state;

    public bool called;

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
        this.food = GameObject.Find("MakeFoodProgram").gameObject.GetComponent<MakeFoodProgram>();
        this.phonecall = GameObject.Find("PhoneCallProgram").gameObject.GetComponent<PhoneCallProgram>();

        // register each individual trigger
        this.triggers = new Trigger();
        this.triggers.RegisterActionWithTrigger("PackageArrives",this.packageRetrieve);
        this.triggers.RegisterActionWithTrigger("VacuumTime", this.vacuum);
        this.triggers.RegisterActionWithTrigger("MakeFoodTime", this.food);
        this.triggers.RegisterActionWithTrigger("PhoneCalls", this.phonecall);


        this.goals = null;
        this.goalIdx = -1;

        this.state = "Idle";

        this.called = false; // by default

        this.player = GameObject.Find("Player");
        this.playerCanvas = GameObject.Find("PlayerCanvas").GetComponent<Player>();
        this.chair = GameObject.Find("Chair_office");
        this.pg = GameObject.Find("PackageDelivery");
        this.lunch = GameObject.Find("Lunch");
        this.stove = GameObject.Find("Stove");
        this.oven = GameObject.Find("Oven").GetComponent<OvenObject>();
        this.fridge = GameObject.Find("Fridge").GetComponent<FridgeObject>();
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

        Transform tar = this.pathfinder.target;

        switch (this.state)
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
                
                // check if package is picked up
                if (this.pg != null && tar == this.pg.transform)
                {
                    if (Vector2.Distance(this.rg.transform.position, tar.position) <= 1.5F)
                    {
                        Debug.Log("PkgArrival1");
                        this.pg.GetComponent<PgController>().MoveToChair();
                        SetDestination(this.chair);
                    }
                }

                // check if package is put in office
                
                if (this.chair != null && tar == this.chair.transform)
                {
                    if (Vector2.Distance(this.rg.transform.position, this.pathfinder.target.transform.position) <= 2.3F)
                    {
                        Debug.Log("PkgArrival2");
                        SetDestination(home);
                        this.pg.name = "ArrivedPackage";
                        this.state = "Idle";
                    }
                }
                break;
            case "Remind Phonecall":
                // check if reminded user already
                
                if (this.player != null && tar == this.player.transform)
                {
                    if (Vector2.Distance(this.rg.transform.position, this.pathfinder.target.transform.position) <= 2F)
                    {

                        FindObjectOfType<AudioManager>().Stop("PhoneCall3");
                        ResetDest();
                        StartCoroutine(loopWaitCall(home));
                    }
                }
                break;
            case "Make Food":
                // move to the stove first
                if (this.stove != null && tar == this.stove.transform)
                {
                    if (Vector2.Distance(this.rg.transform.position, tar.position) <= 2F)
                    {
                        Debug.Log("StoveArrival");
                        // make sure meal is not made or is being made
                        if (!this.lunch.GetComponent<LunchObject>().enabled && this.playerCanvas.currActivityText.text != "preparing a meal") { 
                            this.stove.GetComponent<StoveObject>().state = "on";
                            this.stove.GetComponent<StoveObject>().removeAct();
                            this.fridge.removeAct();
                            this.oven.removeAct();
                            ResetDest();
                            StartCoroutine(loopWaitFood(home));
                        } else
                        {
                            ResetDest();
                            SetDestination(home);
                        }


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


    IEnumerator loopWaitFood(GameObject home)
    {
        int cnt = 0;

        while (cnt <= 100)
        {
            yield return new WaitForSeconds(0.1F);
            cnt += 1;
        }
        SetDestination(home);
        this.state = "Idle";
        Debug.Log("finishMakeFood");
        this.stove.GetComponent<StoveObject>().state = "off";
        this.lunch.GetComponent<SpriteRenderer>().enabled = true;
        this.lunch.GetComponent<LunchObject>().distanceBound = 3;
        this.lunch.GetComponent<LunchObject>().enabled = true;
    }

    IEnumerator loopWaitCall(GameObject home)
    {
        while (this.called == false)
        {
            yield return new WaitForSeconds(0.1F);
        }
        SetDestination(home);
        this.state = "Idle";
        this.called = false;
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
