using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class OnSettingUI : MonoBehaviour
    {
        [SerializeField] private GameObject[] target;

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
                foreach (var newTarget in target)
                {
                    Time.timeScale = 1;
                    newTarget.SetActive(false);
                    IsSetting = false;
                }
            }
            else
            {
                Time.timeScale = 0;
                target[0].SetActive(true);
                IsSetting = true;
            }
        }
    }
}