using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainManager : MonoBehaviour
{
    #region Properties
    private static Dictionary<string, UnityEvent> eventDictionary = new Dictionary<string, UnityEvent>();

    public PossibleCardStats possibleCardStats;
    #endregion

    #region Main Unity Functions

    private void Start()
    {
        StartListining("Generate", Generate);
        StartListining("Save", Save);
        StartListining("Execute", Execute);
    }

    #endregion

    #region EventManager
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
    #endregion

    #region Manage Buttons

    private void Generate()
    {
        Debug.Log("Generate");
    }

    public void Save()
    {
        Debug.Log("Save");
    }

    public void Execute()
    {
        Debug.Log("Execute");
    }
    #endregion

    #region Show Buttons

    private void OnGUI()
    {
        if (GUILayout.Button("Generuj")) TriggerEvent("Generate");
        if (GUILayout.Button("Zapisz")) TriggerEvent("Save");
        if (GUILayout.Button("Wykonaj")) TriggerEvent("Execute");

    }


    #endregion
}
