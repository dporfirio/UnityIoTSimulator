using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Experimental.Rendering.Universal;

public class TimeUpdater : MonoBehaviour
{

	public Text day;
	public Text time;
	public Toggle pause;
	public Toggle play;
	public Toggle ff;
	public Toggle fff;

	// dependencies
	// the time of day affects the outside lighting
	private LightController lc;

	// the time of day can affect when certain events occur
	private EventHub es;

	private List<string> iotHistory;
	private List<IoTDevice> devices;
	private Player player;

	private int inc = 0;

	void Awake ()
    {
    	// find the player
    	GameObject gp = GameObject.Find("PlayerCanvas");
    	player = gp.GetComponent<Player>();

    	// find the day/night cycle controller
    	GameObject g = GameObject.Find("LightController");
        lc = g.GetComponent<LightController>();

        // find the event system
        GameObject ges = GameObject.Find("EventHub");
        es = ges.GetComponent<EventHub>();

        // get all of the IoT devices
        devices = new List<IoTDevice>();
        GameObject iotG = GameObject.Find("IoT");
        Light2D[] iotLights = iotG.GetComponentsInChildren<Light2D>();

        foreach (Light2D light in iotLights) {
        	devices.Add(light.GetComponent<LightDevice>());
        }
    }

	void Start() {

		pause.onValueChanged.AddListener(delegate {
			toggleValueChanged(pause);
		});
		play.onValueChanged.AddListener(delegate {
			toggleValueChanged(play);
		});
		ff.onValueChanged.AddListener(delegate {
			toggleValueChanged(ff);
		});
		fff.onValueChanged.AddListener(delegate {
			toggleValueChanged(fff);
		});

		pause.isOn = true;
		day.text = "Sunday";
		time.text = "12:00:00 AM";
		StartCoroutine(timer());

		iotHistory = new List<string>();
	}

	void toggleValueChanged(Toggle tog) {
		ColorBlock cb = tog.colors;
		if (tog.isOn)
		{
			cb.normalColor = Color.gray;
			cb.highlightedColor = Color.gray;
			cb.pressedColor = Color.gray;
			cb.selectedColor = Color.gray;
			cb.disabledColor = Color.gray;
		}
		else
		{
			cb.normalColor = Color.white;
			cb.highlightedColor = Color.white;
			cb.pressedColor = Color.white;
			cb.selectedColor = Color.white;
			cb.disabledColor = Color.white;
		}
		tog.colors = cb;

		// set the inc
		if (play.isOn) {
			inc = 1;
		}
		else if (ff.isOn) {
			inc = 20;
		}
		else if (fff.isOn) {
			inc = 40;
		}
		else if (pause.isOn) {
			inc = 0;
		}
	}

	IEnumerator timer()
	{	
		// begin a timer on loop
		// start on midnight on Sunday
		int daysPassed = 0;
		int day_int = 0;
		int second = 0;
		int minute = 0;
		int hour = 0;
		while (true) {
			//Wait for 4 seconds
			yield return new WaitForSeconds(0.01F);

			second += inc;
			if (second >= 60) {
				int diff = second-60;
				second = diff;
				minute += 1;
				if (minute == 60) {
					minute = 0;
					hour += 1;
					if (hour == 24) {
						hour = 0;
						day_int += 1;
						daysPassed += 1;
						System.IO.File.WriteAllLines(@"day" + daysPassed + ".txt", iotHistory);
						iotHistory.Clear();
						if (day_int == 7) {
							day_int = 0;
						}
						// get the day
						switch (day_int)
						{
							case 0:
								day.text = "Sunday";
								break;
							case 1:
								day.text = "Monday";
								break;
							case 2:
								day.text = "Tuesday";
								break;
							case 3:
								day.text = "Wednesday";
								break;
							case 4:
								day.text = "Thursday";
								break;
							case 5:
								day.text = "Friday";
								break;
							case 6:
								day.text = "Saturday";
								break;

						}
					}
				}
			}

			// update everything necessary
			lc.UpdateLight(hour, minute, second);
			es.UpdateExternalEvents(day_int, hour, minute, second);

			int hour_int = 0;
			string hour_str = "00";
			string minute_str = "00";
			string am_pm = "AM";

			// set hour
			if (hour == 0) {
				hour_int = 12;
			}
			else if (hour >= 13) {
				hour_int = hour - 12;
			}
			else {
				hour_int = hour;
			}

			hour_str = "" + hour_int;
			if (hour_int < 10) {
				hour_str = "0" + hour_int;
			}

			// set minute
			minute_str = "" + minute;
			if (minute < 10) {
				minute_str = "0" + minute;
			}

			// set am_pm
			if (hour < 12) {
				am_pm = "AM";
			}
			else {
				am_pm = "PM";
			}

			time.text = hour_str + ":" + minute_str + " " + am_pm;

			// update the iotHistory
			string newSecond = "";
			foreach (IoTDevice dev in devices) {
				newSecond = newSecond + " " + dev.state;
			}
			newSecond = newSecond + " " + player.currActivity.description;
			iotHistory.Add(newSecond);
		}
	}

}
