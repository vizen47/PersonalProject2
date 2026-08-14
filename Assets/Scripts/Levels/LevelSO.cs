using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{
    [CreateAssetMenu(fileName = "Level data", menuName = "SO/Level/Data")]
    public class LevelSO : ScriptableObject
    {
        public Scene Scene;
    }
}