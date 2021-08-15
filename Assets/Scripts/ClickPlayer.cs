using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

//Attach this script to an empty gameobject.
//When you click on a sprite with a collider it will tell you it's name.
public class ClickPlayer : MonoBehaviour
{
    public Text currActivityText;
    private PlayerOptions op;
    public Activity currActivity;
    public Player player;
    private List<HouseObject> tmp;
    public SpriteRenderer mark;

    // dummy activity
    private Activity otherActivity;

    private GameObject pler;


    private void Awake()
    {
        GameObject g = GameObject.Find("PlayerOptionPane");
        op = g.GetComponent<PlayerOptions>();

        // initialize the array
        tmp = new List<HouseObject>();

        // set up the dummy activity
        otherActivity = new Other();
        currActivity = otherActivity;
        pler = GameObject.Find("Player");
    }

    void Start()
    {
        currActivity = otherActivity;

    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, pler.transform.position,99F);
        // determine if we clicked a button
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            if (EventSystem.current.currentSelectedGameObject.name.Contains("PlayerOption"))
            {
                EventSystem.current.SetSelectedGameObject(null);
                return;
            }
        }

        // see if we are close to anything
        // get the options
        List<Operation> opts = new List<Operation>();
        List<HouseObject> closeObjects = new List<HouseObject>();

        // if no current activity, show as "other"
        if (!currActivity.CheckActivityConditions())
        {
            //currActivity.EndAct();
            currActivity = otherActivity;
            currActivityText.text = "other";
        }

        // get all houseobjects
        tmp = player.householdObjects;

        // check objects close to the player
        foreach (HouseObject householdObject in tmp)
        {
            bool isClose = householdObject.QueryPosition();

            if (isClose)
            {
                closeObjects.Add(householdObject);
            }
        }

        // show a mark if close to interactable objects
        if (closeObjects.Count != 0)
        {
            mark.enabled = true;
        } else
        {
            mark.enabled = false;
        }

        //If the left mouse button is clicked.
        if (Input.GetMouseButtonDown(0))
        {
            //Get the mouse position on the screen and send a raycast into the game world from that position.
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            int layerMask = LayerMask.GetMask("Player");

            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero,0.1F, layerMask);

            //If something was hit, the RaycastHit2D.collider will not be null.
            if (hit.collider != null)
            {
                if (hit.collider.name == "PlayerClick")
                {
                    foreach (HouseObject householdObject in closeObjects)
                    {
                        foreach (HumanAction act in householdObject.actions)
                        {
                            opts.Add(act);
                        }
                        foreach (Activity act in householdObject.activities)
                        {
                            opts.Add(act);
                        }
                    }
                    op.ToggleOptions(opts);
                }

            }
        }
    }

}



