using UnityEngine;

namespace Systems
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public Transform playerTrm;
        
        private void Awake()
        {
            Instance = this;
        }
    }
}
