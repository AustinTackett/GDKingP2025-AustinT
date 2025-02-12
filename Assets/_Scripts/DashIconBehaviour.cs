using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DashIconBehaviour : MonoBehaviour
{
    TextMeshProUGUI label;
    public float cooldown;
    public float cooldownRate;
    public float fill;
    Image overlay;

    void Start()
    {
        label = GetComponentInChildren<TextMeshProUGUI>();
        Image[] images = GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++) 
        {
            if (images[i].tag == "Overlay" ) 
            {
                overlay = images[i];
            }
        }
        overlay.fillAmount = 0.0f;

        cooldownRate = PinBehaviour.cooldownRate;
    }

    void Update()
    {
        cooldown = PinBehaviour.cooldown;

        if (cooldown > 0.0)
        {
            string message = string.Format("{0:0.0}", cooldown);
            fill = cooldown / cooldownRate;
            overlay.fillAmount = fill;
            label.text = message;
        }
        else if (cooldown == 0)
        {
            label.text = "";
        }
    
    }
}
