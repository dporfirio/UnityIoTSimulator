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

	void Awake() {
		GameObject g = GameObject.Find("PlayerCanvas");
        player = g.GetComponent<Player>();
	}

    // Start is called before the first frame update
    void Start()
    {
        buttonsToggled = false;
        goList = new List<GameObject>();
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
		        currButtonY += 15;
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

    void ClickButton(Operation act) {
    	RemoveOptions();
    	act.Act();
    }
}
