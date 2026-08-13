using Systems.TurnSystem;
using TMPro;
using UnityEngine;

namespace UI.TweenUI
{
    public class TweenTurnTextChangeUI : MonoBehaviour
    {
        [SerializeField] VertexGradient winVertexGradient;
        [SerializeField] VertexGradient defaultVertexGradient;
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            ChangeText();
        }
        
        private void ChangeText()
        {
            if (TurnManager.Instance.CurrentState.Value == TurnManager.TurnState.Win)
            {
                _text.text = "Victory";
                _text.colorGradient =  winVertexGradient;
            }
            else if (TurnManager.Instance.CurrentState.Value == TurnManager.TurnState.Lose)
            {
                _text.text = "Defeat";
                _text.colorGradient = defaultVertexGradient;
            }
        }
    }
}