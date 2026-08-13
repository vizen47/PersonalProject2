using Systems.TurnSystem;
using TMPro;
using UnityEngine;

namespace UI.TextUI
{
    public class TurnTextBoxUI : MonoBehaviour
    {
        private TextMeshProUGUI targetText;
        [TextArea] [SerializeField] private string winText;
        [TextArea] [SerializeField] private string loseText;
        
        private void Awake()
        {
            targetText = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            SetText();
        }

        private void SetText()
        {
            if (TurnManager.Instance.CurrentState.Value == TurnManager.TurnState.Win)
            {
                targetText.SetText(winText);
            }
            else if (TurnManager.Instance.CurrentState.Value == TurnManager.TurnState.Lose)
            {
                targetText.SetText(loseText);
            }
        }
    }
}
