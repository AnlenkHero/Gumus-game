using System;
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI highScore;
    [SerializeField]
    private Timer timer;

    private void Awake()
    {
        highScore.text = PlayerPrefs.GetString("HighScore", "00:00");
    }

    private void OnEnable()
    {
        PlayerController.OnGameOver += SetHighScore;
    }

    private void OnDisable()
    {
        PlayerController.OnGameOver -= SetHighScore;
    }

    private void SetHighScore()
    {
        if(String.Compare(timer.TimerValue.text, highScore.text, StringComparison.Ordinal) > 0)
        {
            highScore.text = timer.TimerValue.text;

            PlayerPrefs.SetString("HighScore", highScore.text);
            PlayerPrefs.Save();
        }
    }
}