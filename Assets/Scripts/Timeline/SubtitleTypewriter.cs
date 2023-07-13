using System.Collections;
using TMPro;
using UnityEngine;

public class SubtitleTypewriter : MonoBehaviour
{
    private TextMeshProUGUI text;
    private string fullText;
    private float typeSpeed;
    private Coroutine coroutine;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void StartTypewriter(string newText, float speed)
    {
        fullText = newText;
        typeSpeed = speed;
        text.text = "";

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        foreach (char c in fullText)
        {
            text.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
}