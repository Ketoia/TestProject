using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "PossibleCardStats", menuName = "ScriptableObjects/PossibleCardStats", order = 1)]
public class PossibleCardStats : ScriptableObject
{
    public List<string> PossibleNames;
    public List<string> PossibleDescriptions;
    public List<Sprite> PossibleTextures;
    public List<Effects> PossibleEffects;
}

[CustomEditor(typeof(PossibleCardStats))]
[CanEditMultipleObjects]
public class LookAtPointEditor : Editor
{
    SerializedProperty PossibleNames;
    SerializedProperty PossibleDescriptions;
    SerializedProperty PossibleTextures;
    SerializedProperty PossibleEffects;

    void OnEnable()
    {
        PossibleNames = serializedObject.FindProperty("PossibleNames");
        PossibleDescriptions = serializedObject.FindProperty("PossibleDescriptions");
        PossibleTextures = serializedObject.FindProperty("PossibleTextures");
        PossibleEffects = serializedObject.FindProperty("PossibleEffects");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(PossibleNames);
        EditorGUILayout.PropertyField(PossibleDescriptions);
        EditorGUILayout.PropertyField(PossibleTextures);
        EditorGUILayout.PropertyField(PossibleEffects);

        if (PossibleEffects.arraySize > 5) PossibleEffects.arraySize = 5;

        serializedObject.ApplyModifiedProperties();
    }
}