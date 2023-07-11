using System;
using UnityEngine;
using UnityEngine.UI;

public enum SceneName
{
    MainMenu,
    GameScene,
    Settings,
    CutScene
}
public class ChangeSceneTo : MonoBehaviour
{
    [SerializeField]
    private SceneName sceneName;
    private void Awake()
    {
        Button button = GetComponent<Button>();
            
        if (button == null)
            throw new InvalidOperationException("This script should be attached to button");
            
        button.onClick.AddListener(MoveToScene);
    }
    private void MoveToScene()
    {
        CanvasSplit.Instance.ChangeScene(sceneName.ToString());
    }
}
