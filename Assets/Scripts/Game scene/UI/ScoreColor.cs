using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


public class ScoreColor : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI loseText;
    [SerializeField]
    private TextMeshProUGUI inGameHighScore;
    [SerializeField]
    private TextMeshProUGUI inGamePlayerScore;
    [SerializeField]
    private TextMeshProUGUI postGameHighScore;
    [SerializeField]
    private TextMeshProUGUI postGamePlayerScore;
    [SerializeField]
    private string[] onBeatText;
    [SerializeField]
    private string[] onNotBeatText;
    private bool _beat;

    private void OnEnable()
    {
        PlayerController.OnGameOver += SetLostText;
    }

    private void OnDisable()
    {
        PlayerController.OnGameOver -= SetLostText;
    }

    private void Update()
    {
        if (string.Compare(inGamePlayerScore.text, inGameHighScore.text, StringComparison.Ordinal) > 0)
        {
            postGamePlayerScore.color = Color.green;
            inGamePlayerScore.color = Color.green;
            _beat = true;
        }
        else if(string.Compare(inGamePlayerScore.text, inGameHighScore.text, StringComparison.Ordinal) < 0)
        {
            postGamePlayerScore.color = Color.red;
            inGamePlayerScore.color = Color.red;
            _beat = false;
        }
    }

    private void SetLostText()
    {
        if (_beat)
        {
            loseText.text = onBeatText[Random.Range(0, onBeatText.Length)];
        }
        else
        {
            loseText.text = onNotBeatText[Random.Range(0, onNotBeatText.Length)];
        }
        
    }
}
