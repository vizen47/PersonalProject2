using Combat.Cards;
using Systems.TurnSystem;
using UI;
using UnityEngine;

namespace Combat.Orbs
{
    public class Orb : MonoBehaviour
    {
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private Transform cardContainer;
        
        private void InitCard()
        {
            Instantiate(cardPrefab, cardContainer);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (TurnManager.Instance.CurrentState.Value == TurnManager.TurnState.PlayerTurn)
            {
                InitCard();

                cardContainer.gameObject.GetComponent<CardListContainerUI>().ArrangeCards();
                
                Destroy(gameObject);
            }
        }
    }
}
