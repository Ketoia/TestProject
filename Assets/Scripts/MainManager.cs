using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainManager : MonoBehaviour
{
    #region Properties
    private static Dictionary<string, UnityEvent> eventDictionary = new Dictionary<string, UnityEvent>();

    public PossibleCardStats possibleCardStats;
    public Card card;

    Player player;
    #endregion

    #region Main Unity Functions

    private void Start()
    {
        StartListining("Generate", Generate);
        StartListining("Save", Save);
        StartListining("Execute", Execute);

        player = FindObjectOfType(typeof(Player)) as Player;
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
        card.Name = possibleCardStats.PossibleNames[Random.Range(0, possibleCardStats.PossibleNames.Count)];
        card.Description = possibleCardStats.PossibleDescriptions[Random.Range(0, possibleCardStats.PossibleDescriptions.Count)];
        card.sprite = possibleCardStats.PossibleTextures[Random.Range(0, possibleCardStats.PossibleTextures.Count)];
        card.Effect = possibleCardStats.PossibleEffects[Random.Range(0, possibleCardStats.PossibleEffects.Count)];
    }

    public void Save()
    {
        Debug.Log("Save");
    }

    public void Execute()
    {
        foreach(Effects.Calculations effect in card.Effect.calculations)
        {
            string type = effect.calculationType.value;
            if(type == "Multiply")
            {
                if(effect.propertyType.value == "HP")
                {
                    player.Hp *= effect.Value;
                }
                else if (effect.propertyType.value == "Mana")
                {
                    player.Mana *= effect.Value;
                }
                else if (effect.propertyType.value == "Speed")
                {
                    player.Speed *= effect.Value;
                }
            }
            else if(type == "Add")
            {
                if (effect.propertyType.value == "HP")
                {
                    player.Hp += effect.Value;
                }
                else if (effect.propertyType.value == "Mana")
                {
                    player.Mana += effect.Value;
                }
                else if (effect.propertyType.value == "Speed")
                {
                    player.Speed += effect.Value;
                }
            }
            else if(type == "substract")
            {
                if (effect.propertyType.value == "HP")
                {
                    player.Hp -= effect.Value;
                }
                else if (effect.propertyType.value == "Mana")
                {
                    player.Mana -= effect.Value;
                }
                else if (effect.propertyType.value == "Speed")
                {
                    player.Speed -= effect.Value;
                }
            }
        }
        
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
