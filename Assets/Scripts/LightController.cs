using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine;

public class LightController : MonoBehaviour
{

    // currently all lights are hard-coded, unfortunately
    public Light2D Sun;
	public Light2D EastLight;
	public Light2D WestLight;
    public Light2D MasterBath;
    public Light2D GuestBed;
    public Light2D MasterBed;
    public Light2D Kitchen;
    public Light2D Dining;
    public Light2D Living;
    public Light2D Foyer;
    public Light2D Office;
    public Light2D Hallway1;
    public Light2D Hallway2;
    public Light2D EntranceBath;

    public LightDevice MasterBathDev;
    public LightDevice GuestBedDev;
    public LightDevice MasterBedDev;
    public LightDevice KitchenDev;
    public LightDevice DiningDev;
    public LightDevice LivingDev;
    public LightDevice FoyerDev;
    public LightDevice OfficeDev;
    public LightDevice Hallway1Dev;
    public LightDevice Hallway2Dev;
    public LightDevice EntranceBathDev;
    public List<LightDevice> lightDevs;

    // Start is called before the first frame update
    void Start()
    {
        lightDevs = new List<LightDevice>();
        lightDevs.Add(MasterBath.gameObject.GetComponent<LightDevice>());
        lightDevs.Add(GuestBed.gameObject.GetComponent<LightDevice>());
        lightDevs.Add(MasterBed.gameObject.GetComponent<LightDevice>());
        lightDevs.Add(Kitchen.gameObject.GetComponent<LightDevice>());
        lightDevs.Add(Dining.gameObject.GetComponent<LightDevice>());
        lightDevs.Add(Living.gameObject.GetComponent<LightDevice>());
        lightDevs.Add(Foyer.gameObject.GetComponent<LightDevice>());
        lightDevs.Add(Office.gameObject.GetComponent<LightDevice>());
        lightDevs.Add(Hallway1.gameObject.GetComponent<LightDevice>());
        lightDevs.Add(Hallway2.gameObject.GetComponent<LightDevice>());
        lightDevs.Add(EntranceBath.gameObject.GetComponent<LightDevice>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLight(int hour, int minute, int second) {

        int totalSeconds = 60*60*24; // seconds * minutes * hours

        // calculate percentage of day completion
        int completed = (60*60*hour) + (60*minute) + second;

        // percent completed
        double percentCompleted = (completed*1.0)/totalSeconds;

        if (percentCompleted < 0.23) {
            Sun.intensity = 0.1F;
        }
        else if (percentCompleted < 0.375) {

            // west light
            WestLight.intensity = 0.8F;

            // east light
            EastLight.intensity = (float) (0.1 + 1.1*((percentCompleted-0.23)/(0.375-0.23)));

            // sun
            Sun.intensity = (float) (0.1 + 0.9*((percentCompleted-0.23)/(0.375-0.23)));;

            // electric lights
            foreach (LightDevice light in lightDevs) {
                light.minIntensity = (float) (0.1 + 0.7*((percentCompleted-0.23)/(0.375-0.23)));
                light.updateIntensity();
            }
        }
        else if (percentCompleted < 0.50) {

            // west light
            WestLight.intensity = 0.8F;

            // east light
            EastLight.intensity = (float) (1.2 - 0.4*((percentCompleted-0.375)/(0.5-0.375)));

        }
        else if (percentCompleted < 0.625) {

            // west light
            WestLight.intensity = (float) (0.8 + 0.4*((percentCompleted-0.5)/(0.625-0.5)));

            // east light
            EastLight.intensity = 0.8F;

        }
        else if (percentCompleted < 0.77) {

            // west light
            WestLight.intensity = (float) (1.2 - 1.1*((percentCompleted-0.625)/(0.77-0.625)));

            // east light
            EastLight.intensity = (float) (0.8 - 0.7*((percentCompleted-0.75)/(0.77-0.75)));

            // sun
            Sun.intensity = (float) (1.0 - 0.9*((percentCompleted-0.625)/(0.77-0.625)));

            // electric lights
            foreach (LightDevice light in lightDevs) {
                light.minIntensity = (float) (0.7 - 0.6*((percentCompleted-0.625)/(0.77-0.625)));
                light.updateIntensity();
            }

        }
        else {
            Sun.intensity = 0.1F;
        }
    }
}
