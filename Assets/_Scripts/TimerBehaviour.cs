using TMPro;
using UnityEngine;

public class TimerBehaviour : MonoBehaviour
{
    private float timer;
    private float timeOfStart;
    private TextMeshProUGUI textfield;

    void Start()
    {
        timeOfStart = Time.time;

        textfield = GetComponent<TextMeshProUGUI>();

        if (textfield == null) 
        {
            Debug.Log("No text component found!");
        }
    }

    void Update()
    {
        timer = Time.time - timeOfStart;

        if (textfield != null)
        {
            int minutes = Mathf.FloorToInt(timer/ 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            string timeLabel = string.Format("<color=black>Time: <color=#0080ff>{0:00}<color=black>:<color=#0080ff>{1:00}", minutes, seconds);
            textfield.text = timeLabel;
        }
        
        //Debug.Log(timer);
    }
}
