using Audio;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class AttackButtonHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private RectTransform rectA;
        [SerializeField] private RectTransform rectB;
        private Vector3 moveVecA;
        private Vector3 moveVecB;
        private Vector3 startVecA;
        private Vector3 startVecB;
        
        private void Start()
        {
            startVecA = rectA.anchoredPosition;
            startVecB = rectB.anchoredPosition;
            moveVecA = new Vector3(-10f + startVecA.x, startVecA.y, 0f);
            moveVecB = new Vector3(10f + startVecB.x, startVecB.y, 0f);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            StartControlMotion();
            HoverSound();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            StopControlMotion();
        }

        private void HoverSound()
        {
            SoundManager.Instance.PlaySFXOnChannel(1, transform.position, SoundManager.Instance.HoverUI);
        }
        
        private void StartControlMotion()
        {
            rectA.DOAnchorPos(moveVecA, 0.5f).SetEase(Ease.OutExpo);
            rectB.DOAnchorPos(moveVecB, 0.5f).SetEase(Ease.OutExpo);
        }

        private void StopControlMotion()
        {
            rectA.DOAnchorPos(startVecA, 0.5f).SetEase(Ease.OutExpo);
            rectB.DOAnchorPos(startVecB, 0.5f).SetEase(Ease.OutExpo);
        }
    }
}