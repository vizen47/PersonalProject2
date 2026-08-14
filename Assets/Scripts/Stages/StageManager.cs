using CoreLib;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Stages
{
    public class StageManager : MonoSingleton<StageManager>
    {
        [SerializeField] private FadeInOut fadeInOut;
        [field: SerializeField] public int CurrentStage { get; private set; }
        public int currentStageNumber;
        
        public void SetCurrangeStage(int value)
        {
            CurrentStage = value;
        }
        
        public void StartLevel(int number)
        {
            currentStageNumber = number;
        }
    }
}
