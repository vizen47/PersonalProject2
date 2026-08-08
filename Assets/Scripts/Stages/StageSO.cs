using UnityEngine;

namespace Stages
{
    [CreateAssetMenu(fileName = "Stage Data", menuName = "SO/Stage data", order = 15)]
    public class StageSO : ScriptableObject
    {
        public int level;
        public int maxTurn;
    }
}