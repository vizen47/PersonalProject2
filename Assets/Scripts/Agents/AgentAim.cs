using Agents;
using CoreLib;
using UnityEngine;

public class AgentAim : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private Transform firePosCenter;
    
    private PlayerController playerController;
    private Camera _camera;
    
    public NotifyValue<float> angle = new  NotifyValue<float>();

    private void Awake()
    {
        _camera =  Camera.main;
    }
    
    public void Init(PlayerController player)
    {
        playerController = player;
    }

    private void Update()
    {
        RotateFirePos();
    }
    
    private void RotateFirePos()
    {
        Vector3 aimPos = playerController.PlayerInput.AimInput;
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(aimPos);

        // 실제 발사 지점에서부터의 방향을 구함
        Vector2 direction = worldPos - (Vector2)firePosCenter.position;

        float worldAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 부모의 회전만큼 빼서 로컬 각도로 변환
        float localAngle = worldAngle - transform.eulerAngles.z;

        firePosCenter.localRotation = Quaternion.Euler(0, 0, localAngle);
    }
}
