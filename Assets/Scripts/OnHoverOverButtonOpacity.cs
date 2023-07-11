using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnHoverOverButtonOpacity : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    private Image image;
    private float _opacity;

    private void Awake()
    {
        _opacity = image.color.a;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 255);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, _opacity);
    }
}
