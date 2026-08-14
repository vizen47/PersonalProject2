using Combat.Bullets;
using UnityEngine;

namespace Combat
{
    public class DamageCaster : MonoBehaviour
    {
        [SerializeField] private float attackRange;
        private Bullet bullet; 
        private int damage;

        private void Awake()
        {
            gameObject.SetActive(false);
            bullet = GetComponentInParent<Bullet>();
            damage = bullet.Damage;
        }

        private void OnEnable()
        {
            gameObject.SetActive(false);
        }

        public void OnDamageCast()
        {
            gameObject.SetActive(true);
            
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, attackRange);
            foreach (Collider2D p in hit)
            {   
                if (p.TryGetComponent(out IDamageable damageable))
                {
                    damageable.ApplyDamage(damage);
                }
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
