using System.Collections;
using Combat.Bullets;
using DG.Tweening;
using Systems;
using Systems.Pooling;
using Systems.TurnSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Combat.Enemies
{
    public class EnemyAttackModule : MonoBehaviour
    {
        public UnityEvent onFire;
        
        [field: SerializeField] private Transform rangeTrm;
        [SerializeField] private Transform firePos;
        [Header("Settings")]
        [SerializeField] private PoolItemSO setBullet;
        [SerializeField] private float rotateSpeed = 0.5f;
        
        private bool _bulletIsDestroyed;
        
        public IEnumerator Attack()
        {
            if (TurnManager.Instance.CurrentState.Value == TurnManager.TurnState.EnemyTurn)
            {
                RotateRangeTrm();
                _bulletIsDestroyed = false;
                yield return new WaitForSeconds(rotateSpeed);
                Shoot();
            }
                
            yield return new WaitUntil(() => _bulletIsDestroyed);
        }
        
        private void RotateRangeTrm()
        {
            Vector3 offset = gameObject.transform.root.localEulerAngles;
            Vector2 target = GameManager.Instance.playerTrm.position - rangeTrm.position;
            
            rangeTrm.transform.DOLocalRotate(new Vector3(0, 0, Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg - 90 + offset.z ), rotateSpeed);
        }
        
        public void Shoot()
        {
            Projectile projectile = PoolManager.Instance.Pop(setBullet.ItemName) as Projectile;
            
            if (projectile == null)
            {
                _bulletIsDestroyed = true;
                return;
            }

            projectile.InitAndFire(firePos: firePos, firePower: 50);

            if (projectile is Bullet bullet)
                bullet.onExplosion.AddListener(OnBulletFinished);

            onFire?.Invoke();
        }

        private void OnBulletFinished()
        {
            _bulletIsDestroyed = true;
        }
    }
}