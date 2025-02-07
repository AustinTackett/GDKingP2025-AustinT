using UnityEngine;
using TMPro;

public class DashIconBehaviour : MonoBehaviour
{
    TextMeshProUGUI label;
    float cooldown;
    float cooldownRate;

    void Start()
    {
        label = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        string message = string.Format("{0:0.0}", PinBehaviour.cooldown);
        label.text = message;
    }
}
