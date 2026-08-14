using Agents;
using Combat.Enemies;
using CoreLib;
using Players;
using Stages;
using UnityEngine;
using UnityEngine.Events;

namespace Systems.TurnSystem
{
    public class TurnManager : MonoSingleton<TurnManager>
    {
        public UnityEvent onWinEffect;
        public UnityEvent onLoseEffect;
        
        [SerializeField] private PlayerAttackModule playerAttackModule;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private HealthModule playerHealth;
        
        public enum TurnState
        {
            PlayerTurn = 0,
            EnemyTurn = 1,
            Win = 2,
            Lose = 3
        }
        
        [field: SerializeField] public EnemyTurnController EnemyTurnController {get; private set;}
        
        [field: SerializeField] public int MaxTurn { get; private set; }
        
        public NotifyValue<int> CurrentTurn { get; private set; }
        public NotifyValue<TurnState> CurrentState { get; private set; }
        
        public bool IsActing { get; private set; }


        protected override void Awake()
        {
            base.Awake();
            
            CurrentState = new NotifyValue<TurnState>();
            CurrentTurn =  new NotifyValue<int>();
            InitTurn();
        }

        private void Start() => CurrentTurn.OnValueChanged += HandleCheckLose;

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            CurrentTurn.OnValueChanged -= HandleCheckLose;
        } 

        private void HandleCheckLose(int prev, int next)
        {
            if (CurrentTurn.Value >= MaxTurn)
            {
                CurrentState.Value = TurnState.Lose;
                onLoseEffect?.Invoke();
            }
        }

        private void InitTurn()
        {
            CurrentTurn.Value = 0;
            CurrentState.Value = TurnState.PlayerTurn;
        }

        public bool StartAction()
        {
            if (IsActing) return false;
            IsActing = true;
            return true;
        }

        public void StopAction()
        {
            IsActing = false;
        }
        
        public void EndPlayerTurn()
        {
            if (CurrentState.Value != TurnState.PlayerTurn) return;

            if ( CurrentState.Value == TurnState.Lose)
                return;
            if ( CurrentState.Value == TurnState.Win)
                return;

            CurrentState.Value = TurnState.EnemyTurn;
            playerAttackModule.InitPlayerAttacked();
        }

        public void EndEnemyTurn()
        {
            if (CurrentState.Value != TurnState.EnemyTurn) return;

            if ( CurrentState.Value == TurnState.Lose)
                return;
            if ( CurrentState.Value == TurnState.Win)
                return;

            CurrentTurn.Value++;
            CurrentTurn.Value = Mathf.Clamp(CurrentTurn.Value, 0, MaxTurn);

            if (CurrentTurn.Value >= MaxTurn)
            {
                CurrentState.Value = TurnState.Lose;
                return;
            }

            CurrentState.Value = TurnState.PlayerTurn;
            playerMovement.TrueCanMove(true);
        }

        public void CheckWin()
        {
            if (EnemyTurnController.Enemies.Count == 0 || CheckEnemyAlive())
            {
                CurrentState.Value = TurnState.Win;
                onWinEffect?.Invoke();
#if UNITY_EDITOR
                Debug.Log("승리");
#endif
            }
        }

        public void CheckLose()
        {
            if (CurrentState.Value == TurnState.Lose || playerHealth.CurrentHealth.Value <= 0)
            {
                onLoseEffect?.Invoke();
#if UNITY_EDITOR
                Debug.Log("패배");
#endif
            }
        }

        private bool CheckEnemyAlive()
        {
            foreach (EnemyAttackModule enemy in EnemyTurnController.Enemies)
            {
                if (enemy != null) return false;
            }
            
            return true;
        }
        
    }
}
