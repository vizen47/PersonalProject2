using Agents;
using Combat;
using Combat.Bullets;
using CoreLib;
using Systems.Pooling;
using Systems.TurnSystem;
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
        [SerializeField] private PlayerMovement playerMovement;        
        
        [Header("Parts")]
        [SerializeField] private GameObject attackRange;
        [SerializeField] private GameObject attackReach;
        [SerializeField] private Transform firePos;
        private Camera _camera;

        public readonly NotifyValue<float> CurrentPower = new NotifyValue<float>();

        private bool _attacked;
        
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
            if (TurnManager.Instance.CurrentState.Value == TurnManager.TurnState.Lose || TurnManager.Instance.CurrentState.Value == TurnManager.TurnState.Win) return;
            
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

        public void InitPlayerAttacked() => _attacked = false;
        
        private void ShowAttackRange()
        {
            if (TurnManager.Instance.CurrentState.Value == TurnManager.TurnState.Lose || TurnManager.Instance.CurrentState.Value == TurnManager.TurnState.Win) return;
            
            attackReach.transform.localScale = new Vector3(CurrentPower.Value,1, 1);
        }

        public void Shoot()
        {
            if (TurnManager.Instance.CurrentState.Value == TurnManager.TurnState.EnemyTurn || _attacked ||
                TurnManager.Instance.CurrentState.Value == TurnManager.TurnState.Lose ||
                TurnManager.Instance.CurrentState.Value == TurnManager.TurnState.Win) return;
            
            // Projectile projectile =
            //     PoolManager.Instance.Pop(bullet.list[1].ItemName) as Projectile;
        
            Projectile projectile =
                PoolManager.Instance.Pop(BulletManager.Instance.CurrentBullet.ItemName) as Projectile;
            
            if (projectile == null) return;
        
            projectile.InitAndFire
            (
                firePos: firePos,
                firePower: CurrentPower.Value * 25
            );
        
            onFire?.Invoke();

            BulletManager.Instance.SetBullet(defaultBullet);
            TurnManager.Instance.StartAction();
            _attacked = true;
            playerMovement.FalseCanMove();
        }
    }
}
