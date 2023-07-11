using System;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class EmojiFadeInOut : MonoBehaviour
{
    [SerializeField]
    private float startOpacity;
    [SerializeField]
    private List<Image> images = new List<Image>();

    void Start()
    {
        FindChildImages(transform);
    }
    void FindChildImages(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Image image = child.GetComponent<Image>();
            if (image != null)
            {
                images.Add(image);
                Color color = image.color;
                color.a = startOpacity;
                image.color = color;
            }
        
            if(child.childCount > 0)
            {
                FindChildImages(child);
            }
        }
    }

    public void FadeInOutOpacity(float opacity,float duration, [CanBeNull] Action callback=null)
    {
        foreach (Image image in images)
        {
            image.DOFade(opacity, duration).SetUpdate(true).OnComplete(() =>
            {
                if (callback != null) callback();
            });
        }
    }
    
}