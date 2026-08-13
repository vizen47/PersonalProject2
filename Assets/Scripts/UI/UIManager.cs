using CoreLib;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoSingleton<UIManager>
    {
        public bool IsActiveCard { get; private set; }
    
        [field: SerializeField] public GameObject UseCardUI {get; private set;}

        private void Update()
        {
            if (IsActiveCard)
                UseCardUI.SetActive(true);
            else
                UseCardUI.SetActive(false);
        }

        public void CheckIsHoveringCard(bool isHoveringCard)
        {
            IsActiveCard = isHoveringCard;
        }
    }
}
