using CoreLib;
using UnityEngine;

public class PlayerAttackModule : MonoBehaviour
{
    [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
    [SerializeField] private AgentAim agentAim;
    [SerializeField] private GameObject attackRange;
    [SerializeField] private GameObject attackReach;
    [SerializeField] private float minRange = 1f;
    [SerializeField] private float maxRange = 15f;

    public float CurrentRange { get; private set; }

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
            attackRange.SetActive(true);
            attackReach.SetActive(true);
        }
        else
        {
            attackRange.SetActive(false);
            attackReach.SetActive(false);
        }
    }

    private void SetSize()
    {
        if (PlayerInput.AimRangeInput)
        {
            Vector3 aimPos = PlayerInput.AimInput;
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(aimPos);

            float distance = Vector2.Distance(PlayerInput.AimInput, worldPos);
            distance = Mathf.Clamp(distance, minRange, maxRange);
            CurrentRange = distance;
        }
    }
    
    private void ShowAttackRange()
    {
        attackReach.transform.position = new Vector2(CurrentRange, 0);
    }
}
