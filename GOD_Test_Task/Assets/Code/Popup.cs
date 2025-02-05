using System;
using DG.Tweening;
using UnityEngine;

namespace Code
{
    public class Popup : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _transform;
        
        private Vector2 targetPosition;
        private Vector2 startPosition;

        private void Awake()
        {
            targetPosition = _transform.anchoredPosition;
            startPosition = new Vector2(targetPosition.x, -Screen.height / 2);
        }

        public void Show()
        {
            Sequence sequence = DOTween.Sequence();
            
            sequence.Append(_canvasGroup.DOFade(1, 0.5f).From(0))
                .Join(_transform.DOAnchorPos(targetPosition, 0.5f).From(startPosition))
                .SetEase(Ease.OutBounce);
        }

        public void Hide()
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(_canvasGroup.DOFade(0, 0.5f).From(0))
                .Join(_transform.DOAnchorPos(startPosition, 0.5f).From(targetPosition));
        }
    }
}