using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Effect", menuName = "ScriptableObjects/SpawnManagerCustomEffect", order = 1)]
[Serializable]
public class Effects : ScriptableObject
{
    [Serializable]
    public struct Calculation
    {
        public string value;
    }

    [Serializable]
    public struct PlayerPropertyType
    {        
        public string value;
    }

    [Serializable]
    public struct Calculations
    {
        public Calculation calculationType;
        public PlayerPropertyType propertyType;
        public int Value;
    }

    public Calculations[] calculations;
}

[CustomPropertyDrawer(typeof(Effects.Calculation))]
public class EffectsDrawerCalculations : PropertyDrawer
{
    static readonly string[] comboItemDatabase = { "Multiply", "Add", "substract" };
    static readonly GUIContent[] comboItemDatabaseGUIContents = Array.ConvertAll(comboItemDatabase, i => new GUIContent(i));

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        property = property.FindPropertyRelative("value");
        EditorGUI.BeginChangeCheck();
        int selectedIndex = Array.IndexOf(comboItemDatabase, property.stringValue);
        selectedIndex = EditorGUI.Popup(position, label, selectedIndex, comboItemDatabaseGUIContents);
        if (EditorGUI.EndChangeCheck())
        {
            property.stringValue = comboItemDatabase[selectedIndex];
        }
    }
}

[CustomPropertyDrawer(typeof(Effects.PlayerPropertyType))]
public class EffectsDrawerPropertyType : PropertyDrawer
{
    static readonly string[] comboItemDatabase = { "HP", "Mana", "Speed" };
    static readonly GUIContent[] comboItemDatabaseGUIContents = Array.ConvertAll(comboItemDatabase, i => new GUIContent(i));

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        property = property.FindPropertyRelative("value");
        EditorGUI.BeginChangeCheck();
        int selectedIndex = Array.IndexOf(comboItemDatabase, property.stringValue);
        selectedIndex = EditorGUI.Popup(position, label, selectedIndex, comboItemDatabaseGUIContents);
        if (EditorGUI.EndChangeCheck())
        {
            property.stringValue = comboItemDatabase[selectedIndex];
        }
    }
}