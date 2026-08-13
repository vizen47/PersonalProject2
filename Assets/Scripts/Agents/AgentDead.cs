using Combat.Enemies;
using Systems.TurnSystem;
using UnityEngine;

namespace Agents
{
    public class AgentDead : MonoBehaviour
    {
        [SerializeField] private GameObject deadParticle;
        [SerializeField] private bool isPlayer;

        private bool _isDead;

        private void Update()
        {
            deadParticle.transform.localEulerAngles = Vector3.zero - transform.root.rotation.eulerAngles;
        }
        
        public void Dead()
        {
            if (_isDead) return;
            _isDead = true;

            deadParticle.SetActive(true);

            if (isPlayer)
            {
                TurnManager.Instance.CurrentState.Value = TurnManager.TurnState.Lose;
                TurnManager.Instance.CheckLose();
            }
            else
            {
                EnemyAttackModule enemy = transform.root.GetComponentInChildren<EnemyAttackModule>();
                if (enemy != null)
                    TurnManager.Instance.EnemyTurnController.RemoveEnemyList(enemy);

                TurnManager.Instance.CheckWin(); // 적 죽을 때마다 승리 조건 체크
            }
        }
    }
}