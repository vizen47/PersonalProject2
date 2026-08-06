    using DG.Tweening;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    namespace UI
    {
        [RequireComponent(typeof(Canvas), typeof(GraphicRaycaster))]
        public class CardHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
        {
            [SerializeField] private float  duration = 0.2f;
            [SerializeField] private Vector2 hoverOffset = new Vector2(0, 80f);
            private Vector2 basePosition;
            private RectTransform rect;
            private Canvas _canvas;
            public bool IsHovered { get; private set; }
            
            private void CacheComponents()
            {
                if (rect == null)
                    rect = GetComponent<RectTransform>();

                if (_canvas == null)
                    _canvas = GetComponent<Canvas>();
            }
            
            public void SetPosition(Vector2 position)
            {
                CacheComponents();
                
                basePosition = position;
                
                if (!IsHovered)
                    rect.anchoredPosition = basePosition;
            }
            
            public void OnPointerEnter(PointerEventData eventData)
            {
                CacheComponents();
                
                IsHovered = true;
                UIManager.Instance.CheckIsHoveringCard(IsHovered);
                
                rect.DOKill();
                _canvas.overrideSorting = true;
                _canvas.sortingOrder = 100;
                rect.DOAnchorPos(basePosition + hoverOffset, duration).SetEase(Ease.OutCubic);
            }

            public void OnPointerExit(PointerEventData eventData)
            {
                IsHovered = false;
                UIManager.Instance.CheckIsHoveringCard(IsHovered);

                rect.DOKill();
                _canvas.overrideSorting = false;
                rect.DOAnchorPos(basePosition, duration).SetEase(Ease.OutCubic);
            }
        }
    }
