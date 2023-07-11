using TMPro;
using UnityEngine;

public class TextForGameLosePanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI targetText;
    [SerializeField]
    private TextMeshProUGUI textToChange;
    private void Awake()
    {
        textToChange.text += targetText.text;
    }
}
