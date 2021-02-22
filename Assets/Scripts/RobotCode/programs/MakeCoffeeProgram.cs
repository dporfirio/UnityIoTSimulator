using UnityEngine;

public class MakeCoffeeProgram : Program {

	public RobotController rctrl;

	public void Start() {
		this.rctrl = transform.parent.parent.gameObject.GetComponent<RobotController>();
	}
	
	public override void Execute() {
		Debug.Log("Execute Coffee Making!");
		// get package sprite
		GameObject drink = GameObject.Find("CoffeeMaker");

		if (drink == null) {
			Debug.Log("WARNING: PackageDelivery not found. Could it have been instantaneously retrieved?");
			return;
		}

        // attempt to set destination to package
        this.rctrl.SetDestination(drink);
    }

}