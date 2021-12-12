using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "ScriptableObjects/Effect", order = 1)]
public class Effects : ScriptableObject
{
    [Serializable]
    public struct Calcs
    {
        public CalculationTypes CalculationType;
        public PlayerPropertyTypes PlayerPropertyType;
        public float CalculationValue;
    }

    public List<Calcs> Calculations;

    public enum CalculationTypes
    {
        Multiply,
        Substract,
        Add
    }

    public enum PlayerPropertyTypes
    {
        Hp,
        Mana,
        Speed
    }
}