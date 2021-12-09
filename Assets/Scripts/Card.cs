using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    private string _Name;
    public string Name
    {
        get => _Name;
        set
        {
            _Name = value;
            TextName.text = value;
        }
    }

    private string _Description;
    public string Description
    {
        get => _Description;
        set
        {
            _Description = value;
            TextDesc.text = value;
        }
    }

    private Sprite _sprite;
    public Sprite sprite
    {
        get => _sprite;
        set
        {
            _sprite = value;
            CardImage.sprite = value;
        }
    }

    private Effects _Effect;
    public Effects Effect
    {
        get => _Effect;
        set
        {
            _Effect = value;
            TextEffect.text = "";

            Effects.Calculations[] calc = value.calculations;
            for (int i = 0; i < calc.Length; i++)
            {
                TextEffect.text += calc[i].calculationType.value + " " + calc[i].Value;

                if (calc[i].calculationType.value == "substract") TextEffect.text += " from ";
                else TextEffect.text += " to ";

                TextEffect.text += calc[i].propertyType.value;
                if (i != calc.Length - 1) TextEffect.text += "\n";
            }
        }
    }

    public Text TextName;
    public Text TextDesc;
    public Image CardImage;
    public Text TextEffect;
}
