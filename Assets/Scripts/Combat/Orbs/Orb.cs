using Combat.Cards;
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
            InitCard();
            
            Destroy(gameObject);
        }
    }
}
