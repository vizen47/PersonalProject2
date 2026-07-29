using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class CardListContainerUI : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private int maxAngle = 60;
        [SerializeField] private int maxCard = 12;
        [SerializeField] private float maxRadius = 600f;
        [SerializeField] private Vector3 pivotOffset = new Vector3(0f, -500f, 0f);
        [Header("CardMovement")]
        [SerializeField] private float duration = 0.5f;
        
        private readonly List<RectTransform> cardList = new List<RectTransform>();
        private CardHoverUI cardHoverUI;
        
        private void ArrangeCards()
        {
            cardList.Clear();
            foreach (Transform child in transform)
            {
                if (child.gameObject.activeSelf && child.TryGetComponent<RectTransform>(out var rect))
                {
                    cardList.Add(rect);
                }
            }
            
            int cardCount = cardList.Count;
            if (cardCount == 0) return;

            if (cardCount == 1)
            {
                cardList[0].DOAnchorPos(Vector3.zero + new Vector3(0, -50, 0), duration).SetEase(Ease.OutCubic);
                cardList[0].DOLocalRotate(new Vector3(0, 0, 0), duration).SetEase(Ease.OutCubic);
                
                if (cardList[0].TryGetComponent<CardHoverUI>(out var hoverUI))
                {
                    hoverUI.SetPosition(Vector2.zero);
                }
                
                return;
            }
        
            float totalAngle = Mathf.Min(maxAngle, maxCard * (cardCount - 1));
            float angleStep = totalAngle / (cardCount - 1);
            float startAngle = totalAngle / 2f; // 왼쪽 시작 각도

            for (int i = 0; i < cardCount; i++)
            {
                // 각 카드의 회전각 (왼쪽에서 오른쪽으로 회전)
                float targetAngle = startAngle - (i * angleStep);
            
                // 변환 (유니티는 위쪽(Y축)을 기준으로 정렬하기 위해 +90도 처리)
                float radians = (targetAngle + 90f) * Mathf.Deg2Rad;

                // 삼각함수로 로컬 좌표 계산 후 오프셋 적용
                Vector3 localPos = new Vector3(
                    Mathf.Cos(radians) * maxRadius,
                    Mathf.Sin(radians) * maxRadius,
                    0
                ) + pivotOffset;

                if (cardList[i].TryGetComponent<CardHoverUI>(out var hoverUI))
                {
                    hoverUI.SetPosition(localPos);
                }

                // 좌표 및 회전 적용
                if (Application.isPlaying)
                {
                    cardList[i].DOKill(); 

                    cardList[i].DOAnchorPos(localPos, duration).SetEase(Ease.OutCubic);
                    cardList[i].DOLocalRotate(new Vector3(0, 0, targetAngle), duration).SetEase(Ease.OutCubic);
                }
                else
                {
                    cardList[i].anchoredPosition = localPos;
                    cardList[i].localRotation = Quaternion.Euler(0, 0, targetAngle);
                }
            }
        }
    
        private void OnValidate()
        {
            ArrangeCards();
        }
    }
}
