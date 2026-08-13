using Systems.TurnSystem;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TurnUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentTurnText;

        private void Start()
        {
            TurnManager.Instance.CurrentTurn.OnValueChanged += HandleTurnUI;
            currentTurnText.SetText($"{0}/{TurnManager.Instance.MaxTurn}");
        }

        private void OnDestroy()
        {
            if (TurnManager.Instance != null)
                TurnManager.Instance.CurrentTurn.OnValueChanged -= HandleTurnUI;
        }
            
        
        private void HandleTurnUI(int prev, int next)
        {
            if (next != prev)
            {
                currentTurnText.SetText($"{next}/{TurnManager.Instance.MaxTurn}");
            }
        }
    }
}