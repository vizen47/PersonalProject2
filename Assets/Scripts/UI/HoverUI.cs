using Audio;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class HoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Vector3 targetScale;
        [SerializeField] private float duration;
        private Vector3 _startScale;
        private RectTransform _targetRect;
        
        private void  Awake()
        {
            _targetRect = transform.GetComponent<RectTransform>();
            _startScale =  _targetRect.localScale;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            Enter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Exit();
        }

        private void Enter()
        {   
            _targetRect.DOScale(targetScale, duration).SetEase(Ease.OutExpo).SetUpdate(true);
            SoundManager.Instance.PlaySFXOnChannel(1, transform.position, SoundManager.Instance.HoverUI);
        }

        private void Exit()
        {
            _targetRect.DOScale(_startScale, duration).SetEase(Ease.OutExpo).SetUpdate(true);
        }
    }
}