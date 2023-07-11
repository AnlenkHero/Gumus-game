using TMPro;
using UnityEngine;

namespace Helpers
{
    public class TextScrolling : MonoBehaviour
    {
        public float scrollSpeed = 30.0f;
        private TextMeshProUGUI _textMeshProUGUI;
        private RectTransform _textRectTransform;
        private float _textWidth;
        private float canvasHeight;
        private Vector2 _initialPos;

        void Start()
        {
            _textRectTransform = GetComponent<RectTransform>();
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            _textWidth = _textMeshProUGUI.preferredWidth;//_textRectTransform.rect.width;
            canvasHeight = GetComponentInParent<Canvas>().pixelRect.width;
            _initialPos = _textRectTransform.anchoredPosition;
        }

        void Update()
        {
            _textRectTransform.anchoredPosition -= new Vector2(scrollSpeed *  Time.deltaTime, 0.0f);

            if (_textRectTransform.anchoredPosition.x <= - _textWidth - canvasHeight)
            {
                _textRectTransform.anchoredPosition = _initialPos;
            }
        }
    }
}
