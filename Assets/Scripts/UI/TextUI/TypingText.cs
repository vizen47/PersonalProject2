using System.Collections;
using Audio;
using Systems.TurnSystem;
using TMPro;
using UnityEngine;

namespace UI.TextUI
{
    public class TypingText : MonoBehaviour
    {
        [TextArea][SerializeField] private string winTextStr;
        [TextArea][SerializeField] private string loseTextStr;
        [SerializeField] private TextMeshProUGUI targetText;
        [SerializeField] private SoundClipSO typingSound;
        private readonly WaitForSeconds waitForSeconds =  new WaitForSeconds(0.05f);
        
        private void OnEnable()
        {
            targetText.text = "";
            StartCoroutine(Typing());
        }

        private IEnumerator Typing()
        {
            if (TurnManager.Instance.CurrentState.Value == TurnManager.TurnState.Win)
            {
                foreach (char c in winTextStr)
                {
                    yield return waitForSeconds;
                    SoundManager.Instance.PlaySFXOnChannel(0, transform.position, typingSound);
                    targetText.text += c;
                }
            }
            else if (TurnManager.Instance.CurrentState.Value == TurnManager.TurnState.Lose)
            {
                foreach (char c in loseTextStr)
                {
                    yield return waitForSeconds;
                    SoundManager.Instance.PlaySFXOnChannel(0, transform.position, typingSound);
                    targetText.text += c;
                }
            }
        }
    }
}