using UnityEngine;

namespace Systems
{
    public class ThisGameObjectDontDestroy : MonoBehaviour
    {
        private static ThisGameObjectDontDestroy _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}