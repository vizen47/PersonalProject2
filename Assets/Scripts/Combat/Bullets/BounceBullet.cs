using UnityEngine;

namespace Combat.Bullets
{
    public class BounceBullet : Bullet
    {
        [SerializeField] private ContactFilter2D filter;
        
        private Vector2 lastFrameVelocity;
        private float _timer;
        
        private void Start()
        {
            _timer = 0f;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            
            lastFrameVelocity = _rigid.linearVelocity; 
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            if (_timer >= _lifeTime)
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