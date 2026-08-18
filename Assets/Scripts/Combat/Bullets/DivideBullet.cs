using System.Collections.Generic;
using Systems.Pooling;
using UnityEngine;

namespace Combat.Bullets
{
    public class DivideBullet : Bullet
    {
        [SerializeField] private Transform[] spawnPoint;
        [SerializeField] private PoolItemSO[] divideBullets;

        protected override void Update()
        {
            if (IsDead) return;

            _timer += Time.deltaTime;

            if (_timer >= _lifeTime || _bulletScreen.IsOffscreen())
            {
                IsDead = true;
                base.DestroyBullet();
            }
        }
        
        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            if (_timer >= _lifeTime)
            {
                base.OnCollisionEnter2D(collision);
                return;
            }
            
            int groundLayer = LayerMask.NameToLayer("Ground");

            if (collision.gameObject.layer == groundLayer)
            {
                var spawnedGroup = new List<SmallBullet>();
                for (int i = 0; i < divideBullets.Length; i++)
                {
                    IPoolable obj = PoolManager.Instance.Pop(divideBullets[i].ItemName);
                    obj.GameObject.transform.position = spawnPoint[i].position;

                    if (obj is SmallBullet small)
                    {
                        small.SetGroup(spawnedGroup);
                        spawnedGroup.Add(small);
                    }
                }

                PoolManager.Instance.Push(this);
            }
        }

        protected override void DestroyBullet()
        {
            PoolManager.Instance.Push(this);
        }
    }
}