using System.Collections;
using UnityEngine;

namespace Combat.Bullets
{
    public class GravityBullet : Bullet
    {
        [SerializeField] private float gravityTime = 1f;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _rigid.gravityScale = 1f;
        }

        protected override void Update()
        {
            if (IsDead) return;

            _timer += Time.deltaTime;

            if (_timer >= gravityTime)
            {
                _rigid.gravityScale = -5f;
            }
            
            if (_timer >= _lifeTime || _bulletScreen.IsOffscreen())
            {
                IsDead = true;
                DestroyBullet();
            }
        }
    }
}