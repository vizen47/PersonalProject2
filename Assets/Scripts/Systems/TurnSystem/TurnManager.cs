using CoreLib;
using Stages;
using UnityEngine;

namespace Systems.TurnSystem
{
    public class TurnManager : MonoBehaviour
    {
        public enum TurnState
        {
            PlayerTurn = 0,
            EnemyTurn = 1,
            Win = 2,
            Lose = 3
        }
        
        public static TurnManager Instance;
        
        [field: SerializeField] public int CurrentTurn { get; private set; }
        [field: SerializeField] public int MaxTurn { get; private set; }
        public NotifyValue<TurnState> CurrentState { get; private set; }
        
        [field: SerializeField] public bool IsActing { get; private set; }
        
        private void Awake()
        {
            Instance = this;
            CurrentState = new NotifyValue<TurnState>();
        }
        
        private void InitTurn()
        {
            MaxTurn = FindAnyObjectByType<Stage>().StageInfo.maxTurn;
            CurrentTurn = 0;
            CurrentState.Value = TurnState.PlayerTurn;
        }

        private void Start() => InitTurn();

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

            if (CheckLose()) { CurrentState.Value = TurnState.Lose; return; }
            if (CheckWin())  { CurrentState.Value = TurnState.Win;  return; }

            CurrentState.Value = TurnState.EnemyTurn;
        }

        public void EndEnemyTurn()
        {
            if (CurrentState.Value != TurnState.EnemyTurn) return;

            if (CheckLose()) { CurrentState.Value = TurnState.Lose; return; }
            if (CheckWin())  { CurrentState.Value = TurnState.Win;  return; }

            CurrentTurn++;
            CurrentTurn = Mathf.Clamp(CurrentTurn, 0, MaxTurn);

            if (CurrentTurn >= MaxTurn)
            {
                CurrentState.Value = TurnState.Lose;
                return;
            }

            CurrentState.Value = TurnState.PlayerTurn;
        }

        private bool CheckWin()
        {
            // 모든 적 처치 여부
            return false;
        }

        private bool CheckLose()
        {
            // 플레이어 체력이 0인지
            return false;
        }
    }
}
