using TMPro;
using UnityEngine;

namespace Stages
{
    public class StageUI : MonoBehaviour
    {
        [Header("UI Controls")]
        [TextArea] [SerializeField] private string title;
        [SerializeField] private TextMeshProUGUI titleTmg;
        
        public void Init(int stageLevel)
        {
            titleTmg.SetText(title);
            StageManager.Instance.SetCurrangeStage(stageLevel);
        }
    }
}