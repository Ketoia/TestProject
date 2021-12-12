using UnityEngine;

public class Player : MonoBehaviour
{
    public float Hp, Mana, Speed;

    Rect windowRect = new Rect(80, 25, 150, 100);

    void OnGUI()
    {
        windowRect = GUI.Window(0, windowRect, DoMyWindow, "Player");
    }

    void DoMyWindow(int windowID)
    {
        GUILayout.Label($"HP: {Hp}");
        GUILayout.Label($"Mana: {Mana}");
        GUILayout.Label($"Speed: {Speed}");
    }
}
