using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class GameOverPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject panelObject;
    [SerializeField]
    private Image panelImage;
    [SerializeField]
    private GameObject gameOverText;
    [SerializeField]
    private float duration = 1.0f;
    [SerializeField]
    private EmojiFadeInOut loveEmoji;
    [SerializeField]
    private EmojiFadeInOut deathEmoji;
    [SerializeField]
    private RectTransform deathEmojiRect;
    [SerializeField]
    private MoveAndScaleUi moveAndScaleUi;
    [SerializeField]
    private GameObject highScore;
    [SerializeField]
    private GameObject playerScore;
    private bool _called;
    private Vector2 _deathEmojiInitialPos;
    [SerializeField]
    private Vector2 targetPos =  new Vector2(-825, -385);

    private void Awake()
    {
        _deathEmojiInitialPos = deathEmojiRect.anchoredPosition;
        panelObject.SetActive(false);
        panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, 0);
        gameOverText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        PlayerController.OnGameOver += ShowGameOverPanel;
    }

    private void OnDisable()
    {
        PlayerController.OnGameOver -= ShowGameOverPanel;
    }

 /*   private void ShowGameOverPanel()
    {
        if (!_called)
        {
            _called = true;
            panelObject.SetActive(true);
        loveEmoji.FadeInOutOpacity(0, duration);
        panelImage.DOFade(1, duration).SetUpdate(true).OnComplete(() =>
        {
            gameOverText.gameObject.SetActive(true);
            deathEmoji.FadeInOutOpacity(1, duration,() =>
            {
                {
                    moveAndScaleUi.MoveAndScale(deathEmojiRect, new Vector2(-825, -385), duration, duration,
                        2.5f,0,Ease.InQuad, () =>
                        {
                            moveAndScaleUi.MoveAndScale(deathEmojiRect, _deathEmojiInitialPos, duration, duration,
                                1,0.5f,Ease.OutQuad, () =>
                                {
                                    highScore.gameObject.SetActive(true);
                                    playerScore.gameObject.SetActive(true);
                                });
                        });
                }
            });
        });
        }
    }*/
 
 private void ShowGameOverPanel()
 {
     if (_called)
     {
         return;
     }

        _called = true;
         panelObject.SetActive(true);
        
         loveEmoji.FadeInOutOpacity(0, duration);

         Sequence sequence = DOTween.Sequence();
        
         sequence.Append(panelImage.DOFade(1, duration).SetUpdate(true));
         sequence.AppendCallback(() =>
         {
             gameOverText.gameObject.SetActive(true);
             deathEmoji.FadeInOutOpacity(1, duration);
         });
         sequence.Append(moveAndScaleUi.MoveAndScale(deathEmojiRect, targetPos, duration, duration, 2.5f, Ease.InQuad));
         sequence.AppendInterval(0.5f); 
         sequence.Append(moveAndScaleUi.MoveAndScale(deathEmojiRect, _deathEmojiInitialPos, duration, duration, 1, Ease.OutQuad));
         sequence.AppendCallback(() =>
         {
             highScore.gameObject.SetActive(true);
             playerScore.gameObject.SetActive(true);
         });
     
 }

}