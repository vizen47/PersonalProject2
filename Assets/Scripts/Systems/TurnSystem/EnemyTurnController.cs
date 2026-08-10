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
        private void OnDisable() => TurnManager.Instance.CurrentState.OnValueChanged -= OnTurnChanged;

        private void OnTurnChanged(TurnManager.TurnState prev, TurnManager.TurnState next)
        {
            if (next == TurnManager.TurnState.EnemyTurn)
            {
                StartCoroutine(RunEnemyTurn());
            }
        }

        private IEnumerator RunEnemyTurn()
        {
            foreach (EnemyAttackModule enemy in Enemies)
            {
                if (enemy == null) continue; // 이미 죽은 적은 건너뜀

                yield return enemy.Attack(); // 이 적의 공격이 "끝날 때까지" 기다림
            }

            TurnManager.Instance.EndEnemyTurn(); // 전부 다 쐈으면 턴 종료
        }
    }
}