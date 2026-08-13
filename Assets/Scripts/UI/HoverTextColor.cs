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
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            BrightText();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ResetTextColor();
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