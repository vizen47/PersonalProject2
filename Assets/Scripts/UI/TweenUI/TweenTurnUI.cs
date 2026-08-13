using System;
using DG.Tweening;
using Systems.TurnSystem;
using TMPro;
using UnityEngine;

namespace UI.TweenUI
{
    public class TweenTurnUI : MonoBehaviour
    {
        [SerializeField] private RectTransform targetRect;
        [SerializeField] private Vector3 targetScale = Vector3.one;
        [SerializeField] private Vector2 targetPosition;

        private Vector2 startPosition;
        private Vector3 startScale;
        
        private void Awake()
        {
            if (targetRect == null)
                targetRect = GetComponent<RectTransform>();
            
            startPosition = targetRect.anchoredPosition;
            startScale = targetRect.localScale;
        }

        private void OnEnable()
        {
            targetRect.DOKill();

            targetRect.anchoredPosition = startPosition;
            targetRect.localScale = Vector3.zero;
            
            PlayVictory();
        }

        private void OnDisable()
        {
            targetRect.DOKill();
        }
        
        private void PlayVictory()
        {
            Sequence seq = DOTween.Sequence();

            seq.Append(targetRect.DOScale(targetScale, 0.45f)
                .SetEase(Ease.OutBack));

            seq.Join(targetRect.DOAnchorPos(targetPosition, 0.45f)
                .SetEase(Ease.OutCubic));
        }
    }
}