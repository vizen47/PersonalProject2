using System.Collections;
using Combat.Bullets;
using Systems;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Combat.Cards
{
    public class Card : MonoBehaviour
    {
        public UnityEvent onCardUse;
        
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private PoolItemSO bullet;
        
        private CardHoverUI cardHoverUI;
        
        // 자기 자신은 사라지고
        // 카드 능력 리스트에 추가. 그리고 사라지는 효과 실행(onCardUse)

        private void Awake()
        {
            cardHoverUI = GetComponent<CardHoverUI>();
        }
        
        public void ChangeCardData(Sprite cardIcon, string cardTitle, string cardDescription)
        {
            icon.sprite = cardIcon;
            titleText.text = cardTitle;
            descriptionText.text = cardDescription;
        }

        private void Update()
        {
            if (Keyboard.current.yKey.wasPressedThisFrame && cardHoverUI.IsHovered)
            {
                onCardUse?.Invoke();
                
                BulletManager.Instance.SetBullet(bullet);
            }
        }
    }
}
