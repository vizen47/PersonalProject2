using System.Xml;
using Effects;
using Systems;
using UnityEngine;
using UnityEngine.Events;

namespace Combat.Bullets
{
    public class Bullet : Projectile, IPoolable
    {
        public UnityEvent onExplosion;
        
        [field: SerializeField] public PoolItemSO PoolItem { get; private set; }
        [SerializeField] private PoolItemSO impactEffect;

        public GameObject GameObject => gameObject;

        protected DamageCaster DamageCaster;
        protected Rigidbody2D _rigid;
        protected float _firePower; // 발사 파워는 마우스로 조절된 값을 받아서 써야함.
        protected float _lifeTime = 5f;
        
        [Header("Bullet Settings")]
        [field: SerializeField] public int Damage {get; private set;}
        protected Vector3 _fireDirection;
        
        protected override void Awake()
        {
            base.Awake();
            _rigid = GetComponent<Rigidbody2D>();
            DamageCaster = GetComponentInChildren<DamageCaster>();
        }

        protected virtual void Fire()
        {
            _rigid.AddForce(_fireDirection * _firePower, ForceMode2D.Impulse);
        }
        
        public override void InitAndFire(Transform firePos, float firePower)
        {
            _firePower = firePower;
            
            transform.SetPositionAndRotation(firePos.position, firePos.rotation);
            
            _fireDirection = firePos.right;
            _rigid.linearVelocity = Vector2.zero;
            _rigid.angularVelocity = 0f;
            
            Fire();
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (IsDead) return;
            IsDead = true;
            
            if (impactEffect != null)
            {
                EffectPlayer effect = PoolManager.Instance.Pop(impactEffect.ItemName) as EffectPlayer;
                effect.SetPositionAndPlay(transform.position);
            }
            onExplosion?.Invoke(); // 닿았을 때 이펙트

            DamageCaster.OnDamageCast();
            
            DestroyBullet();
        }
        
        protected virtual void DestroyBullet()
        {
            PoolManager.Instance.Push(this);
        }
    }
}
