using Systems;
using UnityEngine;

namespace Agents
{
    public class HealthModule : MonoBehaviour, IDamageable
    {
        private const int DefaultHealth = 100;
        
        [field: SerializeField] public int CurrentHealth { get; private set; }
        [field: SerializeField] public int MaxHealth { get; private set; } = DefaultHealth;

        private HealthVisual healthVisual;

        private void Awake()
        {
            healthVisual = TryGetComponent(out HealthVisual compo) ?  compo : null;
        }

        private void Start()
        {            
            CurrentHealth = MaxHealth;
        }
        
        public void ApplyDamage(int amount)
        {
            CurrentHealth -= amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

            if (healthVisual != null)
            {
#if UNITY_EDITOR
                Debug.Log($"{gameObject.transform.root.name}의 체력: {CurrentHealth}");
#endif
                healthVisual.SetVisualHealthBar(CurrentHealth);
            }
        }
    }
}