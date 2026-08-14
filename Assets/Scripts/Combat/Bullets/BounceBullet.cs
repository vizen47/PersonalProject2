using UnityEngine;

namespace Combat.Bullets
{
    public class BounceBullet : Bullet
    {
        [SerializeField] private float detectRange;
        [SerializeField] private ContactFilter2D filter;
        
        private Vector2 lastFrameVelocity;
        
        protected override void Update()
        {
           base.Update(); 
           if (IsDead) return;
           
            lastFrameVelocity = _rigid.linearVelocity; 
        }

        private bool CheckIDamageable()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectRange);
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out IDamageable _))
                    return true;
            }
            return false;
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            if (CheckIDamageable() || _timer >= _lifeTime)
            {
                base.OnCollisionEnter2D(collision);
                return;
            }
            
            Vector2 normal = collision.contacts[0].normal;
            
            Vector2 reflectedDirection = Vector2.Reflect(lastFrameVelocity.normalized, normal);

            _rigid.linearVelocity = reflectedDirection * (lastFrameVelocity.magnitude);
        }
    }
}