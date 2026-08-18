using System.Collections.Generic;
using Systems.Pooling;
using UnityEngine;

namespace Combat.Bullets
{
    public class SplitBullet : Bullet
    {
        [SerializeField] private Transform[] spawnPoint;
        [SerializeField] private PoolItemSO[] smallSplitBullets;

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
            int enemyLayer = LayerMask.NameToLayer("Enemy");
            
            if (collision.gameObject.layer == groundLayer || collision.gameObject.layer == enemyLayer)
            {
                var spawnedGroup = new List<SmallSplitBullet>();
                for (int i = 0; i < smallSplitBullets.Length; i++)
                {
                    IPoolable obj = PoolManager.Instance.Pop(smallSplitBullets[i].ItemName);
                    obj.GameObject.transform.position = spawnPoint[i].position;
                    if (i == 0)
                    {
                        obj.GameObject.GetComponent<SmallSplitBullet>().Direction = -transform.up;
                    }
                    else
                    {
                        obj.GameObject.GetComponent<SmallSplitBullet>().Direction = transform.up;
                    }

                    if (obj is SmallSplitBullet small)
                    {
                        small.SetGroup(spawnedGroup);
                        spawnedGroup.Add(small);
                    }
                }

                onExplosion?.Invoke();
                PoolManager.Instance.Push(this);
            }
        }

        protected override void DestroyBullet()
        {
            PoolManager.Instance.Push(this);
        }
    }
}