using System;
using Audio;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class HoverTextColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private VertexGradient finishGradient;
        private VertexGradient startGradient;
        [SerializeField] private TextMeshProUGUI targetText;

        private void Awake()
        {
            startGradient = targetText.colorGradient;
        }

        private void OnEnable()
        {
            ResetTextColor();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            BrightText();
            HoverSound();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ResetTextColor();
        }

        private void HoverSound()
        {
            SoundManager.Instance.PlaySFXOnChannel(0, transform.position, SoundManager.Instance.HoverUI);
        }
        
        private void BrightText()
        {
            targetText.colorGradient = finishGradient;
        }

        private void ResetTextColor()
        {
            targetText.colorGradient = startGradient;
        }
    }
}