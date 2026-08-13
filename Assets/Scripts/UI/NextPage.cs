using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class NextPage : MonoBehaviour
    {
        [SerializeField] private RectTransform[] targetRect;
        private Vector2 _movePageVec = new Vector2(1920, 0);
        private int cnt;
        
        public void ChangePage(bool isRight)
        {
            if (isRight)
            {
                foreach (RectTransform page in targetRect)
                {
                    page.DOAnchorPos(page.anchoredPosition - _movePageVec, 0.5f).SetEase(Ease.OutExpo);
                    cnt = Mathf.Clamp(cnt + 1, 0, targetRect.Length);
                }
            }
            else
            {
                foreach (RectTransform page in targetRect)
                {
                    page.DOAnchorPos(page.anchoredPosition + _movePageVec, 0.5f).SetEase(Ease.OutExpo);
                    cnt = Mathf.Clamp(cnt - 1, 0, targetRect.Length);
                }
            }
        }
    }
}