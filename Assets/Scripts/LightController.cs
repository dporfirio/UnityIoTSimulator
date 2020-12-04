using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine;

public class LightController : MonoBehaviour
{

	public Light2D EastLight;
	public Light2D WestLight;

    // Start is called before the first frame update
    void Start()
    {
        
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
            EastLight.intensity = 0.1F;
            WestLight.intensity = 0.1F; 
        }
        else if (percentCompleted < 0.28) {

            // west light
            WestLight.intensity = (float) (0.1 + 0.8*((percentCompleted-0.23)/(0.28-0.23)));

            // east light
            EastLight.intensity = (float) (0.1 + 1.1*((percentCompleted-0.23)/(0.375-0.23)));

        }
        else if (percentCompleted < 0.375) {

            // west light
            WestLight.intensity = 0.8F;

            // east light
            EastLight.intensity = (float) (0.1 + 1.1*((percentCompleted-0.23)/(0.375-0.23)));

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
        else if (percentCompleted < 0.75) {

            // west light
            WestLight.intensity = (float) (1.2 - 1.1*((percentCompleted-0.625)/(0.77-0.625)));

            // east light
            EastLight.intensity = 0.8F;

        }
        else if (percentCompleted < 0.77) {

            // west light
            WestLight.intensity = (float) (1.2 - 1.1*((percentCompleted-0.625)/(0.77-0.625)));

            // east light
            EastLight.intensity = (float) (0.8 - 0.7*((percentCompleted-0.75)/(0.77-0.75)));

        }
        else {
            EastLight.intensity = 0.1F;
            WestLight.intensity = 0.1F; 
        }
    }
}
