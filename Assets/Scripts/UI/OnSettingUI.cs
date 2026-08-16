using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class OnSettingUI : MonoBehaviour
    {
        [SerializeField] private GameObject target;

        public bool IsSetting { get; private set; }

        private void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                SetTarget();
            }
        }

        public void SetTarget()
        {
            if (IsSetting)
            {
                Time.timeScale = 1;
                target.SetActive(false);
                IsSetting = false;
            }
            else
            {
                Time.timeScale = 0;
                target.SetActive(true);
                IsSetting = true;
            }
        }
    }
}