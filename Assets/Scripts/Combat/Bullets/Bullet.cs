using Systems;
using UnityEngine;

namespace Combat.Bullets
{
    public class Bullet : Projectile, IPoolable
    {
        [field: SerializeField] public PoolItemSO PoolItem { get; private set; }
        public GameObject GameObject => gameObject;

        private Rigidbody2D _rigid;
        private float _firePower; // 발사 파워는 마우스로 조절된 값을 받아서 써야함.
        private float _lifeTime = 10f;
        
        [Header("Bullet Settings")]
        [SerializeField] private BulletDataSO bulletData;
        private int _damage;
        private float _knockbackPower;
        private Vector3 _fireDirection;
        
        protected override void Awake()
        {
            base.Awake();
            _rigid = GetComponent<Rigidbody2D>();
        }

        private void Fire()
        {
            _rigid.AddForce(_fireDirection * _firePower, ForceMode2D.Impulse);
        }
        
        public override void InitAndFire(Transform firePos, int damage, float knockbackPower, float firePower)
        {
            _damage = damage;
            _knockbackPower = knockbackPower;
            _firePower = firePower;
            
            transform.SetPositionAndRotation(firePos.position, firePos.rotation);
            
            _fireDirection = firePos.right;
            _rigid.linearVelocity = Vector2.zero;
            _rigid.angularVelocity = 0f;
            
            Fire();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (IsDead) return;
            IsDead = true;
            
            // 닿았을 때 이펙트

            DestroyBullet();
        }

        private void DestroyBullet()
        {
            PoolManager.Instance.Push(this);
        }
    }
}
