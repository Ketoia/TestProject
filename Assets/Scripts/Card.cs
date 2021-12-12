using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public string _Name;
    public string Name
    {
        get => _Name;
        set
        {
            _Name = value;
            TextName.text = value;
            EditorApplication.QueuePlayerLoopUpdate();
        }
    }

    public string _Description;
    public string Description
    {
        get => _Description;
        set
        {
            _Description = value;
            TextDesc.text = value;
            EditorApplication.QueuePlayerLoopUpdate();
        }
    }

    public Sprite _sprite;
    public Sprite sprite
    {
        get => _sprite;
        set
        {
            _sprite = value;
            CardImage.sprite = value;
            EditorApplication.QueuePlayerLoopUpdate();
        }
    }

    public Effects _Effect;
    public Effects Effect
    {
        get => _Effect;
        set
        {
            _Effect = value;
            if (value == null) return;
            TextEffect.text = "";

            List<Effects.Calcs> calc = value.Calculations;
            for (int i = 0; i < calc.Count; i++)
            {
                TextEffect.text += calc[i].CalculationType + " " + calc[i].CalculationValue;

                if (calc[i].CalculationType.ToString() == "Substract") TextEffect.text += " from ";
                else if (calc[i].CalculationType.ToString() == "Multiply") TextEffect.text += " by ";
                else TextEffect.text += " to ";

                TextEffect.text += calc[i].PlayerPropertyType;
                if (i != calc.Count - 1) TextEffect.text += "\n";
            }
        }
    }

    public Text TextName;
    public Text TextDesc;
    public Image CardImage;
    public Text TextEffect;
}

[CustomEditor(typeof(Card)), CanEditMultipleObjects]
public class CardPropertyDrawer : Editor
{
    public override void OnInspectorGUI()
    {        
        Card card = target as Card;

        #region Name
        EditorGUI.BeginChangeCheck();
        string Name = EditorGUILayout.TextField("Name", card._Name);
        if (EditorGUI.EndChangeCheck())
        {
            card._Name = Name;
            card.TextName.text = Name;
            EditorApplication.QueuePlayerLoopUpdate();
        }
        #endregion

        #region Description
        EditorGUI.BeginChangeCheck();
        string Description = EditorGUILayout.TextField("Description", card._Description);
        if (EditorGUI.EndChangeCheck())
        {
            card._Description = Description;
            card.TextDesc.text = Description;
            EditorApplication.QueuePlayerLoopUpdate();
        }
        #endregion

        #region Sprite
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Sprite");
        Sprite sprite = (Sprite)EditorGUILayout.ObjectField(card._sprite, typeof(Sprite), allowSceneObjects: true);
        EditorGUILayout.EndHorizontal();
        if (EditorGUI.EndChangeCheck())
        {
            card._sprite = sprite;
            card.CardImage.sprite = sprite;
            EditorApplication.QueuePlayerLoopUpdate();
        }
        #endregion

        #region Effect
        EditorGUI.BeginChangeCheck();
        Effects Effect = (Effects)EditorGUILayout.ObjectField("Effect", card._Effect, typeof(Effects), allowSceneObjects: true);
        if (EditorGUI.EndChangeCheck())
        {
            card._Effect = Effect;
            string text = "";

            List<Effects.Calcs> calc = card.Effect.Calculations;
            for (int i = 0; i < calc.Count; i++)
            {
                text += calc[i].CalculationType + " " + calc[i].CalculationValue;

                if (calc[i].CalculationType.ToString() == "Substract") text += " from ";
                else if (calc[i].CalculationType.ToString() == "Multiply") text += " by ";
                else text += " to ";

                text += calc[i].PlayerPropertyType;
                if (i != calc.Count - 1) text += "\n";
            }
            card.TextEffect.text = text;

            EditorApplication.QueuePlayerLoopUpdate();
        }
        #endregion
    }
}
