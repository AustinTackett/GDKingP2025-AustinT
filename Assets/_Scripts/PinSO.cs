using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "PinSO", menuName = "Scriptable Objects/PinSO")]
public class PinSO : ScriptableObject
{
    public Pin[] pins;

    public int count() {
        return pins.Length;
    }

    public Pin getPin(int index)
    {
        return pins[index];
    }
}
