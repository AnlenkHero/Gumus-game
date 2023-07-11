using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using JetBrains.Annotations;
using TMPro;
using UnityEngine.SceneManagement;

public class CanvasSplit : MonoBehaviour
{
    [SerializeField]
    private RectTransform leftSide;  
    [SerializeField] 
    private RectTransform rightSide;
    [SerializeField]
    private GameObject emptyImage;
    [SerializeField]
    private TextMeshProUGUI loadingText;
    public float duration = 1.0f;   
    private float _initialPositionLeft;
    private float _initialPositionRight;
    public static CanvasSplit Instance;
    


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        _initialPositionLeft = leftSide.anchoredPosition.x;
        _initialPositionRight = rightSide.anchoredPosition.x;
        DOTween.Init();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void ChangeScene(string sceneName)
    {
        MoveToCenter( () => StartCoroutine(LoadAsync(sceneName)));
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        MoveToSides();
        //StartCoroutine(AnimateCanvas());
    }

    private void OnSceneUnloaded(Scene scene)
    {
        MoveToCenter();
    }

    IEnumerator AnimateCanvas()
    {
        MoveToSides();
        yield return new WaitForSeconds(duration);
        MoveToCenter();
    }

    IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        loadingText.gameObject.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingText.text = (progress * 100f).ToString("0") + "%";
            yield return null;
        }
        loadingText.gameObject.SetActive(false);
    }

    public void CanvasQuit()
    {
        MoveToCenter(Application.Quit);
    }

    private void MoveToSides([CanBeNull] Action callback=null)
    {
        if (emptyImage != null)
            emptyImage.SetActive(true);
    
        if (leftSide != null)
            leftSide.DOAnchorPosX(-leftSide.rect.width, duration);
    
        if (rightSide != null)
            rightSide.DOAnchorPosX(rightSide.rect.width, duration).OnComplete(() =>
            {
                if (emptyImage != null)
                    emptyImage.SetActive(false);
                callback?.Invoke();
            });
    }

    private void MoveToCenter([CanBeNull] Action callback=null)
    {
        emptyImage.SetActive(true);
        leftSide.DOAnchorPosX(_initialPositionLeft, duration);
        rightSide.DOAnchorPosX(_initialPositionRight, duration).OnComplete(() =>
        {
            emptyImage.SetActive(false);
            callback?.Invoke();
        });
    }
}