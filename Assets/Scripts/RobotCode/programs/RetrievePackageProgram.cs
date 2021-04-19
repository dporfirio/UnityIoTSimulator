using UnityEngine;

public class RetrievePackageProgram : Program {

	public RobotController rctrl;

	public void Start() {
		this.rctrl = transform.parent.parent.gameObject.GetComponent<RobotController>();
	}
	
	public override void Execute() {
		Debug.Log("Execute Retrieve Package!");
		// get package sprite
		GameObject pg = GameObject.Find("PackageDelivery");

		if (pg == null) {
			Debug.Log("WARNING: PackageDelivery not found. Could it have been instantaneously retrieved?");
			return;
		}

        // attempt to set destination to package
        this.rctrl.SetDestination(pg);
		this.rctrl.state = "Retrieve Package";
	}

}