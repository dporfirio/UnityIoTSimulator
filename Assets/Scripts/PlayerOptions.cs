using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOptions : MonoBehaviour
{

	// sample button
	public GameObject buttonPrefab;
	public GameObject canvasParent;
	private bool buttonsToggled;
	private List<GameObject> goList;
	private Player player;

    public GameObject confirmUI;
    public Text promtText;

    private int confirmed;

	void Awake() {
		GameObject g = GameObject.Find("PlayerCanvas");
        player = g.GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        buttonsToggled = false;
        goList = new List<GameObject>();

        //confirmUI = GameObject.Find("ConfirmUI");
        confirmUI.SetActive(false);

        confirmed = 0; // 0 for default, -1 for decline, 1 for confirm
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleOptions(List<Operation> buttonArgs) {
    	if (buttonArgs.Count == 0) {
    		foreach (GameObject sample in goList) {
    			Destroy(sample);
    		}
    		goList.Clear();
    		buttonsToggled = false;
    	}
    	else if (!buttonsToggled) {
    		int currButtonY = 0;
    		foreach (Operation act in buttonArgs) {
    			string str = act.command;
		    	GameObject sample = Instantiate(buttonPrefab);
		    	//sample.GetComponentsInChildren<Button>()[0].onClick.AddListener(delegate{
                sample.GetComponent<Toggle>().onValueChanged.AddListener(delegate {
                    this.ClickButton(act);
                });
		    	Text txt = sample.GetComponentsInChildren<Image>()[0].GetComponentsInChildren<Text>()[0];
		    	txt.text = str;
		        sample.transform.SetParent(canvasParent.transform, false);
		        sample.transform.position += new Vector3(0,currButtonY,0);
		        goList.Add(sample);
		        buttonsToggled = true;
		        currButtonY += 40;
	    	}
    	}
    	else { 
    		foreach (GameObject sample in goList) {
    			Destroy(sample);
    		}
    		goList.Clear();
    		buttonsToggled = false;
    	} 
    }

    void RemoveOptions() {
    	foreach (GameObject sample in goList) {
			Destroy(sample);
		}
		goList.Clear();
		buttonsToggled = false;
    }

    public void ClickCancel()
    {
        confirmed = -1;
    }

    public void ClickConfirm()
    {
        confirmed = 1;

    }

    void ClickButton(Operation act) {
    	RemoveOptions();
        confirmUI.SetActive(true);
        promtText.text = "Please confirm if you want to perform: " + act.description;

        StartCoroutine(loopWaitConfirm(act));
    }

    IEnumerator loopWaitConfirm(Operation act)
    {
        while (confirmed == 0)
        {
            yield return new WaitForSeconds(0.1F);
        }

        if (confirmed == -1)
        {
            confirmUI.SetActive(false);
        }
        else if (confirmed == 1)
        {
            confirmUI.SetActive(false);
            List<Operation> blank = new List<Operation>();
            this.ToggleOptions(blank);
            act.Act();
        }
        confirmed = 0;
    }
}
