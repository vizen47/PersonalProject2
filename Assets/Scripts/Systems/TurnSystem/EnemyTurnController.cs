using System.Collections;
using System.Collections.Generic;
using Combat.Enemies;
using UnityEngine;

namespace Systems.TurnSystem
{
    public class EnemyTurnController : MonoBehaviour
    {
        [field: SerializeField] public List<EnemyAttackModule> Enemies { get; private set; }

        private void Start() => TurnManager.Instance.CurrentState.OnValueChanged += OnTurnChanged;

        private void OnDisable()
        {
            if (TurnManager.Instance != null)
                TurnManager.Instance.CurrentState.OnValueChanged -= OnTurnChanged;
        }
        
        private void OnTurnChanged(TurnManager.TurnState prev, TurnManager.TurnState next)
        {
            if (next == TurnManager.TurnState.EnemyTurn)
            {
                StartCoroutine(RunEnemyTurn());
            }
        }

        private IEnumerator RunEnemyTurn()
        {
            var list = new List<EnemyAttackModule>(Enemies);
    
            foreach (EnemyAttackModule enemy in list)
            {
                if (enemy == null) continue;
                yield return enemy.Attack();
            }
    
            TurnManager.Instance.EndEnemyTurn();
        }

        public void RemoveEnemyList(EnemyAttackModule enemy)
        {
            Enemies.Remove(enemy);
        }
    }
}