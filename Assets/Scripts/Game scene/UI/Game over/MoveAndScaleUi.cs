using DG.Tweening;
using UnityEngine;

public class MoveAndScaleUi : MonoBehaviour
{
    public Tween MoveAndScale(RectTransform myObject, Vector2 targetPos, float moveDuration, float scaleDuration, float scaleFactor, Ease easeType)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(myObject.DOAnchorPos(targetPos, moveDuration).SetEase(easeType));
        sequence.Join(myObject.DOScale(scaleFactor, scaleDuration).SetEase(easeType));
        return sequence;
    }
}