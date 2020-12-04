using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HouseObject : MonoBehaviour
{

	public Player player;
	public List<Action> actions;
	public List<Activity> activities;
	public float distanceBound;

	// register self with player
	void Awake() {
		GameObject g = GameObject.Find("PlayerCanvas");
        this.player = g.GetComponent<Player>();
	}

    // Start is called before the first frame update
    void Start()
    {
        player.registerObject(this);
    }

    // see how close this object is to the player
    public bool QueryPosition() {
    	bool isClose = false;

    	float dist = Vector2.Distance(this.player.GetPlayerMovement().transform.position,transform.position);
    	if (dist < distanceBound) {
    		isClose = true;
    	}

    	return isClose;
    }


}
