using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainManager
{
    private static Dictionary<string, UnityEvent> eventDictionary = new Dictionary<string, UnityEvent>();

    public static void StartListining(string Key, UnityAction listener)
    {
        if (eventDictionary.TryGetValue(Key, out UnityEvent thisEvent))
        {
            thisEvent.AddListener(listener);
            Debug.Log("Added listener to Key: " + Key );
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            eventDictionary.Add(Key, thisEvent);
            Debug.Log("Created Key: " + Key + " and added listener to it");
        }
    }

    public static void StopListining(string Key, UnityAction listener)
    {
        if (eventDictionary.TryGetValue(Key, out UnityEvent thisEvent))
        {
            thisEvent.RemoveListener(listener);
            Debug.Log("Removed listener from Key: " + Key);
        }
    }

    public static void TriggerEvent(string Key)
    {
        if (eventDictionary.TryGetValue(Key, out UnityEvent thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}
