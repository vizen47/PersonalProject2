using UnityEngine;

namespace Systems.TurnSystem
{
    public class PlayerTurnController : MonoBehaviour
    {
        private void Start() => TurnManager.Instance.CurrentState.OnValueChanged += OnTurnChanged;
        private void OnDisable() => TurnManager.Instance.CurrentState.OnValueChanged -= OnTurnChanged;

        private void OnTurnChanged(TurnManager.TurnState prev, TurnManager.TurnState next)
        {
            bool canAct = next == TurnManager.TurnState.PlayerTurn;

            if (TurnManager.Instance.CurrentState.Value == TurnManager.TurnState.PlayerTurn)
            {
                
            }
        }
    }
}