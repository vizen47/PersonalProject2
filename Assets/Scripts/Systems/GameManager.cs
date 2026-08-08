using Stages;
using UnityEngine;

namespace Systems
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
        [field: SerializeField] public Transform PlayerTrm { get; private set; }
        [field: SerializeField] public int CurrentTurn { get; private set; }
        [field: SerializeField] public int MaxTurn { get; private set; }
        
        private void Awake()
        {
            Instance = this;
        }

        private void Start() => InitTurn();

        public void UseTurn()
        {
            CurrentTurn++;
            CurrentTurn = Mathf.Clamp(CurrentTurn, 0, MaxTurn);
        }
        
        private void InitTurn()
        {
            MaxTurn = FindAnyObjectByType<Stage>().StageInfo.maxTurn;
            CurrentTurn = 0;
        }
    }
}
