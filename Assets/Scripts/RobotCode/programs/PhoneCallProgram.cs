using UnityEngine;

public class PhoneCallProgram : Program {

	public RobotController rctrl;

	public void Start() {
		this.rctrl = transform.parent.parent.gameObject.GetComponent<RobotController>();
	}
	
	public override void Execute() {
		Debug.Log("Execute Reminding PhoneCall!");
		
        // attempt to set destination to package
        this.rctrl.SetDestination(GameObject.Find("Player"));
		FindObjectOfType<AudioManager>().Play("PhoneCall3");

		GameObject.Find("ActivityPanel").GetComponent<TimeUpdater>().ChangeInc(1);
		this.rctrl.state = "Remind Phonecall";
	}
}