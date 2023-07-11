using TMPro;
using UnityEngine;

public class SetPowerUpText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI originalText;

    public void SetText(KeyCode keyCode, string powerUpName)
    {
        originalText.text = "";
        originalText.text = $"Press {keyCode.ToString()} to use {powerUpName}";
    }
}
