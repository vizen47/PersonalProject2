using Agents;
using Combat;
using CoreLib;
using Systems;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackModule : MonoBehaviour
{
    [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
    public UnityEvent onFire;
    [SerializeField] private PoolItemSO bullet;
    
    [SerializeField] private AgentAim agentAim;
    
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
        Projectile projectile =
            PoolManager.Instance.Pop(bullet.ItemName) as Projectile;
        
        if (projectile == null) return;
        
        projectile.InitAndFire
        (
            firePos: firePos,
            damage: 1,
            knockbackPower: 0,
            firePower: CurrentPower.Value * 20
        ); // 테스트 용 임시 하드코딩
        
        onFire?.Invoke();
    }
}
