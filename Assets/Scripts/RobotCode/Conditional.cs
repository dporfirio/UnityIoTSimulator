using System.Collections.Generic;
using UnityEngine;
public class Conditional {
	private Dictionary<IoTDevice, string> stoppingDeviceStates;

	public void RegisterStopCondition(string dev, string state)
	{
        IoTDevice[] devices = Object.FindObjectsOfType<IoTDevice>();
        IoTDevice selectedDevice = null;
        foreach (IoTDevice device in devices)
        {
            if (device.name == dev)
            {
                selectedDevice = device;
            } else
            {
                Debug.Log("Cannot find the device match with the device name sent from the backend (This shouldn't happen unless new devices are added)");
            }
        }

        this.stoppingDeviceStates.Add(selectedDevice, state);
	}

	public bool CompareStopCondition()
    {
        // true means STOP!

        IoTDevice[] devices = Object.FindObjectsOfType<IoTDevice>();

        string tmpState;
        foreach (IoTDevice device in devices)
        {
            if (this.stoppingDeviceStates.TryGetValue(device, out tmpState))
            {
                if (device.state != tmpState) // check AND
                {
                    return false;
                }
            }
        }

        return true;
    }
}