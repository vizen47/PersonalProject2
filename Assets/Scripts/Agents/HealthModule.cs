using CoreLib;
using Systems;
using UnityEngine;

namespace Agents
{
    public class HealthModule : MonoBehaviour, IDamageable
    {
        private const int DefaultHealth = 100;
        
        public NotifyValue<int> CurrentHealth { get; set; } = new NotifyValue<int>();
        [field: SerializeField] public int MaxHealth { get; private set; } = DefaultHealth;
        [SerializeField] private AgentDead agentDead;

        private HealthVisual healthVisual;

        private void Awake()
        {
            healthVisual = TryGetComponent(out HealthVisual compo) ? compo : null;
        }

        private void Start()
        {            
            CurrentHealth.Value = MaxHealth;
            CurrentHealth.OnValueChanged += HandleHealthChange;
        }
        
        private void OnDestroy() => CurrentHealth.OnValueChanged -= HandleHealthChange;
        
        private void HandleHealthChange(int prev, int next)
        {
            if (healthVisual != null)
            {
#if UNITY_EDITOR
                Debug.Log($"{gameObject.transform.root.name}의 체력: {next}");
#endif
                healthVisual.SetVisualHealthBar(next);
            }

            if (next <= 0)
            {
                agentDead.Dead();
#if UNITY_EDITOR
                Debug.Log($"{gameObject.transform.root.name}가 죽었습니다.");
#endif
            }
        }

        public void ApplyDamage(int amount)
        {
            int newHealth = Mathf.Clamp(CurrentHealth.Value - amount, 0, MaxHealth);
            CurrentHealth.Value = newHealth;
        }
    }
}