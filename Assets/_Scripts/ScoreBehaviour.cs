using UnityEngine;
using TMPro;
using System;

public class ScoreBehaviour : MonoBehaviour
{
    private int score;
    private TextMeshProUGUI textfield;

    void Start()
    {
        SpawningBehaviour spawner = GameObject.Find("BallSpawner").GetComponent<SpawningBehaviour>();
        spawner.increaseScore += updateScore;

        textfield = GetComponent<TextMeshProUGUI>();

        // The first ball spawned is in the start method and does not fire the event so start score is 1
        score = 1;
        setScoreTextField(score);
    }

    private void updateScore(object sender, EventArgs e)
    {
        if (textfield != null)
        {
            score++;
            setScoreTextField(score);
        }
        
    }

    private void setScoreTextField(int score)
    {
        string timeLabel = string.Format("<color=black>Score: <color=black>{0}", score);
        textfield.text = timeLabel;
    }
}
