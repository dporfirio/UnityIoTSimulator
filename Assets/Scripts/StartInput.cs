using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartInput : MonoBehaviour
{
    Dictionary<KeyCode, System.Action> buttonKeys;

    // Start is called before the first frame update
    void Awake()
    {
        buttonKeys = new Dictionary<KeyCode, System.Action>();

    }

    // Update is called once per frame
    void OnGUI()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        Event e = Event.current;
        if (e != null && e.type == EventType.KeyDown)
        {
            if (buttonKeys.ContainsKey(e.keyCode))
            {
                buttonKeys[e.keyCode]();
            }
        }
    }

    public void AddKey(KeyCode k, System.Action f)
    {
        buttonKeys[k] = f;
    }

    public void RemoveKey(KeyCode k)
    {
        buttonKeys.Remove(k);
    }
}
