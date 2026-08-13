using System;
using Combat.Bullets;
using Systems.Pooling;
using Systems.TurnSystem;
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
        private Vector4 invisibleCardValue;
        
        private CardHoverUI cardHoverUI;
            
        private void Awake()
        {
            cardHoverUI = GetComponent<CardHoverUI>();
            invisibleCardValue =  new Vector4(0f, 0f, 0f, 0f);
        }

        private void Start() => onCardUse.AddListener(UsePlayerTurn);

        private void OnDestroy() => onCardUse.RemoveListener(UsePlayerTurn);

        private void Update()
        {
            if (TurnManager.Instance.CurrentState.Value != TurnManager.TurnState.PlayerTurn || TurnManager.Instance.IsActing) return;
            
            if (Keyboard.current.yKey.wasPressedThisFrame && cardHoverUI.IsHovered)
            {
                onCardUse?.Invoke();

                InvisibleCardText();
                BulletManager.Instance.SetBullet(bullet);
            }
        }

        private void UsePlayerTurn()
        {
            TurnManager.Instance.StartAction();
        }
        
        private void InvisibleCardText()
        {
            icon.color = invisibleCardValue;
            titleText.text = "";
            descriptionText.text = "";
        }
    }
}
