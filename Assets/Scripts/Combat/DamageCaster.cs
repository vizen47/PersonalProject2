using System.Collections;
using Combat.Bullets;
using Systems;
using UnityEngine;

namespace Combat
{
    public class DamageCaster : MonoBehaviour
    {
        [SerializeField] private float attackRange;
        private int damage;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void OnDamageCast()
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, attackRange);
            if (hit.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(damage);
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
