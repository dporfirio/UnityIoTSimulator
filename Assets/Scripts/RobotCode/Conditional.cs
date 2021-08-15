using System.Collections.Generic;
using UnityEngine;
public class Conditional {
	private Dictionary<IoTDevice, string> stoppingDeviceStates;
    private Dictionary<IoTDevice, string> actDeviceStates;

    public void RegisterCondition(string dev, string state, bool stop)
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
        if (stop)
        {
            this.stoppingDeviceStates.Add(selectedDevice, state);
        } else
        {
            this.actDeviceStates.Add(selectedDevice, state);
        }
        
	}

	public bool CompareCondition(bool stop)
    {

        // true means STOP!
        IoTDevice[] devices = Object.FindObjectsOfType<IoTDevice>();

        string tmpState;
        foreach (IoTDevice device in devices)
        {
            if (stop)
            {
                if (this.stoppingDeviceStates.TryGetValue(device, out tmpState))
                {
                    if (device.state != tmpState) // check AND
                    {
                        return false;
                    }
                }
            } else
            {
                if (this.actDeviceStates.TryGetValue(device, out tmpState))
                {
                    if (device.state != tmpState) // check AND
                    {
                        return false;
                    }
                }
            }

        }

        return true;
    }
}