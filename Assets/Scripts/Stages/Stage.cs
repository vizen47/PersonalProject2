using UnityEngine;

namespace Stages
{
    public class Stage : MonoBehaviour
    {
        [field: SerializeField] public StageSO StageInfo { get; private set; }
    }
}