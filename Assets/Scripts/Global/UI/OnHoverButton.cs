using Helpers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;

public class OnHoverButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    private GameObject particlePrefab;
    [SerializeField]
    private WordWobble wordWobble;
    [SerializeField]
    private Gradient initialGradient;
    [SerializeField]
    private Gradient targetGradient;

    public void OnPointerEnter(PointerEventData eventData)
    {
        wordWobble.rainbow=targetGradient;
        particlePrefab.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        wordWobble.rainbow = initialGradient;
        particlePrefab.SetActive(false);
    }
}
