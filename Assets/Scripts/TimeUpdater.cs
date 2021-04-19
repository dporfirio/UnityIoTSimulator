using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Experimental.Rendering.Universal;
using Pathfinding;

public class TimeUpdater : MonoBehaviour
{

	public Text day;
	public Text time;
	public Toggle pause;
	public Toggle play;
	public Toggle ff;
	public Toggle fff;
	//public Text test;

	// dependencies
	// the time of day affects the outside lighting
	private LightController lc;

	// the time of day can affect when certain events occur
	private EventHub es;

    //private List<string> iotHistory;
    private List<IoTDevice> devices;
	private Player player;

	private int inc = 0;
	private int preInc = 0;
	private float timeInc = 1F;

    public int daysPassed = 0;
    public int day_int = 0;
	public int second = 0;
	public int minute = 0;
	public int hour = 0;
	private int secondPassed = 0;
	private int secondForCnt = 0;

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

        //iotHistory = new List<string>();
    }

    void Update()
    {
		timer();
    }

    public void toggleValueChanged(Toggle tog) {
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
			ChangeInc(1);
		}
		else if (ff.isOn) {
			ChangeInc(10);
		}
		else if (fff.isOn) {
			ChangeInc(15);
		}
		else if (pause.isOn) {
			ChangeInc(0);
		}
	}

	public void StartAll()
    {
		GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
		this.ChangeInc(1);
		GameObject.Find("PlayerOptionPane").GetComponent<PlayerOptions>().buttonPrefab.GetComponent<Toggle>().interactable = true;
	}

	public void StopAll()
	{
		GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = false;
		this.ChangeInc(0);
		GameObject.Find("PlayerOptionPane").GetComponent<PlayerOptions>().buttonPrefab.GetComponent<Toggle>().interactable = false;
	}

	public void TimeFly(int duration, Activity act)
    {
		this.act = act;
		GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = false;
		StartCoroutine(timeCount(duration));
	}
	
	public Coroutine co;
	public Activity act;

	public void TimeFlyAndStop(int duration, Activity act)
	{
		this.act = act;
		//GameObject.Find("StartInput").GetComponent<StartInput>().AddKey(KeyCode.P, StopTimeFly);
		this.co = StartCoroutine(timeCount(duration));
	}


	public void StartTimeFly(Activity act, int inc)
	{
		this.act = act;
		//GameObject.Find("StartInput").GetComponent<StartInput>().AddKey(KeyCode.P, StopTimeFly);
		this.co = StartCoroutine(timeInfi(inc));
	}

	// Stop timefly by hit S
	//public void StopTimeFly()
 //   {
	//	StopCo();
	//	//GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
		
	//	Debug.Log("end " + GameObject.Find("Player").GetComponent<PlayerMovement>().canMove);
	//	this.act.EndAct();
	//	this.act = null;
	//}

	IEnumerator timeInfi(int inc)
	{
		ChangeInc(inc);
		Debug.Log("start");

		while (true)
		{
			yield return new WaitForSeconds(0.1F);
			//GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = false;

		}
	}

	public void StopCo()
    {
		StopCoroutine(this.co);
		ChangeInc(1);
		this.co = null;
		Debug.Log("stop!!");
	}


	IEnumerator timeCount(int duration)
    {
        int cnt = 0;

		ChangeInc(15);
		Debug.Log("start");

		while (cnt <= duration)
        {
			yield return new WaitForSeconds(0.1F);
			cnt += inc;
            //GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = false;

        }
        Debug.Log("end" + this.act);
		this.act.EndAct();
		GameObject.Find("PlayerCanvas").GetComponent<Player>().currActivity = new Other();
		GameObject.Find("PlayerCanvas").GetComponent<Player>().currActivityText.text = "other";

		this.act = null;

		ChangeInc(1);

		//GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;

    }

	public void ChangeInc(int newInc)
    {
		if (newInc == 0)
        {
			inc = 0;
			preInc = 0;
			GameObject.Find("Robot").gameObject.GetComponent<AIPath>().maxSpeed = 0F;
			GameObject.Find("Robot").gameObject.GetComponent<AIPath>().maxAcceleration = 12F;
			GameObject.Find("Robot").gameObject.GetComponent<AIPath>().slowdownDistance = 1F;

			return;
		}

		preInc = inc;

		inc = newInc;

		if (preInc == 0)
		{
			preInc = 1;
			GameObject.Find("Robot").gameObject.GetComponent<AIPath>().maxSpeed = 2.5F;
		}

		timeInc = (float)inc / (float)preInc;

		GameObject.Find("Robot").gameObject.GetComponent<AIPath>().maxSpeed *= timeInc;
		GameObject.Find("Robot").gameObject.GetComponent<AIPath>().maxAcceleration *= timeInc;
		GameObject.Find("Robot").gameObject.GetComponent<AIPath>().slowdownDistance *= timeInc;
	}

	void timer()
	{
		// begin a timer on loop
		// start on morning on Sunday
		lc.UpdateLight(hour, minute, second);
		second += inc;
		secondPassed += inc;
		secondForCnt += inc;
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

                    secondPassed = 0; // counted every day

                    //System.IO.File.WriteAllLines(@"day" + daysPassed + ".txt", iotHistory);
                    //iotHistory.Clear();
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

		//GameObject.Find("Canvas").GetComponent<StartGame>().SendTimeUpdate(day.text, time.text);


		// update the iotHistory
		//string newSecond = "";
  //      foreach (IoTDevice dev in devices)
  //      {
		//	if (dev.name != "Sun")
		//		newSecond = newSecond + " " + dev.state;
  //      }
  //      newSecond = newSecond + " " + player.currActivity.description + " " + GameObject.Find("Robot").GetComponent<RobotController>().state;

		//iotHistory.Add(newSecond);

        if (secondForCnt > 150 ) { // send update every 150s
			string human = GameObject.Find("PlayerCanvas").GetComponent<Player>().currActivityText.text;
			string robot = GameObject.Find("Robot").GetComponent<RobotController>().state;

			secondForCnt -= 150;
			Debug.Log("day: " + day_int);
			GameObject.Find("Canvas").GetComponent<StartGame>().SendDeviceUpdate(day_int, secondPassed, human, robot);
		}

	}

}
