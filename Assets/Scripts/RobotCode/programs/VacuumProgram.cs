using UnityEngine;

public class VacuumProgram : Program {

	public RobotController rctrl;

	public void Start() {
		this.rctrl = transform.parent.parent.gameObject.GetComponent<RobotController>();
	}
	
	public override void Execute() {
		Debug.Log("Execute Vacuum!");

		FindObjectOfType<AudioManager>().Play("Vacuum");

		// attempt to set destinations
		GameObject one = GameObject.Find("Couch_left");
		GameObject two = GameObject.Find("Fridge");
		GameObject three = GameObject.Find("Fridge");
		GameObject four = GameObject.Find("Chair_kitchen2");
		GameObject five = GameObject.Find("Bookshelf2_office");
		GameObject six = GameObject.Find("Lamp_bedroom_master");
		GameObject seven = GameObject.Find("Chair_guest_bedroom");
		GameObject eight = GameObject.Find("Door");
		GameObject home = GameObject.Find("Window_right");
		GameObject[] arr = new GameObject[9] { one, two, three, four, five, six, seven, eight, home };

		GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().ChangeInc(1);

		this.rctrl.SetGoals(arr);
		this.rctrl.state = "Vacuum";
	}

}