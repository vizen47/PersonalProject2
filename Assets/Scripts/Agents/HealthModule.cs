using CoreLib;
using Systems;
using UnityEngine;

namespace Agents
{
    public class HealthModule : MonoBehaviour, IDamageable
    {
        private const int DefaultHealth = 100;
        
        // [field: SerializeField] public int CurrentHealth { get; private set; }
        public NotifyValue<int> CurrentHealth { get; set; } = new NotifyValue<int>();
        [field: SerializeField] public int MaxHealth { get; private set; } = DefaultHealth;

        private HealthVisual healthVisual;

        private void Awake()
        {
            healthVisual = TryGetComponent(out HealthVisual compo) ?  compo : null;
        }

        private void Start()
        {            
            CurrentHealth.Value = MaxHealth;
        }
        
        public void ApplyDamage(int amount)
        {
            CurrentHealth.Value -= amount;
            CurrentHealth.Value = Mathf.Clamp(CurrentHealth.Value, 0, MaxHealth);

            if (healthVisual != null)
            {
#if UNITY_EDITOR
                Debug.Log($"{gameObject.transform.root.name}의 체력: {CurrentHealth.Value}");
#endif
                healthVisual.SetVisualHealthBar(CurrentHealth.Value);
            }
        }
    }
}