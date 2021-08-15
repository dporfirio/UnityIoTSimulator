using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is used to bound keys to certain functions in the game, such as "Enter" to open the feedback pop-up window.
public class RegisterKey : MonoBehaviour
{
    Dictionary<KeyCode, System.Action> buttonKeys;

    void Awake()
    {
        buttonKeys = new Dictionary<KeyCode, System.Action>();

    }

    void OnGUI()
    {
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
