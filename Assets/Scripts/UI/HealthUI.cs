using Agents;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private HealthModule health;
        [SerializeField] private TextMeshProUGUI healthText;
        
        private Slider healthSlider;
        
        private void Awake()
        {
            healthSlider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            health.CurrentHealth.OnValueChanged += HandleChangeHealth;
            
            healthSlider.value = (float)health.CurrentHealth.Value / health.MaxHealth;
        }
        
        private void OnDisable()
        {
            health.CurrentHealth.OnValueChanged -= HandleChangeHealth;
        }
        
        private void HandleChangeHealth(int prev, int next)
        {
            float ratio = (float)next / health.MaxHealth;
            healthSlider.DOValue(ratio, 1).SetEase(Ease.InOutCubic);
            healthText.SetText(next + " / " + health.MaxHealth);
        }
    }
}
