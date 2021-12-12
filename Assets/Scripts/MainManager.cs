using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

public class MainManager : MonoBehaviour
{
    #region Properties
    private static Dictionary<string, UnityEvent> eventDictionary = new Dictionary<string, UnityEvent>();

    public PossibleCardStats possibleCardStats;
    public GameObject CardGameObject;
    Card card;

    Player player;
    #endregion

    #region Main Unity Functions

    private void Start()
    {
        StartListining("Generate", Generate);
        StartListining("Save", Save);
        StartListining("Execute", Execute);
        StartListining("Load", Load);

        player = FindObjectOfType(typeof(Player)) as Player;
        card = CardGameObject.GetComponent<Card>();

        TriggerEvent("Generate");
    }

    #endregion

    #region EventManager
    public static void StartListining(string Key, UnityAction listener)
    {
        if (eventDictionary.TryGetValue(Key, out UnityEvent thisEvent))
        {
            thisEvent.AddListener(listener);
            //Debug.Log("Added listener to Key: " + Key );
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            eventDictionary.Add(Key, thisEvent);
            //Debug.Log("Created Key: " + Key + " and added listener to it");
        }
    }

    public static void StopListining(string Key, UnityAction listener)
    {
        if (eventDictionary.TryGetValue(Key, out UnityEvent thisEvent))
        {
            thisEvent.RemoveListener(listener);
            //Debug.Log("Removed listener from Key: " + Key);
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

        Debug.Log("Generated Card");
    }

    private void Execute()
    {
        foreach (Effects.Calcs effect in card.Effect.Calculations)
        {
            string type = effect.CalculationType.ToString();
            string playertype = effect.PlayerPropertyType.ToString();
            if (type == "Multiply")
            {
                if (playertype == "Hp")
                {
                    player.Hp *= effect.CalculationValue;
                }
                else if (playertype == "Mana")
                {
                    player.Mana *= effect.CalculationValue;
                }
                else if (playertype == "Speed")
                {
                    player.Speed *= effect.CalculationValue;
                }
            }
            else if (type == "Add")
            {
                if (playertype == "Hp")
                {
                    player.Hp += effect.CalculationValue;
                }
                else if (playertype == "Mana")
                {
                    player.Mana += effect.CalculationValue;
                }
                else if (playertype == "Speed")
                {
                    player.Speed += effect.CalculationValue;
                }
            }
            else if (type == "Substract")
            {
                if (playertype == "Hp")
                {
                    player.Hp -= effect.CalculationValue;
                }
                else if (playertype == "Mana")
                {
                    player.Mana -= effect.CalculationValue;
                }
                else if (playertype == "Speed")
                {
                    player.Speed -= effect.CalculationValue;
                }
            }
        }
        Debug.Log("Executed effect on player");

        TriggerEvent("Generate");
    }

    private void Save()
    {
        string path = EditorUtility.SaveFilePanel("Save card", Application.dataPath + "/Prefabs", "Card", "prefab");
        if (path == "") return;

        PrefabUtility.SaveAsPrefabAsset(CardGameObject, path);
        Debug.Log("Saved Card");
    }

    private void Load()
    {
        //check good path
        string path = EditorUtility.OpenFilePanel("Open card", Application.dataPath + "/Prefabs", "prefab");
        if (path == "") return;

        //check is it card
        GameObject cardoGameobjct = PrefabUtility.LoadPrefabContents(path);
        if (cardoGameobjct.GetComponent<Card>() == null)
        {
            Debug.LogWarning("Wrong Game Object");
            return;
        }

        //instantiate card
        Transform parent = CardGameObject.transform.parent;
        Destroy(CardGameObject);
        CardGameObject = Instantiate(cardoGameobjct, parent);
        card = CardGameObject.GetComponent<Card>();
        Debug.Log("Loaded Card");
    }
    #endregion

    #region Show Buttons

    private void OnGUI()
    {
        GUILayout.Space(25);
        if (GUILayout.Button("Generuj")) TriggerEvent("Generate");
        if (GUILayout.Button("Wykonaj")) TriggerEvent("Execute");
        if (GUILayout.Button("Zapisz")) TriggerEvent("Save");
        if (GUILayout.Button("Wczytaj")) TriggerEvent("Load");
    }
    #endregion
}
