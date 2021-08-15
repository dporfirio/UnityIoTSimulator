using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PgController : MonoBehaviour
{

	public AIDestinationSetter pathfinder;
	private GameObject pkg;
    private GameObject rb;
	private Program packageRetrieve;
    private EventHub ehub;
    private AIPath robot;

    // Start is called before the first frame update
    void Start()
    {
        // get event hub
        this.ehub = GameObject.Find("EventHub").gameObject.GetComponent<EventHub>();

    	// get package sprite
		this.pkg = GameObject.Find("PackageDelivery");

        // get access to the pathfinder
        this.pathfinder = this.gameObject.GetComponent<AIDestinationSetter>();
        this.robot = GameObject.Find("Robot").gameObject.GetComponent<AIPath>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (this.gameObject != null)
        {
            //Debug.Log(this.gameObject);
            // check if speed is updated:

            AIPath tmp = this.gameObject.GetComponent<AIPath>();
        
            tmp.maxSpeed = this.robot.maxSpeed;
            tmp.maxAcceleration = this.robot.maxAcceleration;
            tmp.slowdownDistance = this.robot.slowdownDistance;

        }
        // check if package is picked up
        if (this.pathfinder.target != null)
        {
            if (Vector2.Distance(this.pkg.transform.position, this.pathfinder.target.transform.position) <= 1.5f)
            {
                this.pathfinder.target = null;
                pkg.GetComponent<BoxCollider2D>().enabled = true;
            }
        }

    }

    public void MoveToChair() {
        //this.pathfinder.target = this.rg.transform;

        this.pathfinder.target = GameObject.Find("Chair_office").transform;
    }

    public void HearTrigger(string trigger) {

    }
}
