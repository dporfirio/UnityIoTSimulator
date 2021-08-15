using UnityEngine;

public class MakeFoodProgram : Program {

	public RobotController rctrl;
	private GameObject stove;

	public void Start() {
		this.rctrl = transform.parent.parent.gameObject.GetComponent<RobotController>();
		this.stove = GameObject.Find("Stove");
	}
	
	public override void Execute() {
		Debug.Log("Execute Food Making!");
		// get package sprite
		

		if (this.stove == null) {
			Debug.Log("WARNING: Stove not found.");
			return;
		}

		// attempt to set destination to package
		GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().ChangeInc(1);
		this.rctrl.SetDestination(this.stove);
		this.rctrl.state = "Make Food";
	}

}