using System;
using UnityEngine;
using UnityEngine.UI;

public class QuitWithAnimation : MonoBehaviour
{
    private void Awake()
    {
        Button button = GetComponent<Button>();
            
        if (button == null)
            throw new InvalidOperationException("This script should be attached to button");
            
        button.onClick.AddListener(QuitGame);
    }
    private void QuitGame()
    {
        CanvasSplit.Instance.CanvasQuit();
    }
}
