using Agents;
using Combat;
using Combat.Bullets;
using CoreLib;
using Systems.Pooling;
using UnityEngine;
using UnityEngine.Events;

namespace Players
{
    public class PlayerAttackModule : MonoBehaviour
    {
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
        public UnityEvent onFire;
        [SerializeField] private PoolItemSO defaultBullet;
    
        [SerializeField] private PlayerAimController playerAimController;
    
        [Header("Parts")]
        [SerializeField] private GameObject attackRange;
        [SerializeField] private GameObject attackReach;
        [SerializeField] private Transform firePos;
        private Camera _camera;

        public readonly NotifyValue<float> CurrentPower = new NotifyValue<float>();
    
        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            SetSize();
            ShowAttackRange();
            DrawRange();
        }

        private void DrawRange()
        {
            if (PlayerInput.AimRangeInput)
            {
                attackRange?.SetActive(true);
            }
            else
            {
                attackRange.SetActive(false);
            }
        }

        private void SetSize()
        {
            if (PlayerInput.AimRangeInput)
            {
                Vector3 screenPos = PlayerInput.AimInput;
                screenPos.z = -_camera.transform.position.z;

                Vector3 worldPos = _camera.ScreenToWorldPoint(screenPos);

                float distance = Vector2.Distance(attackRange.transform.position, worldPos);
                distance = Mathf.Clamp(distance, 0.15f, 1f);
                CurrentPower.Value = distance;
            }
        }

        private void ShowAttackRange()
        {
            attackReach.transform.localScale = new Vector3(CurrentPower.Value,1, 1);
        }

        public void Shoot()
        {
            // Projectile projectile =
            //     PoolManager.Instance.Pop(bullet.list[1].ItemName) as Projectile;
        
            Projectile projectile =
                PoolManager.Instance.Pop(BulletManager.Instance.CurrentBullet.ItemName) as Projectile;
            
            if (projectile == null) return;
        
            projectile.InitAndFire // 현재 쏠 차례가 된 총알의 정보(데미지, 힘, 넉백의 정도)를 가져와서 쓴다.
            (
                firePos: firePos,
                firePower: CurrentPower.Value * 20
            ); // 테스트 용 임시 하드코딩
        
            onFire?.Invoke();

            BulletManager.Instance.SetBullet(defaultBullet);
        }
    }
}
