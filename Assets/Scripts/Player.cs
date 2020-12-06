using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

//control the movement of the player with keyboard
public class Player : MonoBehaviour
{

    // activities and activity selection
    public Text currActivityText;
    public Activity currActivity;
    public List<List<string>> activityHistory;
    private PlayerOptions op;
    GraphicRaycaster raycaster;

    // objects in scene
    private List<HouseObject> householdObjects;

    // player icon
    private PlayerMovement pm;

    // dummy activity
    private Activity otherActivity;

    void Awake ()
    {
        // set up the component pane
        this.raycaster = GetComponent<GraphicRaycaster>();
        GameObject g = GameObject.Find("PlayerOptionPane");
        op = g.GetComponent<PlayerOptions>();

        // initialize the array
        householdObjects = new List<HouseObject>();

        // access the player icon
        GameObject pIcon = GameObject.Find("Player");
        pm = pIcon.GetComponent<PlayerMovement>();

        // set up the dummy activity
        otherActivity = new Other();
        currActivity = otherActivity;
    }


    // Start is called before the first frame update
    void Start()
    {
        currActivity = otherActivity;
    }

    // Update is called once per frame
    void Update() {

        // determine if we clicked a button
        if (EventSystem.current.currentSelectedGameObject != null) {
            if (EventSystem.current.currentSelectedGameObject.name.Contains("PlayerOption")) {
                Debug.Log(EventSystem.current.currentSelectedGameObject.name);
                EventSystem.current.SetSelectedGameObject(null);
                return;
            }
        }

        // see if we are close to anything
        // get the options
        List<Operation> opts = new List<Operation>();
        List<HouseObject> closeObjects = new List<HouseObject>();
        foreach (HouseObject householdObject in householdObjects) {
            bool isClose = householdObject.QueryPosition();

            if (isClose) {
                closeObjects.Add(householdObject);
                foreach (Action act in householdObject.actions) {
                    opts.Add(act);
                }
                foreach (Activity act in householdObject.activities) {
                    opts.Add(act);
                }
            }

        }

        if (!currActivity.CheckActivityConditions()) {
            currActivity.EndAct();
            currActivity = otherActivity;
            currActivityText.text = "other";
        }

        // see if the mouse is down
        if (Input.GetMouseButtonDown(0)) {
            // set yp new pointer event
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            List<RaycastResult> results = new List<RaycastResult>();

            // Raycast using the Graphics Raycaster and mouse click position
            pointerData.position = Input.mousePosition;
            this.raycaster.Raycast(pointerData, results);

            // clear the buttons
            if (results.Count == 0) {
                op.ToggleOptions(new List<Operation>());
            }
            else {
                foreach (RaycastResult result in results) {
                    op.ToggleOptions(opts);
                }
            }
        }
    }
    
    public void registerObject(HouseObject go) {
        householdObjects.Add(go);
    }

    // update the current activity
    public void UpdateActivity(Activity act) {
        currActivityText.text = act.description;
        currActivity = act;
    }

    // update the history
    void UpdateHistory(Dictionary<string,string> devStates) {

    }

    public PlayerMovement GetPlayerMovement() {
        return this.pm;
    }
}