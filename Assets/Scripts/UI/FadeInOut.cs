using System.Collections;
using DG.Tweening;
using Stages;
using Systems.TurnSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class FadeInOut : MonoBehaviour
    {
        [SerializeField] private Image fadeInOutImg;
        [SerializeField] private float duration;

        private void Start()
        {
            StartCoroutine(FadeOut());
        }
        
        public void FadeIn()
        {
            fadeInOutImg.raycastTarget = true;

            #region  fadeIn

            // if (TurnManager.Instance != null)
            // {
            //     if (TurnManager.Instance.CurrentState.Value == TurnManager.TurnState.Win)
            //     {
            //         NextLevelFadeIn();
            //     }
            //     else if (TurnManager.Instance.CurrentState.Value == TurnManager.TurnState.Lose)
            //     {
            //         fadeInOutImg.DOFade(1, duration).SetEase(Ease.Linear).OnComplete(() => 
            //             SceneManager.LoadScene($"Level_{StageManager.Instance.CurrentStage}_{StageManager.Instance.currentStageNumber}"));
            //     }
            //     
            //     fadeInOutImg.DOFade(1, duration).SetEase(Ease.Linear).OnComplete(() => 
            //         SceneManager.LoadScene($"MainMenuScene"));
            //     return;
            // }
            
            #endregion

            if (TurnManager.Instance != null)
            {
                if (TurnManager.Instance.CurrentState.Value == TurnManager.TurnState.Win)
                {
                    NextLevelFadeIn();
                    return;
                }
                if (TurnManager.Instance.CurrentState.Value == TurnManager.TurnState.Lose)
                {
                    ContinueCurrentLevel();
                    return;
                }
            }

            fadeInOutImg.DOFade(1, duration).SetEase(Ease.Linear).OnComplete(() => 
                SceneManager.LoadScene($"Level_{StageManager.Instance.CurrentStage}_{StageManager.Instance.currentStageNumber}"));
        }

        public void GoLobby()
        {
            fadeInOutImg.raycastTarget = true;
            
            fadeInOutImg.DOFade(1, duration).SetEase(Ease.Linear).OnComplete(() => 
                SceneManager.LoadScene($"MainMenuScene"));
        }
        
        private void ContinueCurrentLevel()
        {
            fadeInOutImg.raycastTarget = true;
            
            fadeInOutImg.DOFade(1, duration).SetEase(Ease.Linear).OnComplete(() => 
                SceneManager.LoadScene($"Level_{StageManager.Instance.CurrentStage}_{StageManager.Instance.currentStageNumber}"));
        }
        
        private void NextLevelFadeIn()
        {
            fadeInOutImg.raycastTarget = true;

            if (StageManager.Instance.currentStageNumber == 10)
            {
                fadeInOutImg.DOFade(1, duration).SetEase(Ease.Linear).OnComplete(() => 
                SceneManager.LoadScene($"Level_{StageManager.Instance.CurrentStage + 1}_{1}"));
                return;
            }
            
            StageManager.Instance.currentStageNumber++;
            fadeInOutImg.DOFade(1, duration).SetEase(Ease.Linear).OnComplete(() => 
                SceneManager.LoadScene($"Level_{StageManager.Instance.CurrentStage}_{StageManager.Instance.currentStageNumber}"));
        }
        
        private IEnumerator FadeOut()
        {
            fadeInOutImg.color = Color.black;

            yield return null;
            
            fadeInOutImg.raycastTarget = false;

            fadeInOutImg.DOFade(0, duration).SetEase(Ease.Linear);
        }
    }
}